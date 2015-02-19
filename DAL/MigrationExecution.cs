using System.Data.Entity.Migrations;

namespace DAL
{
    public class MigrationExecution
    {
        public static void MigrateDatabaseToLatest()
        {
            var identityConfiguration = new IdentityMigrations.Configuration();
            var identityMigrator = new DbMigrator(identityConfiguration);

            identityMigrator.Update();

            var miamiJobsFinderConfiguration = new MiamiJobsFinderMigrations.Configuration();
            var miamiJobsFinderMigrator = new DbMigrator(miamiJobsFinderConfiguration);

            miamiJobsFinderMigrator.Update();

        }
    }
}
