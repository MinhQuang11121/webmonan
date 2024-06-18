using System.ComponentModel.DataAnnotations;

namespace WebDatMonAn.Models
{
    public class NhaCungCapModel
    {
        [Key]
        public int MaNCC { get; set; }

        public string TenNCC { get; set; }

        public string Email { get; set; }

        public string DiaChi { get; set; }

        public string SoDienThoai { get; set; }

    }
}
