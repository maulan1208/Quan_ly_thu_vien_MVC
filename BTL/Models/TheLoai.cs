using System.ComponentModel.DataAnnotations;
namespace BTL.Models
{
    public class TheLoai
    {
        [Key]
        public int TheLoaiID { get; set; }
        public string TenTheLoai { get; set; }
    }
}
