using Microsoft.EntityFrameworkCore;

namespace BTL.Models
{
    public class QLThuVienDBContext  :  DbContext
    {
        public QLThuVienDBContext()
        {
        }

        public QLThuVienDBContext(DbContextOptions<QLThuVienDBContext> options) : base(options) { }
        public DbSet<DocGia> DocGias { get; set; }
        public DbSet<PhieuMuon> PhieuMuons { get; set; }
        public DbSet<Sach> Sachs { get; set; }
        public DbSet<TacGia> TacGias { get; set; }
        public DbSet<TheLoai> TheLoais { get; set; }
        public DbSet<ChiTiet_PM> ChiTiet_PMs { get; set; }
        public DbSet<Account> Accounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocGia>().ToTable("tbDocGia");
            modelBuilder.Entity<PhieuMuon>().ToTable("tbPhieuMuon");
            modelBuilder.Entity<Sach>().ToTable("tbSach");
            modelBuilder.Entity<TacGia>().ToTable("tbTacGia");
            modelBuilder.Entity<TheLoai>().ToTable("tbTheLoai");
            modelBuilder.Entity<ChiTiet_PM>().ToTable("tbChiTiet_PM");
            modelBuilder.Entity<Account>().ToTable("tbAccount");

        }
    }
}
