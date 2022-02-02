using SmartMeterControl.Access_MS.Models.User;
using System.Collections.Generic;

namespace SmartMeterControl.Access_MS.DTO
{
    public class LoginResponseDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string RToken { get; set; }
        public string Token { get; set; }
        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Department Department { get; set; }
        public string Job { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<PermissionResponseDTO> Permissions { get; set; }
    }

}
