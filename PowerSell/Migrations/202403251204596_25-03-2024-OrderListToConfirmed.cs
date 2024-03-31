namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25032024OrderListToConfirmed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrdersConfirmeds", "OrderListId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrdersConfirmeds", "OrderListId");
        }
    }
}
