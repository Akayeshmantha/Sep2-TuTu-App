using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tu_Tu.Model.Entities
{
    public class Badge
    {
        [Key]
        public string bId { get; set; }
        public string name { get; set; }
        public IList<UserBadges> userBadges { get; set; }
    }
}
