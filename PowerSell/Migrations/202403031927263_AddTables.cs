namespace PowerSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddTables : DbMigration
    {
        public override void Up()
        {
            for (int i = 1; i <= 100; i++)
            {
                CreateTable(
                    $"dbo.Table{i}",
                    c => new
                    {
                        TableId = c.Int(nullable: false, identity: true),
                        TableName = c.String(),
                    })
                    .PrimaryKey(t => t.TableId);
            }
        }

        public override void Down()
        {
            for (int i = 1; i <= 100; i++)
            {
                DropTable($"dbo.Table{i}");
            }
        }
    }
}
