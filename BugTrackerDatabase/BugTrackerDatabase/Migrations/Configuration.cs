namespace BugTrackerDatabase.Migrations
{
    using BugTrackerDatabase.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTrackerDatabase.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BugTrackerDatabase.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "ProjectManager" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Developer" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Submitter" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.Email == "ebwaked87@gmail.com"))
	        {
		        var store = new UserStore<ApplicationUser>(context);
		        var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "ebwaked87@gmail.com",
                    Email = "ebwaked87@gmail.com",
                    FirstName = "Edward",
                    LastName = "Waked",
                    DisplayName = "Eddie",
                    
                };
                }
                // create a new ApplicationUser object (AspNetUsers entry)
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                if (!context.Users.Any(u => u.Email == "ebwaked87@gmail.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        UserName = "ebwaked87@gmail.com",
                        Email = "ebwaked87@gmail.com",
                    }, "Password-1");
                }
                var userId = userManager.FindByEmail("ebwaked87@gmail.com").Id;
                userManager.AddToRole(userId, "Admin");

                // create a new BTUsers entry to match the AspNetUsers entry
                var bt = new BTEntities();
                if (!bt.BtUsers.Any(u => u.AspNetUserId == userId))
                {
                        var btUser = new BtUser();
                        btUser.AspNetUserId = userId;
                        btUser.FirstName = "Eddie";
                        btUser.LastName = "Waked";
                        btUser.Email = "ebwaked87@gmail.com";
                        bt.BtUsers.Add(btUser);
                        bt.SaveChanges();
                    
                
                };
                    //  This method will be called after migrating to the latest version.

                    //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
                    //  to avoid creating duplicate seed data. E.g.
                    //
                    //    context.People.AddOrUpdate(
                    //      p => p.FullName,
                    //      new Person { FullName = "Andrew Peters" },
                    //      new Person { FullName = "Brice Lambson" },
                    //      new Person { FullName = "Rowan Miller" }
                    //    );
                    //
                ;
        } 
    }
}
