using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Models.Global
{
    public class User:BaseEntity
    {
        public string Pin { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }
        public string Phone { get; set; }
        public string RToken { get; set; }
        public int IsActive { get; set; }
        public List<RolePerm> RolePerms { get; set; }
    }
}
