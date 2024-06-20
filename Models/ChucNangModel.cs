using System.ComponentModel.DataAnnotations;

namespace WebDatMonAn.Models
{
    public class ChucNangModel
    {
        [Key]
        public int MaCN { get; set; }

        public string TenQuyen { get; set; }

        public string MoTa { get; set; }
    }
}
