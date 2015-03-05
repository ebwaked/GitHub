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

            //
            // The Big Lebowski Code 
            //

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

              var types = new List<TicketType>
            {
            new TicketType { Name = "Bug" },
            new TicketType { Name = "Featue request" },
            new TicketType { Name = "Improvement" }
            };
            if (!context.TicketTypes.Any(r => r.Name == "Bug"))
            {
                types.ForEach(t => context.TicketTypes.Add(t));
                context.SaveChanges();
            }
            var status = new List<TicketStatus>
            {
            new TicketStatus { Name = "In progress" },
            new TicketStatus { Name = "Not assigned" },
            new TicketStatus { Name = "Closed" }
            };
                            if (!context.TicketStatuses.Any(r => r.Name == "Not assigned"))
                            {
                                status.ForEach(s => context.TicketStatuses.Add(s));
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

            //
            //  End of Big Lebowski Code
            //

        }
        
    }
}
