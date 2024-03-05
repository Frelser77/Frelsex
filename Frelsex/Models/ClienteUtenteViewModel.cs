using System.ComponentModel.DataAnnotations;

namespace Frelsex.Models
{
    public class ClienteUtenteViewModel
    {
        // Proprietà del Cliente
        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(60, ErrorMessage = "La lunghezza del nome non può superare i 255 caratteri.")]
        public string Nome { get; set; }

        [StringLength(16, ErrorMessage = "La lunghezza del codice fiscale non può superare i 16 caratteri.")]
        public string CodiceFiscale { get; set; }

        [StringLength(11, ErrorMessage = "La lunghezza della partita IVA non può superare gli 11 caratteri.")]
        public string PartitaIVA { get; set; }

        [Required(ErrorMessage = "La specifica del tipo di cliente è obbligatoria.")]
        public bool IsAzienda { get; set; }

        // Proprietà dell'Utente
        [Required]
        [StringLength(60)]
        public string Username { get; set; }

        [Required]
        [StringLength(18)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public bool IsAdmin { get; set; }
    }
}