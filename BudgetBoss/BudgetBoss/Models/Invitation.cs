﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetBoss.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        public string ToEmail { get; set; }
        public int HouseholdId { get; set; }
        public string Code { get; set; }

        public virtual Household Household { get; set; }
    }
}