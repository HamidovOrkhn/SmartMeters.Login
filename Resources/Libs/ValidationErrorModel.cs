using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Resources.Libs
{
    public class ValidationErrorModel
    {
        public string Key { get; set; }
        public List<string> Errors { get; set; }
    }
}
