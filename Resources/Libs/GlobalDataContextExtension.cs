using SmartMeterControl.Access_MS.Database;
using SmartMeterControl.Access_MS.Models.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Resources.Libs
{
    public static class GlobalDataContextExtension
    {
        internal static bool HasPermitted(this GlobalDataContext context, int permId, int userId)
        {
            bool comptr = context.GlobalRolesPerms.Any(a => a.UserId == userId && a.RoleId == permId);
            if (!comptr)
            {
                return false;
            }
            return true;
        }
    }
}
