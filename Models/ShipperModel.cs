using System.ComponentModel.DataAnnotations;

namespace WebDatMonAn.Models
{
    public class ShipperModel
    {
        [Key]
        public int MaShip { get; set; }

        public string Ten { get; set; }

        public string SoDienThoai { get; set; }

        public string CongTy { get; set; }

        public DateTime NgayLayDon { get; set; }
    }
}
