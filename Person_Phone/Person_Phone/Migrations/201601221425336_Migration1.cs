namespace Person_Phone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Phones", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Phones", "PhoneType", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.Users", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Phones", "PhoneType", c => c.String(maxLength: 35));
            AlterColumn("dbo.Phones", "PhoneNumber", c => c.String(maxLength: 30));
        }
    }
}
