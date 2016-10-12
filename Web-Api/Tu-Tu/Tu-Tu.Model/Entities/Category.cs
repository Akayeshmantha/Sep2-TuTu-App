using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tu_Tu.Model.Entities
{
    public class Category
    {
        [Key]
        public string catId { get; set; }
        public string name { get; set; }
        public IList<Activity> Activities { get; set; }
    }
}
