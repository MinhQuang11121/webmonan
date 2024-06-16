using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDatMonAn.Models
{
    public class HoaDonModel
    {
        [Key]
        public int MaHD { get; set; }
        public DateTime NgayGiao { get; set; }
        public DateTime NgayDat { get; set; }
        public string DiaChi { get; set; }
        public string CachThanhtoan { get; set; }
        public string loaivanchuyen { get; set; }
        public double phivanchuyen { get; set; }
        public string ghichu { get; set; }
        public int MaTK { get; set; }
        [ForeignKey("MaTK")]
        public TaiKhoanModel TaiKhoan { get; set; }
        public int MaTrangThai { get; set; }
        [ForeignKey("MaTrangThai")]
        public TrangThaiModel TrangThai { get; set; }
    }
}
