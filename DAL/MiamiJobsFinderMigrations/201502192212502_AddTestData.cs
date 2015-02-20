namespace DAL.MiamiJobsFinderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestData : DbMigration
    {
        public override void Up()
        {
            AddPredefinedCustomers();
            AddPredefinedContacts();
            AddPredefinedLocations();
            AddPredefinedJobOffers();
            UpdateJobOffers();
        }
        
        public override void Down()
        {
        }

        private void AddPredefinedCustomers()
        {
            Sql("INSERT INTO Customers Values ('Antonio Pereira', 'apdermas@yahoo.com')");
            Sql("INSERT INTO Customers Values ('Frank Gongora', 'fgongora67@yahoo.com')");
        }

        private void AddPredefinedContacts()
        {
            Sql("INSERT INTO ContactPersons Values ('Empleador_1', 'xxx@yahoo.com', '00000000000')");
            Sql("INSERT INTO ContactPersons Values ('Empleador_2', 'xxx@yahoo.com', '00000000000')");
        }

        private void AddPredefinedLocations()
        {
            Sql("INSERT INTO Locations Values ( 'Miami', 'Fl',331768)");
            Sql("INSERT INTO Locations Values ( 'Hialeah', 'Fl',33018)");
        }

        private void AddPredefinedJobOffers()
        {
            for (int i = 0; i < 1000; i++)
            {
                Sql(string.Format("INSERT INTO JobOffers Values ('Mechanico','01/01/2015', '01/07/2015', 'Job Offer {0}',1,1)", i));
            }
        }

        private void UpdateJobOffers()
        {
            Sql(string.Format("UPDATE JobOffers SET [Title] = 'Mecanico' , [Location_Id] = 1 WHERE Id BETWEEN {0} AND {1}", 1, 299));
            Sql(string.Format("UPDATE JobOffers SET [Title] = 'Doctor'   , [Location_Id] = 1 WHERE Id BETWEEN {0} AND {1}", 300, 699));
            Sql(string.Format("UPDATE JobOffers SET [Title] = 'Maestro'  , [Location_Id] = 1 WHERE Id BETWEEN {0} AND {1}", 700, 1000));
        }
    }
}
