using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.ViewModel
{
    public class ProductViewModel : Product
    {
        public int Count { get; set; }
    }
}
