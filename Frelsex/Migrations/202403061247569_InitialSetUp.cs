namespace Frelsex.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialSetUp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AggiornamentoSpediziones",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Stato = c.String(nullable: false, maxLength: 50),
                    Luogo = c.String(maxLength: 255),
                    Descrizione = c.String(maxLength: 255),
                    DataOraAggiornamento = c.DateTime(nullable: false),
                    IDSpedizione = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Spediziones", t => t.IDSpedizione, cascadeDelete: true)
                .Index(t => t.IDSpedizione);

            CreateTable(
                "dbo.Spediziones",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    NumeroIdentificativo = c.String(nullable: false),
                    DataSpedizione = c.DateTime(nullable: false),
                    Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CittàDestinataria = c.String(nullable: false, maxLength: 50),
                    IndirizzoDestinatario = c.String(nullable: false, maxLength: 255),
                    NominativoDestinatario = c.String(nullable: false, maxLength: 255),
                    CostoSpedizione = c.Decimal(nullable: false, precision: 18, scale: 2),
                    DataConsegnaPrevista = c.DateTime(nullable: false),
                    Stato = c.String(nullable: false, maxLength: 50),
                    IDCliente = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clientes", t => t.IDCliente, cascadeDelete: true)
                .Index(t => t.IDCliente);

            CreateTable(
                "dbo.Clientes",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Nome = c.String(nullable: false, maxLength: 255),
                    CodiceFiscale = c.String(maxLength: 16),
                    PartitaIVA = c.String(maxLength: 11),
                    IsAzienda = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Utentes",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Username = c.String(nullable: false, maxLength: 60),
                    Password = c.String(nullable: false, maxLength: 18),
                    Email = c.String(maxLength: 50),
                    IsAdmin = c.Boolean(nullable: false),
                    ClienteID = c.Int(),
                    RuoloID = c.Int(nullable: false),
                    Cliente_ID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clientes", t => t.Cliente_ID)
                .ForeignKey("dbo.Ruoloes", t => t.RuoloID, cascadeDelete: true)
                .Index(t => t.RuoloID)
                .Index(t => t.Cliente_ID);

            CreateTable(
                "dbo.Ruoloes",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Nome = c.String(nullable: false, maxLength: 255),
                })
                .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.AggiornamentoSpediziones", "IDSpedizione", "dbo.Spediziones");
            DropForeignKey("dbo.Spediziones", "IDCliente", "dbo.Clientes");
            DropForeignKey("dbo.Utentes", "RuoloID", "dbo.Ruoloes");
            DropForeignKey("dbo.Utentes", "Cliente_ID", "dbo.Clientes");
            DropIndex("dbo.Utentes", new[] { "Cliente_ID" });
            DropIndex("dbo.Utentes", new[] { "RuoloID" });
            DropIndex("dbo.Spediziones", new[] { "IDCliente" });
            DropIndex("dbo.AggiornamentoSpediziones", new[] { "IDSpedizione" });
            DropTable("dbo.Ruoloes");
            DropTable("dbo.Utentes");
            DropTable("dbo.Clientes");
            DropTable("dbo.Spediziones");
            DropTable("dbo.AggiornamentoSpediziones");
        }
    }
}
