namespace E_market.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateforiegnkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "category_id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "category_id" });
            RenameColumn(table: "dbo.Products", name: "category_id", newName: "Categoryid");
            AlterColumn("dbo.Products", "Categoryid", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "Categoryid");
            AddForeignKey("dbo.Products", "Categoryid", "dbo.Categories", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Categoryid", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "Categoryid" });
            AlterColumn("dbo.Products", "Categoryid", c => c.Int());
            RenameColumn(table: "dbo.Products", name: "Categoryid", newName: "category_id");
            CreateIndex("dbo.Products", "category_id");
            AddForeignKey("dbo.Products", "category_id", "dbo.Categories", "id");
        }
    }
}
