namespace LuanVan.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<BAOHANH> BAOHANHs { get; set; }
        public virtual DbSet<CHITIETBH> CHITIETBHs { get; set; }
        public virtual DbSet<CHITIETDONHANG> CHITIETDONHANGs { get; set; }
        public virtual DbSet<CHITIETNHAP> CHITIETNHAPs { get; set; }
        public virtual DbSet<CHITIETSANPHAM> CHITIETSANPHAMs { get; set; }
        public virtual DbSet<DONGSANPHAM> DONGSANPHAMs { get; set; }
        public virtual DbSet<DONHANG> DONHANGs { get; set; }
        public virtual DbSet<GIASP> GIASPs { get; set; }
        public virtual DbSet<HINHANHSP> HINHANHSPs { get; set; }
        public virtual DbSet<HINHTHUCTHANHTOAN> HINHTHUCTHANHTOANs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<KHUYENMAI> KHUYENMAIs { get; set; }
        public virtual DbSet<LOAINV> LOAINVs { get; set; }
        public virtual DbSet<NHACUNGCAP> NHACUNGCAPs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<NHASANXUAT> NHASANXUATs { get; set; }
        public virtual DbSet<NHOMSANPHAM> NHOMSANPHAMs { get; set; }
        public virtual DbSet<PHIEUNHAPSP> PHIEUNHAPSPs { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
        public virtual DbSet<TRANGTHAIBH> TRANGTHAIBHs { get; set; }
        public virtual DbSet<TRANGTHAIDONHANG> TRANGTHAIDONHANGs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CHITIETBH>()
                .Property(e => e.CTBH_ID)
                .IsUnicode(false);

            modelBuilder.Entity<CHITIETBH>()
                .Property(e => e.SP_ID)
                .IsUnicode(false);

            modelBuilder.Entity<CHITIETDONHANG>()
                .Property(e => e.CTDH_ID)
                .IsUnicode(false);

            modelBuilder.Entity<CHITIETDONHANG>()
                .Property(e => e.DN_ID)
                .IsUnicode(false);

            modelBuilder.Entity<CHITIETDONHANG>()
                .Property(e => e.SP_ID)
                .IsUnicode(false);

            modelBuilder.Entity<CHITIETNHAP>()
                .Property(e => e.CTN_ID)
                .IsUnicode(false);

            modelBuilder.Entity<CHITIETNHAP>()
                .Property(e => e.PN_ID)
                .IsUnicode(false);

            modelBuilder.Entity<CHITIETNHAP>()
                .Property(e => e.SP_ID)
                .IsUnicode(false);

            modelBuilder.Entity<CHITIETSANPHAM>()
                .Property(e => e.CTSP_ID)
                .IsUnicode(false);

            modelBuilder.Entity<CHITIETSANPHAM>()
                .Property(e => e.CTSP_CAMERATRUOC)
                .IsUnicode(false);

            modelBuilder.Entity<CHITIETSANPHAM>()
                .Property(e => e.CTSP_CAMERASAU)
                .IsUnicode(false);

            modelBuilder.Entity<DONGSANPHAM>()
                .Property(e => e.DSP_ID)
                .IsUnicode(false);

            modelBuilder.Entity<DONHANG>()
                .Property(e => e.DN_ID)
                .IsUnicode(false);

            modelBuilder.Entity<DONHANG>()
                .Property(e => e.KH_ID)
                .IsUnicode(false);

            modelBuilder.Entity<GIASP>()
                .Property(e => e.GIA_ID)
                .IsUnicode(false);

            modelBuilder.Entity<HINHANHSP>()
                .Property(e => e.HA_ID)
                .IsUnicode(false);

            modelBuilder.Entity<HINHANHSP>()
                .Property(e => e.CTSP_ID)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.KH_ID)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.KH_EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.KH_SDT)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.KH_TAIKHOAN)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.KH_MATKHAU)
                .IsUnicode(false);

            modelBuilder.Entity<KHUYENMAI>()
                .Property(e => e.KM_ID)
                .IsUnicode(false);

            modelBuilder.Entity<NHACUNGCAP>()
                .Property(e => e.NCC_ID)
                .IsUnicode(false);

            modelBuilder.Entity<NHACUNGCAP>()
                .Property(e => e.NCC_SDT)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.NV_ID)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.NV_EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.NV_MATKHAU)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.NV_TAIKHOAN)
                .IsUnicode(false);

            modelBuilder.Entity<NHASANXUAT>()
                .Property(e => e.NSX_ID)
                .IsUnicode(false);

            modelBuilder.Entity<NHASANXUAT>()
                .Property(e => e.NSX_SDT)
                .IsUnicode(false);

            modelBuilder.Entity<NHOMSANPHAM>()
                .Property(e => e.NSP_ID)
                .IsUnicode(false);

            modelBuilder.Entity<PHIEUNHAPSP>()
                .Property(e => e.PN_ID)
                .IsUnicode(false);

            modelBuilder.Entity<PHIEUNHAPSP>()
                .Property(e => e.NV_ID)
                .IsUnicode(false);

            modelBuilder.Entity<PHIEUNHAPSP>()
                .Property(e => e.NCC_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.SP_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.CTSP_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.NSP_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.KM_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.GIA_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.DSP_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.NSX_ID)
                .IsUnicode(false);
        }

        public int autottang(string namettable, string namerow, int sl)
        {
            DataContext a = new DataContext();
            int result = a.Database.SqlQuery<int>("select count(*) from " + namettable + " where " + namerow + " = '" + sl + "'").SingleOrDefault();
            if (result >= 1)
            {
                return autottang(namettable, namerow, sl + 1);
            }
            else
            {
                return sl;
            }
        }

        public string KiemTraID(string nametable, string namerow, string id)
        {
            DataContext a = new DataContext();
            string result = a.Database.SqlQuery<string>("select " + id + " from " + nametable + " where " + namerow + " = '" + id + "'").SingleOrDefault();
            return result;
        }

    }
}
