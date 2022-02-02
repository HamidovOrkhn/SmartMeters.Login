using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Attributes
{
    public class RequiredWithMessage: RequiredAttribute
    {
        public RequiredWithMessage()
        {
            ErrorMessage = "{0} sahəsini doldurmaq vacibdir.";
        }
    }
}
