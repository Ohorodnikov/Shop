using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Service.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {

    }

    public class CategoryRepository : BaseRepository<Category, ShopContext>, ICategoryRepository
    {
        public CategoryRepository(ShopContext dataContext) : base(dataContext)
        {
        }
    }
}
