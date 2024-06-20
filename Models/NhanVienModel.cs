using System.ComponentModel.DataAnnotations;

namespace WebDatMonAn.Models
{
    public class NhanVienModel
    {
        [Key]
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public string MatKhau { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
    }
}
