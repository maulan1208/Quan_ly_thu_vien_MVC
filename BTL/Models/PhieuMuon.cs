using System.ComponentModel.DataAnnotations;

namespace BTL.Models
{
    public class PhieuMuon
    {
        [Key]
        public int PhieuMuonID { get; set; }
        [Required]
        public int DocGiaID { get; set; }
        public string TenDocGia { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Phai nhap ngay")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayMuon { get; set; }

                [DataType(DataType.Date)]
        [Required(ErrorMessage = "Phai nhap ngay")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTra { get; set; }
    }
}
