using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Models.Global
{
    public class AppLog
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public string ActionType { get; set; }
        public string IpAddress { get; set; }
        public int PermissionId { get; set; }
        public DateTime DDate { get; set; } = DateTime.Now;
    }
}
