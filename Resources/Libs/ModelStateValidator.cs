using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartMeterControl.Access_MS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Resources.Libs
{
    public class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {
            Dictionary<string,List<string>> data = context.ModelState.ToDictionary(m => m.Key, m => m.Value.Errors.Select(s => s.ErrorMessage).ToList());
            return new BadRequestObjectResult(ResponseMessage<Dictionary<string, List<string>>>.Fail(400,"Parametrlər uyğun deyil",data));
        }
    }
}
