using System.ComponentModel.DataAnnotations;

namespace WebDatMonAn.Models.ViewModel
{
    public class RegisterViewModel
    {
        public string TenTk { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string MatKhau { get; set; }

        [MinLength(5, ErrorMessage = "Bạn cần đặt mật khẩu tối thiểu 5 ký tự")]
        [Compare("MatKhau", ErrorMessage = "Vui lòng nhập mật khẩu giống nhau")]
        public string NhapMatKhauLai { get; set; }
    }
}
