using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tu_Tu.Model.Entities
{
    public class UserBadges
    {
        public User_TuTU User { get; set; }
        public Badge Badge { get; set; }

        [Key, Column(Order = 1)]
        public int userId { get; set; }

        [Key, Column(Order = 2)]
        public string badgeID { get; set; }

        public int count { get; set; }
    }
}
