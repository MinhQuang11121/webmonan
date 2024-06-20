using System.ComponentModel.DataAnnotations;

namespace WebDatMonAn.Models
{
    public class DiaDiemModel
    {
        [Key]
        public int MaDD { get; set; }
        public string TenTinhThanh { get; set; }
        public string TenQuanHuyen { get; set; }
        public string TenPhuongXa { get; set; }
        public string DiaChiCuThe { get; set; }
    }
}
