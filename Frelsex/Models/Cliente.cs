using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frelsex.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [StringLength(16)]
        public string CodiceFiscale { get; set; }

        [StringLength(11)]
        public string PartitaIVA { get; set; }

        [Required]
        public bool IsAzienda { get; set; }

        // Navigational property per relazione uno-a-uno con Utente
        public virtual Utente Utente { get; set; }

        // Navigational property per relazione uno-a-molti con Spedizione
        public virtual ICollection<Spedizione> Spedizioni { get; set; }
    }

}
