using Shop.Data.Models;
using Shop.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Shop.Service.Service
{
    public class ProductService : IProductService
    {
        IProductRepository _product;
        ICategoryRepository _category;

        public ProductService(IProductRepository product, ICategoryRepository category)
        {
            _category = category;
            _product = product;
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
    }

    public interface IProductService
    {
        void AddProduct(Product product);
        Product GetProduct(int id);
        IEnumerable<Product> GetProducts(int categoryId);
        IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> where);
        void RemoveProduct(Product product);
        void RemoveProduct(int productId);
        IEnumerable<Category> GetCategories();
    }
}
