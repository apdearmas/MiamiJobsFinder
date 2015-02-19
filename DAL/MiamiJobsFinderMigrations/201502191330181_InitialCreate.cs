namespace DAL.MiamiJobsFinderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EMail = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EMail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobOffers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IssuedDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        ContactPerson_Id = c.Int(),
                        Location_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactPersons", t => t.ContactPerson_Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .Index(t => t.ContactPerson_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobOffers", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.JobOffers", "ContactPerson_Id", "dbo.ContactPersons");
            DropIndex("dbo.JobOffers", new[] { "Location_Id" });
            DropIndex("dbo.JobOffers", new[] { "ContactPerson_Id" });
            DropTable("dbo.Locations");
            DropTable("dbo.JobOffers");
            DropTable("dbo.Customers");
            DropTable("dbo.ContactPersons");
        }
    }
}
