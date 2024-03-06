using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frelsex.Models
{
    public class AggiornamentoSpedizione
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Stato { get; set; }

        [StringLength(255)]
        public string Luogo { get; set; }

        [StringLength(255)]
        public string Descrizione { get; set; }

        [Required]
        public DateTime DataOraAggiornamento { get; set; }

        [ForeignKey("Spedizione")]
        public int IDSpedizione { get; set; }
        public virtual Spedizione Spedizione { get; set; }
    }

}