namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedOrderList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderLists", "ClientId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderLists", "ClientId");
        }
    }
}
