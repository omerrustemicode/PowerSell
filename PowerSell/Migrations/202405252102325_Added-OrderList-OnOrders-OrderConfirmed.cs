namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderListOnOrdersOrderConfirmed : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Orders", "OrderListId");
            CreateIndex("dbo.OrdersConfirmeds", "OrderListId");
            AddForeignKey("dbo.Orders", "OrderListId", "dbo.OrderLists", "OrderListId", cascadeDelete: true);
            AddForeignKey("dbo.OrdersConfirmeds", "OrderListId", "dbo.OrderLists", "OrderListId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdersConfirmeds", "OrderListId", "dbo.OrderLists");
            DropForeignKey("dbo.Orders", "OrderListId", "dbo.OrderLists");
            DropIndex("dbo.OrdersConfirmeds", new[] { "OrderListId" });
            DropIndex("dbo.Orders", new[] { "OrderListId" });
        }
    }
}
