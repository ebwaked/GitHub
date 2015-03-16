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

           // //
           // // The Big Lebowski Code 
           // //

            var userId = userManager.FindByEmail("admin@coderfoundry.com").Id;

            var projects = new List<Project>
           {
                new Project { Name = "The Big Lebowski" },
                new Project { Name = "Peterkin" },
                new Project { Name = "Ferris" },
                new Project { Name = "Newman" },
                new Project { Name = "Kramer" }
           };
            if (!context.Projects.Any(r => r.Name == "The Big Lebowski"))
            {
                projects.ForEach(p => context.Projects.Add(p));
                context.SaveChanges();
            }

            var typeId = context.TicketTypes.Single(p => p.Name == "Bug").Id;
            var statusId = context.TicketStatuses.Single(p => p.Name == "In progress").Id;
            var project = context.Projects.Single(p => p.Name == "The Big Lebowski").Id;
            var project2 = context.Projects.Single(p => p.Name == "Peterkin").Id;
            var project3 = context.Projects.Single(p => p.Name == "Ferris").Id;
            var project4 = context.Projects.Single(p => p.Name == "Newman").Id;
            var project5 = context.Projects.Single(p => p.Name == "Kramer").Id;
            if (!context.ProjectUsers.Any(r => r.UserId == userId))
            {
                context.ProjectUsers.Add(new ProjectUser()
                {
                    ProjectId = project,
                    UserId = userId
                });
            }
            var tickets = new List<Ticket>
                {

                new Ticket {
                    Title = "Search is broken",
                    Description = "The search never returns results",
                    Created = System.DateTimeOffset.Now,
                    ProjectId = project,
                    TicketTypeId = typeId,
                    TicketStatusId = statusId,
                    OwnerUserId = userId,
                    AssignedUserId = userId
                },
                new Ticket {
                    Title = "Can't attach a file to a ticket",
                    Description = "I get an error undefined everytinme",
                    Created = System.DateTimeOffset.Now,
                    ProjectId = project2,
                    TicketTypeId = typeId,
                    TicketStatusId = statusId,
                    OwnerUserId = userId,
                    AssignedUserId = userId
                },
                new Ticket {
                    Title = "Can't reassign a ticket",
                    Description = "The drop down of users doesn't populate",
                    Created = System.DateTimeOffset.Now,
                    ProjectId = project3,
                    TicketTypeId = typeId,
                    TicketStatusId = statusId,
                    OwnerUserId = userId,
                    AssignedUserId = userId
                },
                new Ticket {
                    Title = "Can't change status of a ticket",
                    Description = "Error every time",
                    Created = System.DateTimeOffset.Now,
                    ProjectId = project4,
                    TicketTypeId = typeId,
                    TicketStatusId = statusId,
                    OwnerUserId = userId,
                    AssignedUserId = userId
                },
                new Ticket {
                    Title = "Can't create a new project",
                    Description = "Validation error",
                    Created = System.DateTimeOffset.Now,
                    ProjectId = project5,
                    TicketTypeId = typeId,
                    TicketStatusId = statusId,
                    OwnerUserId = userId,
                    AssignedUserId = userId
                },
                new Ticket {
                    Title = "Can't assign users to a ticket",
                    Description = "Drop down list doesn't populate",
                    Created = System.DateTimeOffset.Now,
                    ProjectId = project5,
                    TicketTypeId = typeId,
                    TicketStatusId = statusId,
                    OwnerUserId = userId,
                    AssignedUserId = userId
                },
                new Ticket {
                    Title = "Sorting of rows not working",
                    Description = "When you click on a row nothing happens",
                    Created = System.DateTimeOffset.Now,
                    ProjectId = project4,
                    TicketTypeId = typeId,
                    TicketStatusId = statusId,
                    OwnerUserId = userId,
                    AssignedUserId = userId
                },
                new Ticket {
                    Title = "Create new ticket",
                    Description = "Need a textarea for description",
                    Created = System.DateTimeOffset.Now,
                    ProjectId = project3,
                    TicketTypeId = typeId,
                    TicketStatusId = statusId,
                    OwnerUserId = userId,
                    AssignedUserId = userId
                },
                new Ticket {
                    Title = "Timestamps are editable",
                    Description = "Really? How convenient",
                    Created = System.DateTimeOffset.Now,
                    ProjectId = project2,
                    TicketTypeId = typeId,
                    TicketStatusId = statusId,
                    OwnerUserId = userId,
                    AssignedUserId = userId
                },
                new Ticket {
                    Title = "Save after editing broken",
                    Description = "More validation errors",
                    Created = System.DateTimeOffset.Now,
                    ProjectId = project,
                    TicketTypeId = typeId,
                    TicketStatusId = statusId,
                    OwnerUserId = userId,
                    AssignedUserId = userId
                    },
                };
            if (!context.Tickets.Any(r => r.ProjectId == project))
            {
                tickets.ForEach(t => context.Tickets.Add(t));
                context.SaveChanges();
            }

           // //
           // //  End of Big Lebowski Code
           // //

        }
        
    }
}
