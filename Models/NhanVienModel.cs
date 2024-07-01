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
        public string Email { get; set; }


        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }

        public ICollection<ChiTietChucNang> ChiTietChucNangs { get; set; }
    }
}
