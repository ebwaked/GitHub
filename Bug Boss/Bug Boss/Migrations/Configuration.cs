using Bug_Boss.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Bug_Boss.Migrations
{

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
            if (!roleManager.RoleExists("Project Manager"))
            {
                roleManager.Create(new IdentityRole("Project Manager"));
            }
            if (!roleManager.RoleExists("Developer"))
            {
                roleManager.Create(new IdentityRole("Developer"));
            }
            if (!roleManager.RoleExists("Submitter"))
            {
                roleManager.Create(new IdentityRole("Submitter"));
            }

            var user = userManager.FindByName("admin@bugboss.com");
            if (user == null)
            {
                user = new ApplicationUser { 
                    UserName = "admin@bugboss.com", 
                    Email = "admin@bugboss.com",
                    FirstName = "Admin",
                    LastName = "BugBoss",
                    DisplayName = "Admin"
 
                };
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

            if (!context.Users.Any(u => u.Email == "projectmanager@bugboss.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var demoUser = new ApplicationUser
                {
                    UserName = "projectmanager@bugboss.com",
                    Email = "projectmanager@bugboss.com",
                    FirstName = "Project Manager",
                    LastName = "BugBoss",
                    DisplayName = "Project_Manager"
                };

                manager.Create(demoUser, "Password-1");
                manager.AddToRole(demoUser.Id, "Project Manager");
            }

            if (!context.Users.Any(u => u.Email == "developer@bugboss.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var demoUser = new ApplicationUser
                {
                    UserName = "developer@bugboss.com",
                    Email = "developer@bugboss.com",
                    FirstName = "Developer",
                    LastName = "BugBoss",
                    DisplayName = "Developer"
                };

                manager.Create(demoUser, "Password-1");
                manager.AddToRole(demoUser.Id, "Developer");
            }

            if (!context.Users.Any(u => u.Email == "submitter@bugboss.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var demoUser = new ApplicationUser
                {
                    UserName = "submitter@bugboss.com",
                    Email = "submitter@bugboss.com",
                    FirstName = "Submitter",
                    LastName = "BugBoss",
                    DisplayName = "Submitter"
                };

                manager.Create(demoUser, "Password-1");
                manager.AddToRole(demoUser.Id, "Submitter");
            }

            if (!context.TicketStatuses.Any(t=>t.Name == "New"))
            {
                context.TicketStatuses.Add(new TicketStatus { Name = "New" });
                context.SaveChanges();
            }
            if (!context.TicketStatuses.Any(t => t.Name == "In Progress"))
            {
                context.TicketStatuses.Add(new TicketStatus { Name = "In Progress" });
                context.SaveChanges();
            }
            if (!context.TicketStatuses.Any(t => t.Name == "Not Assigned"))
            {
                context.TicketStatuses.Add(new TicketStatus { Name = "Not Assigned" });
                context.SaveChanges();
            }
            if (!context.TicketStatuses.Any(t => t.Name == "Closed"))
            {
                context.TicketStatuses.Add(new TicketStatus { Name = "Closed" });
                context.SaveChanges();
            }

            if (!context.TicketPriorities.Any(t => t.Name == "High Priority"))
            {
                context.TicketPriorities.Add(new TicketPriority { Name = "High Priority" });
                context.SaveChanges();
            }
            if (!context.TicketPriorities.Any(t => t.Name == "Medium Priority"))
            {
                context.TicketPriorities.Add(new TicketPriority { Name = "Medium Priority" });
                context.SaveChanges();
            }
            if (!context.TicketPriorities.Any(t => t.Name == "Low Priority"))
            {
                context.TicketPriorities.Add(new TicketPriority { Name = "Low Priority" });
                context.SaveChanges();
            }

            if (!context.TicketTypes.Any(t => t.Name == "Bug"))
            {
                context.TicketTypes.Add(new TicketType { Name = "Bug" });
                context.SaveChanges();
            }
            if (!context.TicketTypes.Any(t => t.Name == "Feature Request"))
            {
                context.TicketTypes.Add(new TicketType { Name = "Feature Request" });
                context.SaveChanges();
            }
            if (!context.TicketTypes.Any(t => t.Name == "Improvement"))
            {
                context.TicketTypes.Add(new TicketType { Name = "Improvement" });
                context.SaveChanges();
            }
        }
        
    }
}
