namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReturnedProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReturnedProducts",
                c => new
                    {
                        RetProdId = c.Int(nullable: false, identity: true),
                        OrdersId = c.Int(nullable: false),
                        OrderListId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        ServicePrice = c.Int(nullable: false),
                        ServiceName = c.String(),
                        WorkerName = c.String(),
                        WorkerId = c.Int(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RetProdId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReturnedProducts");
        }
    }
}
