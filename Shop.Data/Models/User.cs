using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
    }
}
