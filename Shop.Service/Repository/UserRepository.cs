using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Service.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {

    }

    public class UserRepository : BaseRepository<User, ShopContext>, IUserRepository
    {
        public UserRepository(ShopContext dataContext) : base(dataContext)
        {
        }
    }
}
