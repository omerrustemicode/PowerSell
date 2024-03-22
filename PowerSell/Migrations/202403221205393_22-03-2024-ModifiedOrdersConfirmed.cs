namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22032024ModifiedOrdersConfirmed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrdersConfirmeds", "OrdersId", "dbo.Orders");
            DropIndex("dbo.OrdersConfirmeds", new[] { "OrdersId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.OrdersConfirmeds", "OrdersId");
            AddForeignKey("dbo.OrdersConfirmeds", "OrdersId", "dbo.Orders", "OrdersId", cascadeDelete: true);
        }
    }
}
