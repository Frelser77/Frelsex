namespace Frelsex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSchemaAfterChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Spediziones", "Cliente_ID", "dbo.Clientes");
            DropIndex("dbo.Spediziones", new[] { "Cliente_ID" });
            AlterColumn("dbo.Spediziones", "NumeroIdentificativo", c => c.String(maxLength: 255));
            AlterColumn("dbo.Spediziones", "Cliente_ID", c => c.Int());
            CreateIndex("dbo.Spediziones", "Cliente_ID");
            AddForeignKey("dbo.Spediziones", "Cliente_ID", "dbo.Clientes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Spediziones", "Cliente_ID", "dbo.Clientes");
            DropIndex("dbo.Spediziones", new[] { "Cliente_ID" });
            AlterColumn("dbo.Spediziones", "Cliente_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Spediziones", "NumeroIdentificativo", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Spediziones", "Cliente_ID");
            AddForeignKey("dbo.Spediziones", "Cliente_ID", "dbo.Clientes", "ID", cascadeDelete: true);
        }
    }
}
