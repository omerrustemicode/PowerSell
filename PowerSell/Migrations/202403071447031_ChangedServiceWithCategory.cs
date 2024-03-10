namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedServiceWithCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Services", "ServiceCategory_CategoryId", "dbo.ServiceCategories");
            DropIndex("dbo.Services", new[] { "ServiceCategory_CategoryId" });
            AddColumn("dbo.ServiceCategories", "ServiceId", c => c.Int());
            CreateIndex("dbo.ServiceCategories", "ServiceId");
            AddForeignKey("dbo.ServiceCategories", "ServiceId", "dbo.Services", "ServiceId");
            DropColumn("dbo.Services", "ServiceCategory_CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "ServiceCategory_CategoryId", c => c.Int());
            DropForeignKey("dbo.ServiceCategories", "ServiceId", "dbo.Services");
            DropIndex("dbo.ServiceCategories", new[] { "ServiceId" });
            DropColumn("dbo.ServiceCategories", "ServiceId");
            CreateIndex("dbo.Services", "ServiceCategory_CategoryId");
            AddForeignKey("dbo.Services", "ServiceCategory_CategoryId", "dbo.ServiceCategories", "CategoryId");
        }
    }
}
