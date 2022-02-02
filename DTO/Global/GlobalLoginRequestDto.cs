using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.DTO.Global
{
    public class GlobalLoginRequestDto
    {
        public string Pin { get; set; }
        public string Password { get; set; }
    }
}
