using System.ComponentModel.DataAnnotations;

namespace BTL.Models
{
    public class ChiTiet_PM
    {
        [Key]
        public int ChiTietID { get; set; }
        [Required]
        public int PhieuMuonID { get; set; }
        [Required]
        public int DocGiaID { get; set; }
        public string TenDocGia { get; set; }
        [Required]
        public int SachID { get; set; }
        public string TenSach { get; set; }
        public int SoLuong { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayTraThucTe { get; set; }
    }
}
