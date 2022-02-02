using Microsoft.EntityFrameworkCore;
using SmartMeterControl.Access_MS.Database;
using SmartMeterControl.Access_MS.DTO;
using SmartMeterControl.Access_MS.Models.User;
using SmartMeterControl.Access_MS.Resources.Interfaces;
using SmartMeterControl.Access_MS.Resources.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Resources.Concrete
{
    public class AuthServices : IAuthServices
    {
        private readonly DataContext _db;
        public AuthServices(DataContext db)
        {
            _db = db;
        }

        public async Task<ResponseMessage<LoginResponseDTO>> Login(LoginRequestDTO request)
        {
            string encrytedPassword = AuthLib.HashPass(request.Password);

            User user =await _db.SmartMeterUsers.Where(I => I.Username == request.Username && I.Password == encrytedPassword).FirstOrDefaultAsync();
            if (user is null)
            {
                return ResponseMessage<LoginResponseDTO>.Fail(400, "İstifadəçi adı və ya şifrə yanlışdı.");
            }
            List<RolePerm> rolePerm = _db.SmartMeterRolesPerms.Where(T => T.RoleId == user.RoleId).AsNoTracking().ToList();
            List<PermissionResponseDTO> permissions = (from roleperm in rolePerm
                                            join permission in _db.SmartMeterPermissions on roleperm.PermissionId equals permission.Id
                                            select new PermissionResponseDTO
                                            {
                                                Id = roleperm.PermissionId,
                                                Title = permission.Title
                                            }).ToList();
            user.RToken = Guid.NewGuid().ToString();
            string token = AuthLib.GenerateJSONWebToken(user);
            await _db.SaveChangesAsync();
            LoginResponseDTO response = new()
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                Job = user.Position,
                Phone = user.Phone,
                DivisionId = user.DivisionId,
                DivisionName = _db.Divisions.AsNoTracking().FirstOrDefaultAsync(T => T.Id == user.DivisionId.ToString()).Result?.Name,
                SubjectId = user.SubjectId,
                SubjectName = _db.Subjects.AsNoTracking().FirstOrDefaultAsync(T => T.ID == user.SubjectId.ToString()).Result?.Name,
                Department = _db.SmartMeterDepartments.Find(user.DepartmentId),
                RoleId =user.RoleId,
                RoleName= _db.SmartMeterRoles.Find(user.RoleId)?.Title,
                Token = token,
                RToken = user.RToken,
                Permissions = permissions
            };
            return ResponseMessage<LoginResponseDTO>.Success(response);
        }

        public async Task<ResponseMessage<RefreshResponseDTO>> Refresh(string refresh)
        {
            User user = await _db.SmartMeterUsers.Where(I => I.RToken == refresh).FirstOrDefaultAsync();
            if (user is null)
            {
                return ResponseMessage<RefreshResponseDTO>.Fail(400, "Refresh token yanlışdı.");
            }
            user.RToken = Guid.NewGuid().ToString();
            string token = AuthLib.GenerateJSONWebToken(user);
            await _db.SaveChangesAsync();
            RefreshResponseDTO response = new()
            {
                Token = user.RToken,
                Refresh = token
            };
            return ResponseMessage<RefreshResponseDTO>.Success(response);
        }
    }
}
