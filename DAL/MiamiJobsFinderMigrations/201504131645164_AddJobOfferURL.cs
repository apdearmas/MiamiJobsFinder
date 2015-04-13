namespace DAL.MiamiJobsFinderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobOfferURL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobOffers", "JobOfferURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobOffers", "JobOfferURL");
        }
    }
}
