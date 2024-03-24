namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _24032024OrderManagerAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderListId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderListId");
        }
    }
}
