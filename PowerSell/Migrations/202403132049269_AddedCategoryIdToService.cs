namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategoryIdToService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "CategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "CategoryId");
        }
    }
}
