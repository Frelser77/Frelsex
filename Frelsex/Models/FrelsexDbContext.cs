using System.Data.Entity;

namespace Frelsex.Models
{
    public class FrelsexDbContext : DbContext
    {
        public FrelsexDbContext() : base("name=Frelsex2")
        {
            // Configurazione opzionale del DbContext
            // Database.SetInitializer<FrelsexDbContext>(new CreateDatabaseIfNotExists<FrelsexDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utente>()
                .HasOptional(u => u.Cliente) // Configura la relazione come facoltativa
                .WithOptionalDependent(c => c.Utente); // Specifica quale lato è il dipendente senza specificare MapKey

            // ... altre configurazioni ...
        }

        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Ruolo> Ruoli { get; set; }
        public DbSet<Spedizione> Spedizioni { get; set; }
        public DbSet<AggiornamentoSpedizione> AggiornamentiSpedizione { get; set; }
    }
}