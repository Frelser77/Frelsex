namespace Frelsex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMorning : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Spediziones", "Stato", c => c.String(nullable: false, maxLength: 70));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Spediziones", "Stato", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
