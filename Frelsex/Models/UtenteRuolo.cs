using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frelsex.Models
{
    public class UtenteRuolo
    {
        [Key, Column(Order = 0), ForeignKey("Utente")]
        public int UtenteID { get; set; }

        [Key, Column(Order = 1), ForeignKey("Ruolo")]
        public int RuoloID { get; set; }

        public virtual Utente Utente { get; set; }
        public virtual Ruolo Ruolo { get; set; }
    }
}