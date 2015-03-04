using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bug_Boss.Models
{
    //public class Project
    //{
    //    public Project()
    //    {
    //        this.ProjectUsers = new HashSet<ProjectUser>();
    //        this.Tickets = new HashSet<Ticket>();
    //    }
    //    public int Id { get; set; }
    //    public string Name { get; set; }

    //    public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
    //    public virtual ICollection<Ticket> Tickets { get; set; }
    //}

    public class Project
    {
        public Project()
        {
            Tickets = new HashSet<Ticket>();
            ApplicationUsers = new HashSet<ApplicationUser> ();
        }

            public string Name { get; set; }
            public int Id { get; set; }

            public virtual ICollection<Ticket> Tickets { get; set; }
            public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    
    }

    [Bind(Exclude="PossibleUsersToAssign,PossibleUsersToRemove")]
    public class ProjectViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public MultiSelectList PossibleUsersToAssign { get; set; }
        public string[] NewlyAssignedUsers { get; set; }
        public MultiSelectList PossibleUsersToRemove { get; set; }
        public string[] NewlyRemovedUsers { get; set; }
    }
}


