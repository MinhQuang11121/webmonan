using System.ComponentModel.DataAnnotations;

namespace WebDatMonAn.Models.ViewModel
{
    public class QuenMatKhauViewModel
    {
        [Required(ErrorMessage = "yêu cầu nhập địa chỉ email")]
        [EmailAddress]
        public string Email { get; set; }
    }

}
