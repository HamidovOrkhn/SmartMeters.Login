using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartMeterControl.Access_MS.DTO.Global;
using SmartMeterControl.Access_MS.Resources.Enums;
using SmartMeterControl.Access_MS.Resources.Interfaces;
using SmartMeterControl.Access_MS.Resources.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SmartMeterControl.Access_MS.Resources.Enums.LogEnum;

namespace SmartMeterControl.Access_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalAuthController : ControllerBase
    {
        private readonly IGlobalAuthServices _globalServices;
        public GlobalAuthController(IGlobalAuthServices globalServices)
        {
            _globalServices = globalServices;
        }
        [HttpPost("[action]/{permId}/{ipAddress}")]
        public async Task<IActionResult> Login(GlobalLoginRequestDto req, int permId, string ipAddress)
        {
            var data = await _globalServices.Login(req, permId, ipAddress);
            return StatusCode(data.Code, data);
        }
        [HttpGet("[action]/{token}/{permId}")]
        public async Task<IActionResult> Refresh(string token, int permId)
        {
            var data = await _globalServices.Refresh(token, permId);
            return StatusCode(data.Code, data);
        }
        [HttpGet("[action]/{token}")]
        public IActionResult GetUser(string token)
        {
            var data = _globalServices.UserInfo(token);
            return Ok(data);
        }
        [HttpGet("[action]/{token}/{permId}/{ipAddress}")]
        public IActionResult Logout(string token, int permId, string ipAddress)
        {
            _globalServices.Logout(token, ipAddress, permId);
            return Ok("Logged Out");
        }
    }
}
