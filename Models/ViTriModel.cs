using System.ComponentModel.DataAnnotations;

namespace WebDatMonAn.Models
{
    public class ViTriModel
    {
        [Key]
        public int MaVT { get; set; }
        public string TenVT { get; set; }
        public string MoTa { get; set; }
    }
}
