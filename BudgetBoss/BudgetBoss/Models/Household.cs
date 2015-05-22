using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetBoss.Models
{
    public class Household
    {
        public Household() {
            this.Invitations = new HashSet<Invitation>();
            this.Users = new HashSet<ApplicationUser>();
            this.HouseholdAccounts = new HashSet<HouseholdAccount>();
            this.BudgetItems = new HashSet<BudgetItem>();
            this.Categories = new HashSet<Category>();
        }

        public int? Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set;} 
        public virtual ICollection<HouseholdAccount> HouseholdAccounts { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}