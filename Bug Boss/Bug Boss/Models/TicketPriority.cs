﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug_Boss.Models
{
    public class TicketPriority
    {
        public TicketPriority()
        {
            this.Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
