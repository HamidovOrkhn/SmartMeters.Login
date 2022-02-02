using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Models.Global
{
    public class Role: BaseEntity
    {
        public string Title { get; set; }
        public string SiteUrl { get; set; }
        public int IsHttps { get; set; }
        public string ImageUrl { get; set; }
        public List<RolePerm> RolePerms { get; set; }
    }
}
