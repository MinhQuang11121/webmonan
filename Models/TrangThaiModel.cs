using System.ComponentModel.DataAnnotations;

namespace WebDatMonAn.Models
{
    public class TrangThaiModel
    {
        [Key]
        public int MaTrangThai { get; set; }
        public string TenTrangThai { get; set; }
        public string MoTa { get; set; }
    }
}
