using SmartMeterControl.Access_MS.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartMeterControl.Access_MS.DTO
{
    public class LoginRequestDTO
    {
        [RequiredWithMessage]
        [StringthLengthWithMessage(20, 6)]
        [DisplayName("İstifadəçi adı")]
        public string Username { get; set; }
        [RequiredWithMessage]
        [StringthLengthWithMessage(20,8)]
        [DisplayName("Şifrə")]
        public string Password { get; set; }
    }
}
