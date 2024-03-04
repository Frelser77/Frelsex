namespace Frelsex.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class NomeMigrazioneDescrittivo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Spediziones", "Stato", c => c.String(maxLength: 50));
        }

        public override void Down()
        {
            AlterColumn("dbo.Spediziones", "Stato", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
