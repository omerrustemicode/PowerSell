namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22032024OrderConfirmed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrdersConfirmeds",
                c => new
                    {
                        OrdersConfirmedId = c.Int(nullable: false, identity: true),
                        OrdersId = c.Int(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServicePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServiceDIscount = c.Decimal(precision: 18, scale: 2),
                        IsPaid = c.Boolean(),
                        IsReady = c.Int(),
                        ClientGetService = c.Boolean(),
                        ServiceDateCreated = c.DateTime(),
                        ClientGetServiceDate = c.DateTime(),
                        ServiceDateIsReady = c.DateTime(),
                        TableId = c.Int(),
                        ServiceId = c.Int(),
                        ClientId = c.Int(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.OrdersConfirmedId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Orders", t => t.OrdersId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .ForeignKey("dbo.Tables", t => t.TableId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.OrdersId)
                .Index(t => t.TableId)
                .Index(t => t.ServiceId)
                .Index(t => t.ClientId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdersConfirmeds", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrdersConfirmeds", "TableId", "dbo.Tables");
            DropForeignKey("dbo.OrdersConfirmeds", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.OrdersConfirmeds", "OrdersId", "dbo.Orders");
            DropForeignKey("dbo.OrdersConfirmeds", "ClientId", "dbo.Clients");
            DropIndex("dbo.OrdersConfirmeds", new[] { "UserId" });
            DropIndex("dbo.OrdersConfirmeds", new[] { "ClientId" });
            DropIndex("dbo.OrdersConfirmeds", new[] { "ServiceId" });
            DropIndex("dbo.OrdersConfirmeds", new[] { "TableId" });
            DropIndex("dbo.OrdersConfirmeds", new[] { "OrdersId" });
            DropTable("dbo.OrdersConfirmeds");
        }
    }
}
