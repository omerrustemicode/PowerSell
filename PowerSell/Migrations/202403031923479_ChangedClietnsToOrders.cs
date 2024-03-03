namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedClietnsToOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                        ClientPhone = c.String(),
                        ClientEmail = c.String(),
                        ClientRegDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrdersId = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServicePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServiceDIscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPaid = c.Boolean(nullable: false),
                        IsReady = c.Int(nullable: false),
                        ClientGetService = c.Boolean(nullable: false),
                        ServiceDateCreated = c.DateTime(nullable: false),
                        ClientGetServiceDate = c.DateTime(nullable: false),
                        ServiceDateIsReady = c.DateTime(nullable: false),
                        TableId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        Client_ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.OrdersId)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.Tables", t => t.TableId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_ClientId)
                .Index(t => t.TableId)
                .Index(t => t.ServiceId)
                .Index(t => t.Client_ClientId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServicePrice = c.Double(nullable: false),
                        ServiceDateCreated = c.DateTime(nullable: false),
                        ServiceCategory_CategoryId = c.Int(),
                        ServiceSubCategory_SubCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ServiceId)
                .ForeignKey("dbo.ServiceCategories", t => t.ServiceCategory_CategoryId)
                .ForeignKey("dbo.ServiceSubCategories", t => t.ServiceSubCategory_SubCategoryId)
                .Index(t => t.ServiceCategory_CategoryId)
                .Index(t => t.ServiceSubCategory_SubCategoryId);
            
            CreateTable(
                "dbo.ServiceCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.ServiceSubCategories",
                c => new
                    {
                        SubCategoryId = c.Int(nullable: false, identity: true),
                        SubCategoryName = c.String(),
                        SubNonCategoryId = c.Int(nullable: false),
                        SubNonCategoryName = c.String(),
                        Category_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.SubCategoryId)
                .ForeignKey("dbo.ServiceCategories", t => t.Category_CategoryId)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        TableId = c.Int(nullable: false, identity: true),
                        TableName = c.String(),
                    })
                .PrimaryKey(t => t.TableId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        RegisteredDate = c.DateTime(nullable: false),
                        LastLogin = c.DateTime(nullable: false),
                        UserType = c.String(),
                        Tables_TableId = c.Int(),
                        Orders_OrdersId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Tables", t => t.Tables_TableId)
                .ForeignKey("dbo.Orders", t => t.Orders_OrdersId)
                .Index(t => t.Tables_TableId)
                .Index(t => t.Orders_OrdersId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Client_ClientId", "dbo.Clients");
            DropForeignKey("dbo.Users", "Orders_OrdersId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "TableId", "dbo.Tables");
            DropForeignKey("dbo.Users", "Tables_TableId", "dbo.Tables");
            DropForeignKey("dbo.Orders", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Services", "ServiceSubCategory_SubCategoryId", "dbo.ServiceSubCategories");
            DropForeignKey("dbo.ServiceSubCategories", "Category_CategoryId", "dbo.ServiceCategories");
            DropForeignKey("dbo.Services", "ServiceCategory_CategoryId", "dbo.ServiceCategories");
            DropIndex("dbo.Users", new[] { "Orders_OrdersId" });
            DropIndex("dbo.Users", new[] { "Tables_TableId" });
            DropIndex("dbo.ServiceSubCategories", new[] { "Category_CategoryId" });
            DropIndex("dbo.Services", new[] { "ServiceSubCategory_SubCategoryId" });
            DropIndex("dbo.Services", new[] { "ServiceCategory_CategoryId" });
            DropIndex("dbo.Orders", new[] { "Client_ClientId" });
            DropIndex("dbo.Orders", new[] { "ServiceId" });
            DropIndex("dbo.Orders", new[] { "TableId" });
            DropTable("dbo.Users");
            DropTable("dbo.Tables");
            DropTable("dbo.ServiceSubCategories");
            DropTable("dbo.ServiceCategories");
            DropTable("dbo.Services");
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
        }
    }
}
