namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedServicesAddedVirtual : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceCategories", "ServiceId", "dbo.Services");
            AddColumn("dbo.Services", "Category_CategoryId", c => c.Int());
            AddColumn("dbo.ServiceCategories", "Service_ServiceId", c => c.Int());
            CreateIndex("dbo.Services", "Category_CategoryId");
            CreateIndex("dbo.ServiceCategories", "Service_ServiceId");
            AddForeignKey("dbo.Services", "Category_CategoryId", "dbo.ServiceCategories", "CategoryId");
            AddForeignKey("dbo.ServiceCategories", "Service_ServiceId", "dbo.Services", "ServiceId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceCategories", "Service_ServiceId", "dbo.Services");
            DropForeignKey("dbo.Services", "Category_CategoryId", "dbo.ServiceCategories");
            DropIndex("dbo.ServiceCategories", new[] { "Service_ServiceId" });
            DropIndex("dbo.Services", new[] { "Category_CategoryId" });
            DropColumn("dbo.ServiceCategories", "Service_ServiceId");
            DropColumn("dbo.Services", "Category_CategoryId");
            AddForeignKey("dbo.ServiceCategories", "ServiceId", "dbo.Services", "ServiceId");
        }
    }
}
