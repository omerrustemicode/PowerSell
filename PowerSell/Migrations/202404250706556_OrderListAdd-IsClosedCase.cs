namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderListAddIsClosedCase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderLists", "IsClosedCase", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderLists", "IsClosedCase");
        }
    }
}
