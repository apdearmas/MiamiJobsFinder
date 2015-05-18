using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.IdentityMigrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.IdentityDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"IdentityMigrations";
        }

        protected override void Seed(IdentityDb context)
        {
            if (!context.Users.Any(u => u.UserName == "elukse@yahoo.com") ||
                !context.Users.Any(u => u.UserName == "ruben.gimenez@aol.com"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var roleStore = new RoleStore<IdentityRole>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                if (!context.Roles.Any(r => r.Name == "admin"))
                {
                    roleManager.Create(new IdentityRole
                    {
                        Name = "admin"
                    });
                }

                if (!context.Users.Any(u => u.UserName == "elukse@yahoo.com"))
                {
                    var liz = new ApplicationUser
                    {
                        UserName = "elukse@yahoo.com",
                        Email = "elukse@yahoo.com",
                        EmailConfirmed = true
                    };
                    userManager.Create(liz, "password@123");
                    userManager.AddToRole(liz.Id, "admin");

                }
                if (!context.Users.Any(u => u.UserName == "ruben.gimenez@aol.com"))
                {
                    var ruben = new ApplicationUser
                    {
                        UserName = "ruben.gimenez@aol.com",
                        Email = "ruben.gimenez@aol.com",
                        EmailConfirmed = true
                    };
                    userManager.Create(ruben, "rubengimenez");
                    userManager.AddToRole(ruben.Id, "admin");

                }

            }

        }
    }
}
