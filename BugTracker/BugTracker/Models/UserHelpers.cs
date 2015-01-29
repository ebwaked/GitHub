using System.Data.Entity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTracker.Models
{
    public class UserRolesHelper
    {
        private UserManager<ApplicationUser> manager = 
            new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(
                    new ApplicationDbContext()));

        public bool IsUserInRole(string userId, string roleName)
        {
            var result = manager.IsInRole(userId, roleName);
            return result;
        }

        public IList<string> ListUserRoles(string userId)
        {
           return manager.GetRoles(userId);
        }

        public bool AddUserToRoles(string userId, string roleName)
        {
            var result = manager.AddToRole(userId, roleName);
            return result.Succeeded;
        }

        public bool RemoveUserFromRole(string userId, string roleName)
        {
            var result = manager.RemoveFromRole(userId, roleName);
            return result.Succeeded;
        }

        public IList<ApplicationUser> UsersInRole(string roleName)
        {
            
            var resultList = new List<ApplicationUser>();
            foreach(var user in manager.Users)
            {
                if (IsUserInRole(user.Id, roleName))
                {
                    resultList.Add(user);
                }
            }
            return resultList;
        }

        public IList<ApplicationUser> UsersNotInRole(string roleName)
        {
            
            var resultList = new List<ApplicationUser>();
            foreach(var user in manager.Users)
            {
                if (!IsUserInRole(user.Id, roleName))
                {
                    resultList.Add(user);
                }
            }
            return resultList;
        }
    }

    public class UserProjectsHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<bool> IsOnProject(string userId, int projectId)
        {
            if (await db.ProjectUsers.AnyAsync(p => p.ProjectId == projectId && p.UserId == userId))
            {
                return true;
            }
            return false;
        }

        public async Task AddUserToProject(string userId, int projectId)
        {
            if (!await this.IsOnProject(userId, projectId))
            {
                // add a ProjectUsers entry for this user and project
                var pu = new ProjectUser {ProjectId = projectId, UserId = userId};
                db.ProjectUsers.Add(pu);
                db.SaveChanges();
            }
        }

        public async Task RemoveUserFromProject(string userId, int projectId)
        {
            if (await this.IsOnProject(userId, projectId))
            {
                // remove the ProjectUsers entry for this user and project
                var pu = db.ProjectUsers.SingleAsync(p => p.ProjectId == projectId && p.UserId == userId);
                db.ProjectUsers.Remove(pu.Result);
                db.SaveChanges();
            }
        }

        /// <summary>
        ///  Gets a list of all assigned to the specified project.
        /// </summary>
        /// <param name="projectId">The integer Id of the specified proeject.</param>
        /// <returns>Returns a list of ApplicationUser objects assigned to the project.</returns>
        public async Task<IList<ApplicationUser>>UsersOnProject(int projectId)
        {
            //  add users on the project
            var userList = new List<ApplicationUser>();
            
            foreach (var user in db.Users)
            {
                if (await this.IsOnProject(user.Id, projectId))
                {
                    userList.Add(user);
                }
            }
            return userList;
        }

        public async Task<IList<ApplicationUser>>UsersOnProject(int projectId, string roleName)
        {
            //  add users on the project same as method above except if this one is overloaded with the string RoleName which if I call both parameters it will send me this information
            var userList = new List<ApplicationUser>();
            var rolesHelper = new UserRolesHelper();
            
            foreach (var user in db.Users)
            {
                if ((await this.IsOnProject(user.Id, projectId)) &&
                (rolesHelper.IsUserInRole(user.Id, roleName)))
                {
                    userList.Add(user);
                }
            }
            return userList;
        }

        public async Task<IList<ApplicationUser>>UsersNotOnProject(int projectId)
        {
            //  add users on the project
            var userList = new List<ApplicationUser>();
            
            foreach (var user in db.Users)
            {
                if (!await this.IsOnProject(user.Id, projectId))
                {
                    userList.Add(user);
                }
            }
            return userList;
        }

        public async Task<IList<ApplicationUser>>UsersNotOnProject(int projectId, string roleName)
        {
            //  add users on the project same as method above except if this one is overloaded with the string RoleName which if I call both parameters it will send me this information
            var userList = new List<ApplicationUser>();
            var rolesHelper = new UserRolesHelper();
            
            foreach (var user in db.Users)
            {
                if ((!await this.IsOnProject(user.Id, projectId)) &&
                (rolesHelper.IsUserInRole(user.Id, roleName)))
                {
                    userList.Add(user);
                }
            }
            return userList;
        }
        
        //  Start of the method that gives a list of users
        public async Task<IList<Project>>ListUserProjects(string userId)        
        {
            // list the projects for the user
            var projectList = new List<Project>();
            
            foreach (var project in db.Projects)
            {
                if (await this.IsOnProject(userId, project.Id)) 
                {
                    projectList.Add(project);
                }
            }
            return projectList;
            
        }
    }
}