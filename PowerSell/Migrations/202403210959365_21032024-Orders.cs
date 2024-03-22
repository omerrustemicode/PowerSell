namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21032024Orders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Orders_OrdersId", "dbo.Orders");
            DropIndex("dbo.Users", new[] { "Orders_OrdersId" });
            AddColumn("dbo.Orders", "UserId", c => c.Int());
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.Users", "UserId");
            DropColumn("dbo.Users", "Orders_OrdersId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Orders_OrdersId", c => c.Int());
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropColumn("dbo.Orders", "UserId");
            CreateIndex("dbo.Users", "Orders_OrdersId");
            AddForeignKey("dbo.Users", "Orders_OrdersId", "dbo.Orders", "OrdersId");
        }
    }
}
