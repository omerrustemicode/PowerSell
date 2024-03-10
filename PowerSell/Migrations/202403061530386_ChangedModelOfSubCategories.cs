namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedModelOfSubCategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceSubCategories", "Category_CategoryId", "dbo.ServiceCategories");
            DropForeignKey("dbo.Services", "ServiceSubCategory_SubCategoryId", "dbo.ServiceSubCategories");
            DropIndex("dbo.Services", new[] { "ServiceSubCategory_SubCategoryId" });
            DropIndex("dbo.ServiceSubCategories", new[] { "Category_CategoryId" });
            AddColumn("dbo.ServiceCategories", "CategoryParentId", c => c.Int());
            CreateIndex("dbo.ServiceCategories", "CategoryParentId");
            AddForeignKey("dbo.ServiceCategories", "CategoryParentId", "dbo.ServiceCategories", "CategoryId");
            DropColumn("dbo.Services", "ServiceSubCategory_SubCategoryId");
            DropTable("dbo.ServiceSubCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceSubCategories",
                c => new
                    {
                        SubCategoryId = c.Int(nullable: false, identity: true),
                        SubCategoryName = c.String(),
                        SubNonCategoryId = c.Int(nullable: false),
                        SubNonCategoryName = c.String(),
                        Category_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.SubCategoryId);
            
            AddColumn("dbo.Services", "ServiceSubCategory_SubCategoryId", c => c.Int());
            DropForeignKey("dbo.ServiceCategories", "CategoryParentId", "dbo.ServiceCategories");
            DropIndex("dbo.ServiceCategories", new[] { "CategoryParentId" });
            DropColumn("dbo.ServiceCategories", "CategoryParentId");
            CreateIndex("dbo.ServiceSubCategories", "Category_CategoryId");
            CreateIndex("dbo.Services", "ServiceSubCategory_SubCategoryId");
            AddForeignKey("dbo.Services", "ServiceSubCategory_SubCategoryId", "dbo.ServiceSubCategories", "SubCategoryId");
            AddForeignKey("dbo.ServiceSubCategories", "Category_CategoryId", "dbo.ServiceCategories", "CategoryId");
        }
    }
}
