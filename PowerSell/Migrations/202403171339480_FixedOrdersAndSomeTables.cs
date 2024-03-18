namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedOrdersAndSomeTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Orders", "TableId", "dbo.Tables");
            DropIndex("dbo.Orders", new[] { "TableId" });
            DropIndex("dbo.Orders", new[] { "ServiceId" });
            RenameColumn(table: "dbo.Orders", name: "Client_ClientId", newName: "ClientId");
            RenameIndex(table: "dbo.Orders", name: "IX_Client_ClientId", newName: "IX_ClientId");
            AlterColumn("dbo.Orders", "ServiceDIscount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "IsPaid", c => c.Boolean());
            AlterColumn("dbo.Orders", "IsReady", c => c.Int());
            AlterColumn("dbo.Orders", "ClientGetService", c => c.Boolean());
            AlterColumn("dbo.Orders", "ServiceDateCreated", c => c.DateTime());
            AlterColumn("dbo.Orders", "ClientGetServiceDate", c => c.DateTime());
            AlterColumn("dbo.Orders", "ServiceDateIsReady", c => c.DateTime());
            AlterColumn("dbo.Orders", "TableId", c => c.Int());
            AlterColumn("dbo.Orders", "ServiceId", c => c.Int());
            CreateIndex("dbo.Orders", "TableId");
            CreateIndex("dbo.Orders", "ServiceId");
            AddForeignKey("dbo.Orders", "ServiceId", "dbo.Services", "ServiceId");
            AddForeignKey("dbo.Orders", "TableId", "dbo.Tables", "TableId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "TableId", "dbo.Tables");
            DropForeignKey("dbo.Orders", "ServiceId", "dbo.Services");
            DropIndex("dbo.Orders", new[] { "ServiceId" });
            DropIndex("dbo.Orders", new[] { "TableId" });
            AlterColumn("dbo.Orders", "ServiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "TableId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "ServiceDateIsReady", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "ClientGetServiceDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "ServiceDateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "ClientGetService", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Orders", "IsReady", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "IsPaid", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Orders", "ServiceDIscount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            RenameIndex(table: "dbo.Orders", name: "IX_ClientId", newName: "IX_Client_ClientId");
            RenameColumn(table: "dbo.Orders", name: "ClientId", newName: "Client_ClientId");
            CreateIndex("dbo.Orders", "ServiceId");
            CreateIndex("dbo.Orders", "TableId");
            AddForeignKey("dbo.Orders", "TableId", "dbo.Tables", "TableId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ServiceId", "dbo.Services", "ServiceId", cascadeDelete: true);
        }
    }
}
