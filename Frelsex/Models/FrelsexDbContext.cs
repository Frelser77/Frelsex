using System.Data.Entity;

namespace Frelsex.Models
{
    public class FrelsexDbContext : DbContext
    {
        public FrelsexDbContext() : base("name=Frelsex")
        {
            // Configurazione opzionale del DbContext
        }

        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Spedizione> Spedizioni { get; set; }
        public DbSet<AggiornamentoSpedizione> AggiornamentiSpedizione { get; set; }

    }
}