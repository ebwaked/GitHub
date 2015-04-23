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
        public string doors { get; set; }
        public string engine_fuel { get; set; }
        public string fuel_capacity_l { get; set; }
        public string engine_type { get; set; }
        public string engine_num_cyl { get; set; }
        public string top_speed_kph { get; set; }
        public string zero_100_kph { get; set; }
        public string drive_type { get; set; }
        public string transmission_type { get; set; }
        public string seats { get; set; }
        public string weight_kg { get; set; }
        public string sold_in_us { get; set; }
    }

    public class carViewModel
    {
        public Cars Car { get; set; }
        public string Recalls {get;set;}
        public string Image {get;set;}
    }
}