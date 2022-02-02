using Microsoft.AspNetCore.Mvc;
using SmartMeterControl.Access_MS.DTO;
using SmartMeterControl.Access_MS.Resources;
using SmartMeterControl.Access_MS.Resources.Interfaces;
using SmartMeterControl.Access_MS.Resources.Libs;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Controllers
{
    [Route("api/access/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _auth;
        public AuthController(IAuthServices auth)
        {
            _auth = auth;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            ResponseMessage<LoginResponseDTO> response = await _auth.Login(request);
            return StatusCode(200,response);
        }

        [HttpGet]
        [Route("refresh/{refresh}")]
        public async Task<IActionResult> Refresh(string refresh)
        {
            ResponseMessage<RefreshResponseDTO> response = await _auth.Refresh(refresh);
            return StatusCode(200, response);
        }
    }
}
