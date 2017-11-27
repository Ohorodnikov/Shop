using Shop.Service.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Service.Service
{
    public class PurchaseService : IPurchaseService
    {
        IUserRepository _user;
        IPurchaseRepository _purchase;

        public PurchaseService(IUserRepository user, IPurchaseRepository purchase)
        {
            _purchase = purchase;
            _user = user;
        }
    }

    public interface IPurchaseService
    {

    }
}
