namespace MVC_Resume_MIO.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MVC_Resume_MIO.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC_Resume_MIO.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MVC_Resume_MIO.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
	        {
		        var store = new RoleStore<IdentityRole>(context);
		        var manager = new RoleManager<IdentityRole>(store);
		        var role = new IdentityRole { Name = "Admin" };
		
		        manager.Create(role);
	        }
	
	        if (!context.Roles.Any(r => r.Name == "Moderator"))
	        {
		        var store = new RoleStore<IdentityRole>(context);
		        var manager = new RoleManager<IdentityRole>(store);
		        var role = new IdentityRole { Name = "Moderator" };
		
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
			        DisplayName = "Eddie" 
		        };
		
		        manager.Create(user, "1187-Waked");
		        // manager,AddToRole(user.Id, "Admin");
		        //manager.AddToRole(user.Id, "Moderator");
		        manager.AddToRoles(user.Id, new string[] { "Admin", "Moderator"});
			
	        }

            if (!context.Users.Any(u => u.Email == "admin123@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "admin123@gmail.com",
                    Email = "admin123@gmail.com",
                    FirstName = "Test",
                    LastName = "Administrator",
                    DisplayName = "Admin Tester"
                };

                manager.Create(user, "Abc123!");
                // manager,AddToRole(user.Id, "Admin");
                //manager.AddToRole(user.Id, "Moderator");
                manager.AddToRoles(user.Id, new string[] { "Admin", "Moderator" });

            }
	
	        if (!context.Users.Any(u => u.Email == "bdavis@coderfoundry.com"))
	        {
		        var store = new UserStore<ApplicationUser>(context);
		        var manager = new UserManager<ApplicationUser>(store);
		        var user = new ApplicationUser 
		        {
			        UserName = "bdavis@coderfoundry.com",
			        Email = "bdavis@coderfoundry.com",
			        FirstName = "Bobby",
			        LastName = "Davis",
			        DisplayName = "Bobby" 
		        };
		
		        manager.Create(user, "Password-1");
		        // manager,AddToRole(user.Id, "Admin")
		        manager.AddToRole(user.Id, "Moderator");
		        //manager.AddToRoles(user.Id, new string[] { "Admin, "Moderator});-->
			
	        }
        }
    }
}
