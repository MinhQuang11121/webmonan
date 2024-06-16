using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDatMonAn.Models
{
    public class DanhGiaModel
    {
        [Key]
        public int MaDG { get; set; }
    
         public string NguoiDanhGia { get; set; }
        public string NoiDung { get; set; }
        public int TrangThai { get; set; }

       
        public int MaMonAn { get; set; }
        [ForeignKey("MaMonAn")]
        public MonAnModel MonAn { get; set; }
    }
}
