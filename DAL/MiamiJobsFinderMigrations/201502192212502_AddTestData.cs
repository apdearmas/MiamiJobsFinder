namespace DAL.MiamiJobsFinderMigrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddTestData : DbMigration
    {
        public override void Up()
        {
            AddPredefinedCustomers();
            AddPredefinedContacts();
            AddPredefinedLocations();
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
            Sql("INSERT INTO ContactPersons Values ('Anonymous', 'xxx@yahoo.com', '00000000000')");
        }

        private void AddPredefinedLocations()
        {
            Sql("INSERT INTO Locations Values ( 'Miami', 'Fl',331768)");
        }
    }
}
