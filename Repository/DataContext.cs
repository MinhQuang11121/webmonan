using Microsoft.EntityFrameworkCore;
using WebDatMonAn.Models;

namespace WebDatMonAn.Repository
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<DanhMucModel> DanhMucs { get; set; }
        public DbSet<MonAnModel> MonAns { get; set; }
        public DbSet<DanhGiaModel> DanhGias { get; set; }
        public DbSet<TaiKhoanModel> TaiKhoans { get; set; }
        public DbSet<TrangThaiModel> TrangThais { get; set; }
        public DbSet<ViTriModel> ViTris { get; set; }
        public DbSet<HoaDonModel> HoaDons { get; set; }
        public DbSet<CTHDModel> CTHDs { get; set; }
        public DbSet<NhaCungCapModel> NhaCungCaps { get; set; }
        public DbSet<ShipperModel> Shippers { get; set; }
        public DbSet<PhanQuyenModel> PhanQuyens{ get; set; }
    }
}
