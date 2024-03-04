using System;
using System.ComponentModel.DataAnnotations;

namespace Frelsex.Models
{
    public class AggiornamentoSpedizione
    {
        public int ID { get; set; }
        [Required]
        public int IDSpedizione { get; set; }

        [Required]
        [StringLength(50)]
        public string Stato { get; set; }

        [Required]
        [StringLength(255)]
        public string Luogo { get; set; }

        [StringLength(255)]
        public string Descrizione { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataOraAggiornamento { get; set; }

        [Required]
        public virtual Spedizione Spedizione { get; set; }
    }

}