namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _29032024OrderListIsReady : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderLists", "IsReady", c => c.Int());
            AddColumn("dbo.OrderLists", "Tables_TableId", c => c.Int());
            CreateIndex("dbo.OrderLists", "Tables_TableId");
            AddForeignKey("dbo.OrderLists", "Tables_TableId", "dbo.Tables", "TableId");
            DropColumn("dbo.Orders", "IsReady");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "IsReady", c => c.Int());
            DropForeignKey("dbo.OrderLists", "Tables_TableId", "dbo.Tables");
            DropIndex("dbo.OrderLists", new[] { "Tables_TableId" });
            DropColumn("dbo.OrderLists", "Tables_TableId");
            DropColumn("dbo.OrderLists", "IsReady");
        }
    }
}
