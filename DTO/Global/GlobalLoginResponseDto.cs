using SmartMeterControl.Access_MS.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.DTO.Global
{
    public class GlobalLoginResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Pin { get; set; }
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
