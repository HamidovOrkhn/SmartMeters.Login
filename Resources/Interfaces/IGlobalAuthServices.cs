using SmartMeterControl.Access_MS.DTO.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Resources.Interfaces
{
    public interface IGlobalAuthServices
    {
        public Task<ResponseMessage<object>> Login(GlobalLoginRequestDto request, int permId, string IpAddress);
        public Task<ResponseMessage<object>> Refresh(string refreshToken, int permId);
        public ResponseMessage<object> UserInfo(string token);
        public void Logout(string token, string IpAddress, int PermId);
        public void LogAction(string actionType, string IpAddress, int PermId, int UserId);
    }
}
