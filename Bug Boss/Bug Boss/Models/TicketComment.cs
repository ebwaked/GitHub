﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug_Boss.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public System.DateTimeOffset Created { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
