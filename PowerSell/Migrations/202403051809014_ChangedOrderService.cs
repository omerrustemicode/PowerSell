namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedOrderService : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "ServiceName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ServiceName", c => c.String());
        }
    }
}
