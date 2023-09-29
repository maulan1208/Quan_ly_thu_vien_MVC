using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BTL.Models
{
    public class DocGia
    {
        [Key]
        public int DocGiaID { get; set; }
        public string TenDocGia { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
    }
}
