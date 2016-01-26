namespace Person_Phone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Phones", newName: "PhoneViewModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PhoneViewModels", newName: "Phones");
        }
    }
}
