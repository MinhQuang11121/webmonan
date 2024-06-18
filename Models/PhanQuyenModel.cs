using System.ComponentModel.DataAnnotations;

namespace WebDatMonAn.Models
{
    public class PhanQuyenModel
    {
        [Key]
        public int MaPQ { get; set; }

        public string TenQuyen { get; set; }

        public string MoTa { get; set; }
    }
}
