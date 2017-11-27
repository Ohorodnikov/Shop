using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Service.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {

    }
    public class ProductRepository : BaseRepository<Product, ShopContext>, IProductRepository
    {
        public ProductRepository(ShopContext dataContext) : base(dataContext)
        {
        }
    }
}
