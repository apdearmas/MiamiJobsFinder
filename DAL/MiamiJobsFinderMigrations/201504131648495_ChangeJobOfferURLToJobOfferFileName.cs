namespace DAL.MiamiJobsFinderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeJobOfferURLToJobOfferFileName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobOffers", "JobOfferFileName", c => c.String());
            DropColumn("dbo.JobOffers", "JobOfferURL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobOffers", "JobOfferURL", c => c.String());
            DropColumn("dbo.JobOffers", "JobOfferFileName");
        }
    }
}
