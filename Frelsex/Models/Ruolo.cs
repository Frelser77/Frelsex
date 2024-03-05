using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Frelsex.Models
{
    public class Ruolo
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(256)] // Aggiustare in base alle specifiche del database
        public string Nome { get; set; }

        // Navigational property per relazione con UtentiRuoli
        public virtual ICollection<UtenteRuolo> UtentiRuoli { get; set; }
    }
}