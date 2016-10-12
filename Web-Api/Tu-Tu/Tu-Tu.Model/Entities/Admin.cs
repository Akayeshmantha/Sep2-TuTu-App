using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tu_Tu.Model.Entities
{
    public class Admin
    {
        [Key]
        public string companyName { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public IList<User_TuTU> Users { get; set; }
    }
}
