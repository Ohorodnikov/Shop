using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data
{
    public class PurchaseViewModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public User User { get; set; }
        public double TotalPrice { get; set; }
        public IEnumerable<ProductPurchase> Products { get; set; }
    }
}
