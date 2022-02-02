using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartMeterControl.Access_MS.Attributes;
using SmartMeterControl.Access_MS.Database;
using SmartMeterControl.Access_MS.DTO;
using SmartMeterControl.Access_MS.Resources.Libs;
using SmartMeterControl.Access_MS.Resources.Libs.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly GlobalDataContext _db;
        public string JwtService { get; private set; }

        public ToolsController(IConfiguration configuration, GlobalDataContext db)
        {
            _configuration = configuration;
            _db = db;
        }
        [HttpGet("[action]")]
        public IActionResult EncryptData()
        {
            string data = GlobalAuthLib.Encrypt("TestData", true, _configuration["Salt:Key"]);
            return Ok(data);
        }
        [HttpGet("[action]/{token}")]
        public IActionResult DecryptData(string token)
        {
            string data = GlobalAuthLib.Decrypt(token, true, _configuration["Salt:Key"]);
            return Ok(data);
        }
        [OAuthApi]
        [HttpGet("[action]/{token}")]
        public IActionResult ValidateToken(string token)
        {
            DateTime date = JWTService.GetExpiryTimestamp(token);
            if (date == DateTime.MinValue)
            {
                return BadRequest("Not Valid Jwt");
            }
            return Ok("Valid Jwt");
        }
        [HttpPost("[action]")]
        public IActionResult ValidateUrl([FromBody]UrlAddDTO url)
        {
            var existed = _db.GlobalRoles.Where(a => a.SiteUrl == ExConverter.UrlParser(url.Url).Url).FirstOrDefault();
            if (existed is null)
            {
                return BadRequest("Not Valid Url");
            }
            return Ok(url);
        }
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult Auth()
        {
            return Ok("Valid Jwt");
        }
    }
}
