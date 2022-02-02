using SmartMeterControl.Access_MS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Resources.Libs
{
    public class ExConverter
    {
        public static UrlAddDTO UrlParser(string url)
        {
            UrlAddDTO res = new UrlAddDTO();
            if (url is object)
            {
                res.IsHttps = 0;
                if (url.StartsWith("https"))
                {
                    res.IsHttps = 1;
                }
                string[] arr = url.Split("://");
                url = arr[1];
                res.Url = url;
            }
            return res;
        }
    }
}
