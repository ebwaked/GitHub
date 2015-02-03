namespace BugTracker.Migrations
{
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BugTracker.Models.ApplicationDbContext";
        }

        protected override void Seed(BugTracker.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
                if (!context.Roles.Any(r => r.Name == "Admin"))
                {
                    roleManager.Create(new IdentityRole {Name  = "Admin" });
                }
                if (!context.Roles.Any(r => r.Name == "Developer"))
                {
                    roleManager.Create(new IdentityRole { Name = "Developer" });
                }
                if (!context.Roles.Any(r => r.Name == "Submitter"))
                {
                    roleManager.Create(new IdentityRole { Name = "Submitter" });
                }
                var userManager = new UserManager<ApplicationUser>(
                            new UserStore<ApplicationUser>(context));
                if (!context.Users.Any(u => u.Email == "ebwaked87@gmail.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        UserName = "ebwaked87@gmail.com",
                        Email = "ebwaked87@gmail.com",
                        FirstName = "Edward",
                        LastName = "Waked",
                        DisplayName = "ebwaked"
                    }, "1187-Waked");
                }
                var userId = userManager.FindByEmail("ebwaked87@gmail.com").Id;
                userManager.AddToRole(userId, "Admin");
                
        }
    }
}
