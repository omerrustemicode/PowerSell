namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25032024OrderList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderLists",
                c => new
                    {
                        OrderListId = c.Int(nullable: false, identity: true),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Message = c.String(),
                        Transport = c.String(),
                        ClientName = c.String(),
                    })
                .PrimaryKey(t => t.OrderListId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderLists");
        }
    }
}
