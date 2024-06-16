using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDatMonAn.Models
{
    public class TaiKhoanModel
    {
        [Key]
        public int MaTK { get; set; }
        public string TenTK { get; set; }
        public string MatKhau { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public DateTime NgaySinh { get; set; }
        public DateTime NgayTao { get; set; }
        public string DienThoai { get; set; }
        public string Hinh { get; set; }
        public int TrangThai { get; set; }
        public int MaVT { get; set; }
        [ForeignKey("MaVT")]
        public ViTriModel ViTri { get; set; }
    }
}
