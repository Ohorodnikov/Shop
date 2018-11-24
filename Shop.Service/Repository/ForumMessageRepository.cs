using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Service.Repository
{
    public interface IForumMessageRepository : IBaseRepository<ForumMessages>
    {
        IEnumerable<ForumMessages> IncludeAll();
    }
    public class ForumMessageRepository : BaseRepository<ForumMessages, ShopContext>, IForumMessageRepository
    {
        ShopContext _dataContext;
        public ForumMessageRepository(ShopContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<ForumMessages> IncludeAll()
        {
            return _dataContext.ForumMessages.Include(m => m.User).Include(m => m.Answers);
        }
    }
}
