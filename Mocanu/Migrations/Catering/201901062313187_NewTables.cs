namespace Mocanu.Migrations.Catering
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FoodOrderViews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodName = c.String(),
                        ImageLink = c.String(),
                        FoodIngredients = c.String(),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        FoodType = c.String(),
                        NumberInOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionToFoods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodName = c.String(maxLength: 128),
                        TransactionId = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Foods", t => t.FoodName)
                .ForeignKey("dbo.Transactions", t => t.TransactionId, cascadeDelete: true)
                .Index(t => t.FoodName)
                .Index(t => t.TransactionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionToFoods", "TransactionId", "dbo.Transactions");
            DropForeignKey("dbo.TransactionToFoods", "FoodName", "dbo.Foods");
            DropIndex("dbo.TransactionToFoods", new[] { "TransactionId" });
            DropIndex("dbo.TransactionToFoods", new[] { "FoodName" });
            DropTable("dbo.TransactionToFoods");
            DropTable("dbo.FoodOrderViews");
        }
    }
}
