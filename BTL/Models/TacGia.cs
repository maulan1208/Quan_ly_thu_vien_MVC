using System.ComponentModel.DataAnnotations;

namespace BTL.Models
{
    public class TacGia
    {

        public int TacGiaID { get; set; }
        [Key]
        public string TenTacGia { get; set; }
     //   public ICollection<Sach>? TacPham { get; set; }
    }
}
