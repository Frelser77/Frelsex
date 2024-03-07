using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frelsex.Models
{
    public class Spedizione
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string NumeroIdentificativo { get; set; }

        [Required]
        public DateTime DataSpedizione { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public decimal Peso { get; set; }

        [Required]
        [StringLength(50)]
        public string CittàDestinataria { get; set; }

        [Required]
        [StringLength(255)]
        public string IndirizzoDestinatario { get; set; }

        [Required]
        [StringLength(255)]
        public string NominativoDestinatario { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal CostoSpedizione { get; set; }

        public DateTime DataConsegnaPrevista { get; set; }

        [Required]
        [StringLength(70)]
        public string Stato { get; set; }

        [ForeignKey("Cliente")]
        public int IDCliente { get; set; }
        public virtual Cliente Cliente { get; set; }

        // Navigational property per relazione uno-a-molti con AggiornamentiSpedizione
        public virtual ICollection<AggiornamentoSpedizione> AggiornamentiSpedizione { get; set; }
    }

}