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

        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Ruolo> Ruoli { get; set; }
        public DbSet<UtenteRuolo> UtentiRuoli { get; set; }
    }
}