using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Resources.Enums
{
    public class LogEnum
    {
        public enum ActionType
        {
            Login, 
            Logout
        }
        public static string GetActionType(ActionType en)
        {
            
            switch (en)
            {
                case ActionType.Login:
                    return "Enter";
                case ActionType.Logout:
                    return "Exit";
                default:
                    return "undefined";
            }
        }
    }
}
