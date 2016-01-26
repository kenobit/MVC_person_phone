namespace Person_Phone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FromVM : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PhoneViewModels", newName: "Phones");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Phones", newName: "PhoneViewModels");
        }
    }
}
