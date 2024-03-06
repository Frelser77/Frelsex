using System.ComponentModel.DataAnnotations;

namespace Frelsex.Models
{
    public class MyLogin
    {
        [Required]
        [Display(Name = "Username or Email")]
        public string UsernameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}