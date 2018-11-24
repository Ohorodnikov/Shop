using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Service.Repository
{
    public interface IProductPurchaseRepository : IBaseRepository<ProductPurchase>
    {

    }
    public class ProductPurchaseRepository : BaseRepository<ProductPurchase, ShopContext>, IProductPurchaseRepository
    {
        public ProductPurchaseRepository(ShopContext dataContext) : base(dataContext)
        {

        }
    }
}
