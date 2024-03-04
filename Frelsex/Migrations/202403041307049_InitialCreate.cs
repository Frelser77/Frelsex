namespace Frelsex.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AggiornamentoSpediziones",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    IDSpedizione = c.Int(nullable: false),
                    Stato = c.String(nullable: false, maxLength: 50),
                    Luogo = c.String(nullable: false, maxLength: 255),
                    Descrizione = c.String(maxLength: 255),
                    DataOraAggiornamento = c.DateTime(nullable: false),
                    Spedizione_ID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Spediziones", t => t.Spedizione_ID, cascadeDelete: true)
                .Index(t => t.Spedizione_ID);

            CreateTable(
                "dbo.Spediziones",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    IDCliente = c.Int(nullable: false),
                    NumeroIdentificativo = c.String(nullable: false, maxLength: 255),
                    DataSpedizione = c.DateTime(nullable: false),
                    Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CittàDestinataria = c.String(nullable: false, maxLength: 255),
                    IndirizzoDestinatario = c.String(nullable: false, maxLength: 255),
                    NominativoDestinatario = c.String(nullable: false, maxLength: 255),
                    CostoSpedizione = c.Decimal(nullable: false, precision: 18, scale: 2),
                    DataConsegnaPrevista = c.DateTime(nullable: false),
                    Stato = c.String(nullable: false, maxLength: 50),
                    Cliente_ID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clientes", t => t.Cliente_ID, cascadeDelete: true)
                .Index(t => t.Cliente_ID);

            CreateTable(
                "dbo.Clientes",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Nome = c.String(nullable: false, maxLength: 60),
                    CodiceFiscale = c.String(maxLength: 16),
                    PartitaIVA = c.String(maxLength: 11),
                    IsAzienda = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.AggiornamentoSpediziones", "Spedizione_ID", "dbo.Spediziones");
            DropForeignKey("dbo.Spediziones", "Cliente_ID", "dbo.Clientes");
            DropIndex("dbo.Spediziones", new[] { "Cliente_ID" });
            DropIndex("dbo.AggiornamentoSpediziones", new[] { "Spedizione_ID" });
            DropTable("dbo.Clientes");
            DropTable("dbo.Spediziones");
            DropTable("dbo.AggiornamentoSpediziones");
        }
    }
}
