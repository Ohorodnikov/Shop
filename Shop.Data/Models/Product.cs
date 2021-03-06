﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ProductPurchase> ProductPurchase { get; set; } = new List<ProductPurchase>();

        public ICollection<ForumMessages> Comments { get; set; } = new List<ForumMessages>();
    }
}
