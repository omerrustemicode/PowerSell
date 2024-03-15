namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TotalPriceAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Services", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Services", "ServicePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Services", "ServicePrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Services", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
