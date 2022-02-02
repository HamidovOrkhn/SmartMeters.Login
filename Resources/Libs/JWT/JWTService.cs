using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Resources.Libs.JWT
{
    public class JWTService
    {
        public static DateTime GetExpiryTimestamp(string accessToken)
        {
            IJsonSerializer _serializer = new JsonNetSerializer();
            IDateTimeProvider _provider = new UtcDateTimeProvider();
            IBase64UrlEncoder _urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm _algorithm = new HMACSHA256Algorithm();
            try
            {
                IJwtValidator _validator = new JwtValidator(_serializer, _provider);
                IJwtDecoder decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
                var token = decoder.DecodeToObject<JwtToken>(accessToken);
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(token.exp);
                if (DateTime.Compare(dateTimeOffset.LocalDateTime, DateTime.Now.AddMinutes(1)) < 0)
                {
                    return DateTime.MinValue;
                }
                return dateTimeOffset.LocalDateTime;
            }
            catch (TokenExpiredException)
            {
                return DateTime.MinValue;
            }
            catch (SignatureVerificationException)
            {
                return DateTime.MinValue;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
        private static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
        public static bool ValidateToken(string authToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        }
        public static bool IsExpiredToken(string jwtToken)
        {
            IJsonSerializer _serializer = new JsonNetSerializer();
            IDateTimeProvider _provider = new UtcDateTimeProvider();
            IBase64UrlEncoder _urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm _algorithm = new HMACSHA256Algorithm();
            try
            {
                IJwtValidator _validator = new JwtValidator(_serializer, _provider);
                IJwtDecoder decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
                var token = decoder.DecodeToObject<JwtToken>(jwtToken);
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(token.exp);
                if (DateTime.Compare(dateTimeOffset.LocalDateTime, DateTime.Now.AddMinutes(1)) < 0)
                {
                    return false;
                }
                return true;
            }
            catch (TokenExpiredException)
            {
                return false;
            }
            catch (SignatureVerificationException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
