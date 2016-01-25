namespace Person_Phone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Phones", "UserId", "dbo.Users");
            DropIndex("dbo.Phones", new[] { "UserId" });
            AlterColumn("dbo.Phones", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Phones", "UserId");
            AddForeignKey("dbo.Phones", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phones", "UserId", "dbo.Users");
            DropIndex("dbo.Phones", new[] { "UserId" });
            AlterColumn("dbo.Phones", "UserId", c => c.Int());
            CreateIndex("dbo.Phones", "UserId");
            AddForeignKey("dbo.Phones", "UserId", "dbo.Users", "Id");
        }
    }
}
