using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bug_Boss.Models
{
    public class Project
    {
        public Project()
        {
            Tickets = new HashSet<Ticket>();
            ApplicationUsers = new HashSet<ApplicationUser> ();
        }

            public string Name { get; set; }
            public int Id { get; set; }
            public DateTimeOffset Created { get; set; }
            public Nullable<DateTimeOffset> Updated { get; set; }

            public virtual ICollection<Ticket> Tickets { get; set; }
            public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    
    }

    [Bind(Exclude="PossibleUsersToAssign,PossibleUsersToRemove")]
    public class ProjectViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public Nullable<DateTimeOffset> Updated { get; set; }

        [Display(Name = "Possible Users To Assign")]
        public System.Web.Mvc.MultiSelectList PossibleUsersToAssign { get; set; }
        public string[] NewlyAssignedUsers { get; set; }
        [Display(Name = "Possible Users To Remove")]
        public System.Web.Mvc.MultiSelectList PossibleUsersToRemove { get; set; }
        public string[] NewlyRemovedUsers { get; set; }
    }
}


