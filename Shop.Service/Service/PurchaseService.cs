using Shop.Data.Models;
using Shop.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Shop.Service.Service
{
    public class PurchaseService : IPurchaseService
    {
        IUserRepository _user;
        IPurchaseRepository _purchase;
        IProductPurchaseRepository _productPurchase;
        public PurchaseService(IUserRepository user, IPurchaseRepository purchase, IProductPurchaseRepository productPurchase)
        {
            _purchase = purchase;
            _user = user;
            _productPurchase = productPurchase;
        }

        public void BuyOneItem(ProductPurchase productPurchase)
        {
            _productPurchase.Add(productPurchase);
            _productPurchase.Commit();
        }

        public void CreatePurchase(Purchase purchase)
        {
            _purchase.Add(purchase);
            _purchase.Commit();
        }

        public void CreateUser(User user)
        {
            _user.Add(user);
            _user.Commit();
        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
            return _purchase.GetAll();
        }

        public Purchase GetPurchase(int purchaseId)
        {
            return _purchase.IncludeAll().Where(p => p.Id == purchaseId).FirstOrDefault();
        }

        public IEnumerable<Purchase> GetPurchases(Expression<Func<Purchase, bool>> where)
        {
            var p = _purchase.IncludeAll().Where(where.Compile());
            return p;
        }
    }

    public interface IPurchaseService
    {
        void CreateUser(User user);
        void CreatePurchase(Purchase purchase);
        void BuyOneItem(ProductPurchase productPurchase);
        IEnumerable<Purchase> GetAllPurchases();
        IEnumerable<Purchase> GetPurchases(Expression<Func<Purchase, bool>> where);
        Purchase GetPurchase(int purchaseId);
    }
}
