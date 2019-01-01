namespace Mocanu.Migrations.Catering
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        Address = c.String(maxLength: 50),
                        UserScore = c.Int(nullable: false),
                        CNP = c.String(maxLength: 100),
                        ID_Card_Series = c.String(maxLength: 2),
                        ID_Card_Number = c.String(maxLength: 6),
                        FirstName = c.String(),
                        LastName = c.String(),
                        TelephoneNumber = c.String(),
                        Password = c.String(),
                        isSuspended = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.String(maxLength: 128),
                        Email = c.String(),
                        TotalCost = c.Int(nullable: false),
                        Address = c.String(),
                        ID_Card_Series = c.String(),
                        ID_Card_Number = c.String(),
                        Date = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        FoodName = c.String(nullable: false, maxLength: 128),
                        ImageLink = c.String(),
                        FoodIngredients = c.String(),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        FoodType = c.String(),
                        Transaction_Id = c.Int(),
                    })
                .PrimaryKey(t => t.FoodName)
                .ForeignKey("dbo.Transactions", t => t.Transaction_Id)
                .Index(t => t.Transaction_Id);
            
            CreateTable(
                "dbo.CurrentOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodName = c.String(),
                        Price = c.Int(nullable: false),
                        NumberInOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FoodtoFoodIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodName = c.String(maxLength: 128),
                        IngredientId = c.String(),
                        Ingredient_FoodIngredientId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Foods", t => t.FoodName)
                .ForeignKey("dbo.FoodIngredients", t => t.Ingredient_FoodIngredientId)
                .Index(t => t.FoodName)
                .Index(t => t.Ingredient_FoodIngredientId);
            
            CreateTable(
                "dbo.FoodIngredients",
                c => new
                    {
                        FoodIngredientId = c.String(nullable: false, maxLength: 128),
                        Ingredient = c.String(),
                    })
                .PrimaryKey(t => t.FoodIngredientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FoodtoFoodIngredients", "Ingredient_FoodIngredientId", "dbo.FoodIngredients");
            DropForeignKey("dbo.FoodtoFoodIngredients", "FoodName", "dbo.Foods");
            DropForeignKey("dbo.Transactions", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Foods", "Transaction_Id", "dbo.Transactions");
            DropIndex("dbo.FoodtoFoodIngredients", new[] { "Ingredient_FoodIngredientId" });
            DropIndex("dbo.FoodtoFoodIngredients", new[] { "FoodName" });
            DropIndex("dbo.Foods", new[] { "Transaction_Id" });
            DropIndex("dbo.Transactions", new[] { "ClientId" });
            DropTable("dbo.FoodIngredients");
            DropTable("dbo.FoodtoFoodIngredients");
            DropTable("dbo.CurrentOrders");
            DropTable("dbo.Foods");
            DropTable("dbo.Transactions");
            DropTable("dbo.Clients");
        }
    }
}
