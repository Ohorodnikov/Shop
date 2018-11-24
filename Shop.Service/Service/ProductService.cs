using Shop.Data.Models;
using Shop.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Shop.Service.Service
{
    public class ProductService : IProductService
    {
        IProductRepository _product;
        ICategoryRepository _category;
        IForumMessageRepository _forumMessage;

        public ProductService(IForumMessageRepository forumMessage, IProductRepository product, ICategoryRepository category)
        {
            _category = category;
            _product = product;
            _forumMessage = forumMessage;
        }

        public void AddProduct(Product product)
        {
            _product.Add(product);
            _product.Commit();
        }

        public Product GetProduct(int id)
        {
            return _product.GetById(id);
        }

        public IEnumerable<Product> GetProducts(int categoryId)
        {
            return _product.GetMany(p => p.CategoryId == categoryId);
        }

        public IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> where)
        {
            return _product.GetMany(where);
        }

        public void RemoveProduct(Product product)
        {
            _product.Delete(product);
            _product.Commit();
        }

        public void RemoveProduct(int productId)
        {
            _product.Delete(p => p.Id == productId);
            _product.Commit();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _category.GetAll();
        }

        public IQueryable<Product> GetProducts(int categoryId, int page, int itemsPerPage)
        {
            return _product.GetManyQ(p => p.CategoryId == categoryId)
                .Skip((page - 1) * itemsPerPage)
                .AsQueryable()
                .Take(itemsPerPage)
                .AsQueryable();            
        }

        public IQueryable<Product> GetProducts(Expression<Func<Product, bool>> where, int page, int itemsPerPage)
        {
            return _product.GetManyQ(where)
                .Skip((page - 1) * itemsPerPage)
                .AsQueryable()
                .Take(itemsPerPage)
                .AsQueryable();
        }

        public IEnumerable<ForumMessages> GetComments(int productId)
        {
            return _forumMessage.IncludeAll().Where(m => m.ProductId == productId);
        }

        public int AddComment(int productId, string userId, DateTime dateTime, string message, int? parentComment = null)
        {
            var msg = new ForumMessages
            {
                Message = message,
                UserId = userId,
                ProductId = productId,
                PublishedDateTime = dateTime,
                ReplyToId = parentComment
            };
            _forumMessage.Add(msg);
            _forumMessage.Commit();

            return msg.Id;
        }        
    }

    public interface IProductService
    {
        void AddProduct(Product product);
        Product GetProduct(int id);
        IEnumerable<Product> GetProducts(int categoryId);
        IQueryable<Product> GetProducts(int categoryId, int page, int itemsPerPage);
        IQueryable<Product> GetProducts(Expression<Func<Product, bool>> where, int page, int itemsPerPage);
        IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> where);
        void RemoveProduct(Product product);
        void RemoveProduct(int productId);
        IEnumerable<Category> GetCategories();

        IEnumerable<ForumMessages> GetComments(int productId);
        int AddComment(int productId, string userId, DateTime dateTime, string message, int? parentComment = null);
        
    }
}
