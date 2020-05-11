using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_market.Models
{
    public class Cart
    {
        [Key]
        [ForeignKey("Product")]
        public int product_id { get; set; }
        public DateTime added_at { get; set; }
        public virtual Product Product { get; set; }
        public string calculated(DateTime added)
        {
            DateTime current = DateTime.Today;
            TimeSpan diff = current.Subtract(added);
            int time = diff.Hours;
            if (time < 1) { time = diff.Minutes; return time.ToString() + "Minute"; }
            else if (time >= 1 && time < 24) { return time.ToString() + "hours"; }
            else if (time >= 24 && time < 168) { time = diff.Days; return time.ToString() + "day"; }
            else { time = time / 168; return time.ToString() + "week"; }


        }
    }
}