using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Attributes
{
    public class StringthLengthWithMessage: StringLengthAttribute
    {
        public StringthLengthWithMessage(int maximumLength,int minumumLength) : base(maximumLength)
        {
            MinimumLength = minumumLength;
            ErrorMessage = "{0} minumum " + minumumLength + ", maksimum " + maximumLength + " uzunluğunda ola bilər.";
        }
    }
}
