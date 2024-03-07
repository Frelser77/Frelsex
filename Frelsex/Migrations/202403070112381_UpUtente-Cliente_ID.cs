namespace Frelsex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpUtenteCliente_ID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Utentes", "Cliente_ID", "dbo.Clientes");
            DropIndex("dbo.Utentes", new[] { "Cliente_ID" });
            AddColumn("dbo.Utentes", "Cliente_ID1", c => c.Int());
            CreateIndex("dbo.Utentes", "Cliente_ID1");
            AddForeignKey("dbo.Utentes", "Cliente_ID1", "dbo.Clientes", "ID");
            DropColumn("dbo.Utentes", "ClienteID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Utentes", "ClienteID", c => c.Int());
            DropForeignKey("dbo.Utentes", "Cliente_ID1", "dbo.Clientes");
            DropIndex("dbo.Utentes", new[] { "Cliente_ID1" });
            DropColumn("dbo.Utentes", "Cliente_ID1");
            CreateIndex("dbo.Utentes", "Cliente_ID");
            AddForeignKey("dbo.Utentes", "Cliente_ID", "dbo.Clientes", "ID");
        }
    }
}
