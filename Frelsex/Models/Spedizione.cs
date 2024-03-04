using System;
using System.ComponentModel.DataAnnotations;

namespace Frelsex.Models
{
    public class Spedizione
    {
        public int ID { get; set; }
        [Required]
        public int IDCliente { get; set; }

        [StringLength(255)]
        public string NumeroIdentificativo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataSpedizione { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Il peso deve essere maggiore di 0.")]
        public decimal Peso { get; set; }

        [Required]
        [StringLength(255)]
        public string CittàDestinataria { get; set; }

        [Required]
        [StringLength(255)]
        public string IndirizzoDestinatario { get; set; }

        [Required]
        [StringLength(255)]
        public string NominativoDestinatario { get; set; }

        public decimal CostoSpedizione { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataConsegnaPrevista { get; set; }

        [StringLength(50)]
        public string Stato { get; set; }

        public virtual Cliente Cliente { get; set; }
    }

}