using System.ComponentModel.DataAnnotations;

namespace Frelsex.Models
{
    public class Cliente
    {

        public int ID { get; set; }
        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(60, ErrorMessage = "La lunghezza del nome non può superare i 255 caratteri.")]
        [Display(Name = "Nome Cliente")]
        public string Nome { get; set; }

        [StringLength(16, ErrorMessage = "La lunghezza del codice fiscale non può superare i 16 caratteri.")]
        [Display(Name = "Codice Fiscale")]
        public string CodiceFiscale { get; set; }

        [StringLength(11, ErrorMessage = "La lunghezza della partita IVA non può superare gli 11 caratteri.")]
        [Display(Name = "Partita IVA")]
        public string PartitaIVA { get; set; }

        [Required(ErrorMessage = "La specifica del tipo di cliente è obbligatoria.")]
        [Display(Name = "Account")]
        public bool IsAzienda { get; set; }
    }

}
