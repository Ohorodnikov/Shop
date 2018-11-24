using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public ICollection<ProductPurchase> ProductPurchase { get; set; } = new List<ProductPurchase>();


        public string UserId { get; set; }
        public User User { get; set; }
    }
}
