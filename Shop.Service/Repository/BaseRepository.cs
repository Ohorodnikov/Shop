using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Shop.Service.Repository
{
    public abstract class BaseRepository<T, C> where T: class where C:DbContext
    {
        protected readonly DbSet<T> _dbset;
        private C _dataContext;

        protected BaseRepository(C dataContext)
        {
            _dataContext = dataContext;
            _dbset = dataContext.Set<T>();
        }
        
        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            _dbset.RemoveRange(_dbset.Where(where));
        }

        public virtual T GetById(int id)
        {
            return _dbset.Find(id);
        }

        public virtual T GetById(long id)
        {
            return _dbset.Find(id);
        }

        public virtual T GetById(string id)
        {
            return _dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }

        public virtual IQueryable<T> GetAllQ()
        {
            return _dbset;
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where);
        }

        public virtual IQueryable<T> GetManyQ(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault();
        }

        public IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbset;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IEnumerable<T> AllIncludingReadonly(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbset.AsNoTracking();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void Commit()
        {
            _dataContext.SaveChanges();
        }
    }

    public interface IBaseRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(int Id);
        T GetById(long Id);
        T GetById(string Id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        IQueryable<T> GetAllQ();
        IQueryable<T> GetManyQ(Expression<Func<T, bool>> where);
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        void Commit();
    }
}
