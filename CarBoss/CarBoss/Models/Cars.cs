using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Insight.Database;

namespace CarBoss.Models
{
    public class Cars
    {
        public int id { get; set; }
        [Column("model_year")]
        public string year { get; set; }
        [Column("make")]
        public string make { get; set; }
        [Column("model_name")]
        public string model { get; set; }
        [Column("model_trim")]
        public string trim { get; set; }
        public string body_style { get; set; }
    }
}