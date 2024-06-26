using System.ComponentModel.DataAnnotations;

namespace WebDatMonAn.Models.ViewModel
{
    public class LoginViewModel
    {
        [MaxLength(100)]
        [Required(ErrorMessage = " vui lòng nhập email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = " Vui lòng nhập mật khẩu")]
        [MinLength(5, ErrorMessage = "Bạn cần đặt mật khẩu tối thiểu 5 ký tự")]
        public string Password { get; set; }
    }
}
