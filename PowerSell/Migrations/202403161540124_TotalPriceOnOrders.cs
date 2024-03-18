namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TotalPriceOnOrders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderCases", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.OrderCases", "Order_OrdersId", "dbo.Orders");
            DropForeignKey("dbo.OrderCases", "TableId", "dbo.Tables");
            DropForeignKey("dbo.OrderCases", "UserId", "dbo.Users");
            DropIndex("dbo.OrderCases", new[] { "ClientId" });
            DropIndex("dbo.OrderCases", new[] { "UserId" });
            DropIndex("dbo.OrderCases", new[] { "TableId" });
            DropIndex("dbo.OrderCases", new[] { "Order_OrdersId" });
            AddColumn("dbo.Orders", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropTable("dbo.OrderCases");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderCases",
                c => new
                    {
                        OrderCaseId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        GivenDate = c.DateTime(nullable: false),
                        CloseOrder = c.DateTime(nullable: false),
                        TableId = c.Int(nullable: false),
                        Order_OrdersId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderCaseId);
            
            DropColumn("dbo.Orders", "Total");
            CreateIndex("dbo.OrderCases", "Order_OrdersId");
            CreateIndex("dbo.OrderCases", "TableId");
            CreateIndex("dbo.OrderCases", "UserId");
            CreateIndex("dbo.OrderCases", "ClientId");
            AddForeignKey("dbo.OrderCases", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.OrderCases", "TableId", "dbo.Tables", "TableId", cascadeDelete: true);
            AddForeignKey("dbo.OrderCases", "Order_OrdersId", "dbo.Orders", "OrdersId");
            AddForeignKey("dbo.OrderCases", "ClientId", "dbo.Clients", "ClientId", cascadeDelete: true);
        }
    }
}
