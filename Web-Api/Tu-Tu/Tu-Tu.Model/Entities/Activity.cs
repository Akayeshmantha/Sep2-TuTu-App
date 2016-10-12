using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tu_Tu.Model.Entities
{
    public class Activity
    {
        [Key]
        public string actId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string categoryId { get; set; }
        public DateTime date { get; set; }
        public DateTime expiryDate { get; set; }
        public string attachemts { get; set; }
        public int assigner { set; get; }
        public int assignee { set; get; }
        public string status { get; set; }
        public int completeness { get; set; }
        public int starCount { get; set; }
    }
}
