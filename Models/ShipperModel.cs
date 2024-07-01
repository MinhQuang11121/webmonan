using System.ComponentModel.DataAnnotations;

namespace WebDatMonAn.Models
{
    public class ShipperModel
    {
        [Key]
        public int MaShip { get; set; }

        public string TenDN { get; set; }

        public string SoDienThoai { get; set; }

        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }


    }
}
