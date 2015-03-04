namespace Bug_Boss.Migrations
{
    using Bug_Boss.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bug_Boss.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Bug_Boss.Models.ApplicationDbContext";
        }

        protected override void Seed(Bug_Boss.Models.ApplicationDbContext context)
        {
            
        
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Administrator"))
            {
                roleManager.Create(new IdentityRole("Administrator"));
            }
            if (!roleManager.RoleExists("User"))
            {
                roleManager.Create(new IdentityRole("User"));
            }

            var user = userManager.FindByName("admin@coderfoundry.com");
            if (user == null)
            {
                user = new ApplicationUser { UserName = "admin@coderfoundry.com", Email = "admin@coderfoundry.com" };
                var result = userManager.Create(user, "Password-1");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrator");
                }
            }
            else
            {
                if (!userManager.IsInRole(user.Id, "Administrator"))
                {
                    userManager.AddToRole(user.Id, "Administrator");
                }
            }

            if (!context.TicketStatuses.Any(t=>t.Name == "New"))
            {
                context.TicketStatuses.Add(new TicketStatus { Name = "New" });
            }
            if (!context.TicketPriorities.Any(t => t.Name == "New"))
            {
                context.TicketPriorities.Add(new TicketPriority { Name = "New" });
            }
            if (!context.TicketTypes.Any(t => t.Name == "New"))
            {
                context.TicketTypes.Add(new TicketType { Name = "New" });
            }
        }
        
    }
}
