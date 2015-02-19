namespace DAL.MiamiJobsFinderMigrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.MiamiJobsFinderDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"MiamiJobsFinderMigrations";
        }

        protected override void Seed(DAL.MiamiJobsFinderDb context)
        {
        }
    }
}
