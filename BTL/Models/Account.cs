using System.ComponentModel.DataAnnotations;

namespace BTL.Models
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Phai nhap user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Phai nhap password")]
        public string Password { get; set; }
    }
}
