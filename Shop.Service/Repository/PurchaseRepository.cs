using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace Shop.Service.Repository
{
    public interface IPurchaseRepository : IBaseRepository<Purchase>
    {
        IEnumerable<Purchase> IncludeAll();
    }
    public class PurchaseRepository : BaseRepository<Purchase, ShopContext>, IPurchaseRepository
    {
        private ShopContext _context;
        public PurchaseRepository(ShopContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }

        public IEnumerable<Purchase> IncludeAll()
        {
            return _context.Purchase
                .Include(p => p.ProductPurchase)
                    .ThenInclude(p => p.Product)
                .Include(p => p.User);
        }
    }
}
