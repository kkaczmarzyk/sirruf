using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirruf.Models
{
    public class Shop
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
        //public ICollection<Comment> Comments { get; set; }
    }
}
