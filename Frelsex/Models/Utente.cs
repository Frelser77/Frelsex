using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frelsex.Models
{
    public class Utente
    {
        [Key, ForeignKey("Cliente")]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required]
        [StringLength(60)]
        public string Username { get; set; }

        [Required]
        [StringLength(18)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public bool IsAdmin { get; set; }

        [ScaffoldColumn(false)]
        public int? ClienteID { get; set; }

        [ScaffoldColumn(false)]
        public virtual Cliente Cliente { get; set; }

        [ScaffoldColumn(false)]
        // Navigational property per relazione con UtentiRuoli
        public virtual ICollection<UtenteRuolo> UtentiRuoli { get; set; }
    }
}