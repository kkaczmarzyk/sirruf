using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Sirruf.Models
{
    public class Purchase
    {
        public int ID { get; set; }
        public string ApplicationUserID { get; set; }
        public int CategoryID { get; set; }
        public int ShopID { get; set; }
        public int BrandID { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }
        public Boolean IsPublic { get; set; }

        [Display(Name = "Purchase Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public Shop Shop { get; set; }
        public Brand Brand { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Category Category { get; set; }
        //public ICollection<Comment> Comments { get; set; }
    }
}
