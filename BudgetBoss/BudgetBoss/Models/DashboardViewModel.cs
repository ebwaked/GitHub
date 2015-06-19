using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetBoss.Models
{
    

    public class DashboardViewModel
    {
        public IEnumerable<HouseholdAccount> HouseholdAccounts { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}