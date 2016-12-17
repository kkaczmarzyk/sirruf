using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirruf.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ApplicationUserID { get; set; }
        public Boolean IsPublic { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
        //public ICollection<Comment> Comments { get; set; }
    }
}
