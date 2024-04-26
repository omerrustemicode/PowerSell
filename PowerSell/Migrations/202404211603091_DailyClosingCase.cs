namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DailyClosingCase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyClosingCases",
                c => new
                    {
                        OrderListId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Workers = c.String(),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderListId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DailyClosingCases");
        }
    }
}
