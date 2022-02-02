using SmartMeterControl.Access_MS.DTO;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Resources.Interfaces
{
    public interface IAuthServices
    {
        public Task<ResponseMessage<LoginResponseDTO>> Login(LoginRequestDTO request);
        public Task<ResponseMessage<RefreshResponseDTO>> Refresh(string refresh);
    }
}
