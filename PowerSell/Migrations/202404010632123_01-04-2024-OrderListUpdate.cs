namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01042024OrderListUpdate : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.OrderLists", name: "Tables_TableId", newName: "TableId");
            RenameIndex(table: "dbo.OrderLists", name: "IX_Tables_TableId", newName: "IX_TableId");
            AddColumn("dbo.OrderLists", "IsPaid", c => c.Boolean());
            AddColumn("dbo.OrderLists", "ClientGetService", c => c.Boolean());
            AddColumn("dbo.OrderLists", "ServiceDateCreated", c => c.DateTime());
            AddColumn("dbo.OrderLists", "ClientGetServiceDate", c => c.DateTime());
            AddColumn("dbo.OrderLists", "ServiceDateIsReady", c => c.DateTime());
            AddColumn("dbo.OrderLists", "ServiceDIscount", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Orders", "ServiceDIscount");
            DropColumn("dbo.Orders", "IsPaid");
            DropColumn("dbo.Orders", "ClientGetService");
            DropColumn("dbo.Orders", "ServiceDateCreated");
            DropColumn("dbo.Orders", "ClientGetServiceDate");
            DropColumn("dbo.Orders", "ServiceDateIsReady");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ServiceDateIsReady", c => c.DateTime());
            AddColumn("dbo.Orders", "ClientGetServiceDate", c => c.DateTime());
            AddColumn("dbo.Orders", "ServiceDateCreated", c => c.DateTime());
            AddColumn("dbo.Orders", "ClientGetService", c => c.Boolean());
            AddColumn("dbo.Orders", "IsPaid", c => c.Boolean());
            AddColumn("dbo.Orders", "ServiceDIscount", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.OrderLists", "ServiceDIscount");
            DropColumn("dbo.OrderLists", "ServiceDateIsReady");
            DropColumn("dbo.OrderLists", "ClientGetServiceDate");
            DropColumn("dbo.OrderLists", "ServiceDateCreated");
            DropColumn("dbo.OrderLists", "ClientGetService");
            DropColumn("dbo.OrderLists", "IsPaid");
            RenameIndex(table: "dbo.OrderLists", name: "IX_TableId", newName: "IX_Tables_TableId");
            RenameColumn(table: "dbo.OrderLists", name: "TableId", newName: "Tables_TableId");
        }
    }
}
