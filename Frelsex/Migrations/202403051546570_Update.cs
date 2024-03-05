namespace Frelsex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utentes", "ClienteID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utentes", "ClienteID", c => c.Int(nullable: false));
        }
    }
}
