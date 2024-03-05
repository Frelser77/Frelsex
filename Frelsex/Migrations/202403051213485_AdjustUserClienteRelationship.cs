namespace Frelsex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustUserClienteRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Utentes",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Username = c.String(nullable: false, maxLength: 60),
                        Password = c.String(nullable: false, maxLength: 18),
                        Email = c.String(maxLength: 50),
                        IsAdmin = c.Boolean(nullable: false),
                        ClienteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clientes", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.UtenteRuoloes",
                c => new
                    {
                        UtenteID = c.Int(nullable: false),
                        RuoloID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UtenteID, t.RuoloID })
                .ForeignKey("dbo.Ruoloes", t => t.RuoloID, cascadeDelete: true)
                .ForeignKey("dbo.Utentes", t => t.UtenteID, cascadeDelete: true)
                .Index(t => t.UtenteID)
                .Index(t => t.RuoloID);
            
            CreateTable(
                "dbo.Ruoloes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UtenteRuoloes", "UtenteID", "dbo.Utentes");
            DropForeignKey("dbo.UtenteRuoloes", "RuoloID", "dbo.Ruoloes");
            DropForeignKey("dbo.Utentes", "ID", "dbo.Clientes");
            DropIndex("dbo.UtenteRuoloes", new[] { "RuoloID" });
            DropIndex("dbo.UtenteRuoloes", new[] { "UtenteID" });
            DropIndex("dbo.Utentes", new[] { "ID" });
            DropTable("dbo.Ruoloes");
            DropTable("dbo.UtenteRuoloes");
            DropTable("dbo.Utentes");
        }
    }
}
