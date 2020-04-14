namespace E_market.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "category_id", c => c.Int());
            CreateIndex("dbo.Products", "category_id");
            AddForeignKey("dbo.Products", "category_id", "dbo.Categories", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "category_id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "category_id" });
            DropColumn("dbo.Products", "category_id");
        }
    }
}
