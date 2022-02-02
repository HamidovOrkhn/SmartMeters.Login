using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartMeterControl.Access_MS.Models.User;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SmartMeterControl.Access_MS.Resources.Libs
{
    public class AuthLib
    {
        public static string HashPass(string password)
        {
            var sha1 = SHA1.Create();
            var step1 = Encoding.UTF8.GetBytes(password);
            var step2 = sha1.ComputeHash(step1);
            var encrytedPassword = ByteArrayToString(step2);
            return encrytedPassword;
        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        public static string GenerateJSONWebToken(User userInfo)
        {
            IConfiguration _configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier,userInfo.Id.ToString()),
            new Claim(ClaimTypes.Role,userInfo.RoleId.ToString()),
            new Claim(ClaimTypes.Country,userInfo.DivisionId.ToString()),
            new Claim(ClaimTypes.StreetAddress, userInfo.SubjectId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(1440),
                signingCredentials: credentials);
            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;
        }
        public static string GenerateJSONWebToken(Access_MS.Models.Global.User userInfo, string salt, out DateTime dateExp)
        {
            IConfiguration _configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            dateExp = DateTime.Now.AddMinutes(2);
            object userData = new
            {
                Id = userInfo.Id,
                Pin = userInfo.Pin
            };

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("refreshToken", userInfo.RToken)
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims,
                expires: dateExp,
                signingCredentials: credentials);


            //string encrypted = GlobalAuthLib.Encrypt(JsonConvert.SerializeObject(userData), true, salt);
            token.Payload["userData"] = userData;
            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;
        }
    }
}
