using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartMeterControl.Access_MS.Resources.Libs.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Attributes
{
    public class OAuthApi :Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Auth api middleware invoked");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];
            bool isValid = false;
            isValid = JWTService.ValidateToken(token);
            if (!isValid)
            {
                context.Result = new ObjectResult(new { Token = "", Status = "UnAuthorized" })
                {
                    StatusCode = 401
                };
                return;
            }
        }
    }
}
