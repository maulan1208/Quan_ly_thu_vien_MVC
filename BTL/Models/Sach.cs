using System.ComponentModel.DataAnnotations;
namespace BTL.Models
{
    public class Sach
    {
        [Key]
        public int SachID { get; set; }
        public string TenSach { get; set; }
        public string TenTheLoai { get; set; }
        public string TacGia { get; set; }
        public int SoLuong { get; set; }
        public int TheLoaiID { get; set; }
    }
}
