using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Service.Repository
{
    public interface IPurchaseRepository : IBaseRepository<Purchase>
    {

    }
    public class PurchaseRepository : BaseRepository<Purchase, ShopContext>, IPurchaseRepository
    {
        public PurchaseRepository(ShopContext dataContext) : base(dataContext)
        {
        }
    }
}
