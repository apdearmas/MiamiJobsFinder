namespace DAL.MiamiJobsFinderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDataAnnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactPersons", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ContactPersons", "EMail", c => c.String(nullable: false));
            AlterColumn("dbo.ContactPersons", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "EMail", c => c.String(nullable: false));
            AlterColumn("dbo.JobOffers", "Title", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobOffers", "Title", c => c.String());
            AlterColumn("dbo.Customers", "EMail", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
            AlterColumn("dbo.ContactPersons", "PhoneNumber", c => c.String());
            AlterColumn("dbo.ContactPersons", "EMail", c => c.String());
            AlterColumn("dbo.ContactPersons", "Name", c => c.String());
        }
    }
}
