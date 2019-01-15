namespace Mocanu.Migrations.Catering
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditNew111 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginLogsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(),
                        UserRole = c.String(),
                        LoginDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoginLogsModels");
        }
    }
}
