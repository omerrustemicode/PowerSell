namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDragDropTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tables", "XPosition", c => c.Double());
            AddColumn("dbo.Tables", "YPosition", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tables", "YPosition");
            DropColumn("dbo.Tables", "XPosition");
        }
    }
}
