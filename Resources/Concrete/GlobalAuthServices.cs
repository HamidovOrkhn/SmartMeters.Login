using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SmartMeterControl.Access_MS.Database;
using SmartMeterControl.Access_MS.DTO.Global;
using SmartMeterControl.Access_MS.Models.Global;
using SmartMeterControl.Access_MS.Resources.Interfaces;
using SmartMeterControl.Access_MS.Resources.Libs;
using SmartMeterControl.Access_MS.Resources.Libs.JWT;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static SmartMeterControl.Access_MS.Resources.Enums.LogEnum;

namespace SmartMeterControl.Access_MS.Resources.Concrete
{
    public class GlobalAuthServices : IGlobalAuthServices
    {
        private readonly GlobalDataContext _db;
        private readonly DataContext _dbCommon;
        private readonly IConfiguration _configuration;
        public GlobalAuthServices(GlobalDataContext db, DataContext dbcommon, IConfiguration configuration)
        {
            _db = db;
            _dbCommon = dbcommon;
            _configuration = configuration;
        }

        public async Task<ResponseMessage<object>> Login(GlobalLoginRequestDto request, int permId, string IpAddress)
        {
            string encrytedPassword = AuthLib.HashPass(request.Password);

            User user = await _db.GlobalUsers.Where(I => I.Pin == request.Pin && I.Password == encrytedPassword).FirstOrDefaultAsync();
            if (user is null)
            {
                return ResponseMessage<object>.Fail(401, "İstifadəçi adı və ya şifrə yanlışdı.");
            }

            user.RToken = Guid.NewGuid().ToString();
            string token = AuthLib.GenerateJSONWebToken(user, _configuration["Salt:Key"],out DateTime dateExp);
            await _db.SaveChangesAsync();
            object response = new
            {
                Token = token
            };
            bool isPermitted = _db.HasPermitted(permId, user.Id);
            if (!isPermitted)
            {
                return ResponseMessage<object>.Fail(401, "Icazə mövcud deyil");
            }
            LogAction(GetActionType(ActionType.Login), IpAddress, permId, user.Id);
            return ResponseMessage<object>.Success(response);
        }

        public async Task<ResponseMessage<object>> Refresh(string jwt, int permId)
        {
            string refreshToken = default;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var tk = handler.ReadJwtToken(token: jwt);
                refreshToken = tk.Payload["refreshToken"].ToString();
            }
            catch (Exception)
            {
                throw;
            }
          
            User user = await _db.GlobalUsers.Where(I => I.RToken == refreshToken).FirstOrDefaultAsync();
            if (user is null)
            {
                return ResponseMessage<object>.Fail(401, "Refresh token yanlışdı.");
            }
            user.RToken = Guid.NewGuid().ToString();
            string token = AuthLib.GenerateJSONWebToken(user, _configuration["Salt:Key"], out DateTime dateExp);
            await _db.SaveChangesAsync();
            object response = new
            {
                Token = token
            };
            bool isPermitted = _db.HasPermitted(permId, user.Id);
            if (!isPermitted)
            {
                return ResponseMessage<object>.Fail(401, "Icazə mövcud deyil");
            }
            return ResponseMessage<object>.Success(response);
        }
        public ResponseMessage<object> UserInfo(string token)
        {
            UserDataDto userData = GlobalAuthLib.ReturnPayloadUserData(token);
            List<SmartMeterControl.Access_MS.Models.Global.RolePerm> roles = _db.GlobalRolesPerms.Where(a => a.UserId == userData.Id).ToList();
            var data = (from rls in _db.GlobalRoles.ToList()
                    join userrls in roles on rls.Id equals userrls.RoleId
                    select new
                    {
                        rls.Id,
                        rls.Title,
                        rls.SiteUrl,
                        rls.IsHttps,
                        rls.ImageUrl
                    });
            return ResponseMessage<object>.Success(data);
        }
        public void Logout(string token, string IpAddress, int PermId)
        {
            string userData = default;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var tk = handler.ReadJwtToken(token: token);
                userData = tk.Payload["userData"].ToString();
                UserDataDto data = JsonConvert.DeserializeObject<UserDataDto>(userData);
                LogAction(GetActionType(ActionType.Logout), IpAddress, PermId, data.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void LogAction(string actionType, string IpAddress, int PermId, int UserId)
        {
            _db.AppLogs.Add(new AppLog { ActionType = actionType, IpAddress = IpAddress, PermissionId = PermId, UserId = UserId });
            _db.SaveChanges();
        }
       
    }
}
