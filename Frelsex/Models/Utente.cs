using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frelsex.Models
{
    public class Utente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(60)]
        public string Username { get; set; }

        [Required]
        [StringLength(18)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [ForeignKey("Cliente")]
        public int? ClienteID { get; set; }
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("Ruolo")]
        public int RuoloID { get; set; }
        public virtual Ruolo Ruolo { get; set; }
    }
}
