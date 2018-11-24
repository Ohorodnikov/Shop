using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Service.Service;
using Shop.Data.Models;
using Shop.Service.Repository;
using Shop.Data;
using Shop.Extentions;
using Microsoft.AspNetCore.Http;
using Shop.PayPal;
using Shop.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace Shop.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/sampledata")]
    public class SampleDataController : Controller
    {
        IPurchaseService _purchase;
        IProductService _product;
        private ISession _session;

        const string basket = "basket";

        public SampleDataController(IProductService product, IPurchaseService purchase, 
            IHttpContextAccessor httpContextAccessor)
        {
            _product = product;
            _purchase = purchase;
            _session = httpContextAccessor.HttpContext.Session;
            //AddProductsToCategory(1);
            //AddProductsToCategory(2);
            //AddProductsToCategory(3);
            //AddProductsToCategory(4);
        }

        private void AddProductsToCategory(int catId)
        {
            string GetRandomName(int length)
            {
                string name = string.Empty;
                for (int i = 0; i < length; i++)
                {
                    name += (char)(new Random().Next(65, 90));
                }

                return name;
            }
            var count = new Random().Next(25, 75);
            for (int i = 0; i < count; i++)
            {
                _product.AddProduct(new Product
                {
                    CategoryId = catId,
                    Description = GetRandomName(new Random().Next(3, 20)),
                    Name = GetRandomName(new Random().Next(3, 20)),
                    Price = Math.Round(new Random().NextDouble() * 1000, 2)
                });
            }
        }

        [HttpGet, Route("getcomments")]
        public IEnumerable<CommentViewModel> GetComments(int productId)
        {
            //TestAddParentComment();

            var allComments = _product.GetComments(productId).ToList();
            var viewModel = new List<CommentViewModel>();
            foreach (var com in allComments)
            {                
                viewModel.Add(GenerateComment(com));
            }

            viewModel.RemoveAll(p => p == null);

            return viewModel;
        }
        
        private void TestAddParentComment()
        {
            var productId = 755;
            var message = "Reply Message 2";
            var parentId = 3;
            var userId = "ef4ff7a5-c300-4cc6-bc9f-348fe35ed2c6";
            _product.AddComment(productId, userId, DateTime.Now, message, parentId);
        }

        List<int> includedMessages = new List<int>();

        private CommentViewModel GenerateComment(ForumMessages message)
        {
            if (includedMessages.Contains(message.Id))
            {
                return null;
            }
            includedMessages.Add(message.Id);
            var comment = new CommentViewModel
            {
                User = message.User,
                Id = message.Id,
                Message = message.Message,
                PublishedDateTime = message.PublishedDateTime,
                UserId = message.User.Id,
                UserName = message.User.UserName
            };

            comment.User.Comments = null;
            comment.User = null;
            var answers = new List<CommentViewModel>();

            foreach (var ans in message.Answers)
            {
                if (includedMessages.Contains(ans.Id))
                {
                    continue;
                }
                answers.Add(GenerateComment(ans));
            }

            comment.Answers = answers;

            return comment;
        }

        [HttpGet, Route("addcomment")]
        public JsonResult AddComment(int productId, string message, int? parentId)
        {
            var currUser = _session.Get<User>(AdminController.UserKey);
            if (currUser != null)
            {
                var id = _product.AddComment(productId, currUser.Id, DateTime.Now, message, parentId);

                return Json(new {result = "ok", id });
            }
            return Json(new { result = "You should be registred!" });
        }

        [HttpPost]
        public ActionResult CreatePurchase(string nonce, string userName, double price)
        {
            new ClientTokenHandler().Pay(nonce, price);
            var products = _session.Get<List<ProductViewModel>>(basket);

            var user = new User
            {
                UserName = userName
            };
            _purchase.CreateUser(user);
            var purchase = new Purchase
            {
                DateTime = DateTime.Now,
                UserId = user.Id
            };
            _purchase.CreatePurchase(purchase);
            foreach (var p in products)
            {
                _purchase.BuyOneItem(new ProductPurchase
                {
                    ProductId = p.Id,
                    PurchaseId = purchase.Id,
                    Count = p.Count
                });
            }
            _session.Set(basket, new List<ProductViewModel>());
            return Json("qqq");
        }
        [HttpGet, MapToApiVersion("1.0")]
        [Route("AddProductToBasket")]
        public int AddProductToBasket(int id)
        {
            var products = _session.Get<List<ProductViewModel>>(basket);
            if (products == null)
            {
                products = new List<ProductViewModel>();
            }
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.Count++;
            }
            else
            {
                var dbProd = GetProduct(id);
                product = new ProductViewModel
                {
                    Category = dbProd.Category,
                    CategoryId = dbProd.CategoryId,
                    Count = 1,
                    Description = dbProd.Description,
                    Id = dbProd.Id,
                    Name = dbProd.Name,
                    Price = dbProd.Price
                };
                products.Add(product);
            }
            _session.Set(basket, products);
            return _session.Get<List<PartialViewResult>>(basket).Count;
        }

        [Route("RemoveProductOneItemFromBasket")]
        [HttpGet]
        public int RemoveProductOneItemFromBasket(int id)
        {
            var products = _session.Get<List<ProductViewModel>>(basket);
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                if (product.Count == 1)
                {
                    products.Remove(product);
                }
                else
                {
                    product.Count--;
                }
            }
            _session.Set(basket, products);
            return _session.Get<List<ProductViewModel>>(basket).Count;
        }

        [Route("RemoveProductAllItemsFromBasket")]
        [HttpGet]
        public int RemoveProductAllItemsFromBasket(int id)
        {
            var products = _session.Get<List<ProductViewModel>>(basket);
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
            _session.Set(basket, products);
            return _session.Get<List<ProductViewModel>>(basket).Count;
        }

        [Route("GetBasket")]
        public IEnumerable<ProductViewModel> GetBasket()
        {
            var x = _session.Get<List<ProductViewModel>>(basket);
            return x;
        }

        [HttpGet]
        public string GetToken()
        {
            var x = new ClientTokenHandler().GetServerToken();
            return x;
        }

        [HttpGet, MapToApiVersion("1.0")]
        [Route("categories")]
        public IEnumerable<Category> Categories()
        {
            return _product.GetCategories();
        }

        [HttpGet, MapToApiVersion("2.0")]
        [Route("categories")]
        public IEnumerable<Category> CategoriesV2()
        {
            var categories = _product.GetCategories().ToList();
            foreach (var category in categories)
            {
                var list = _product.GetProducts(category.Id).ToList();
                foreach (var item in list)
                {
                    item.Category = null;
                }
                category.Products = list;
            }
            return categories;
        }

        [HttpGet, MapToApiVersion("1.0")]
        [Route("products")]
        public IEnumerable<Product> Products(int categoryId, int page, int itemsPerPage)
        {
            return _product.GetProducts(categoryId, page, itemsPerPage);
        }

        [HttpGet, MapToApiVersion("1.0")]
        [Route("search")]
        public IEnumerable<Product> Search(string criteria, int page, int itemsPerPage)
        {
            return _product.GetProducts(p => p.Name.Contains(criteria) || p.Description.Contains(criteria), page, itemsPerPage);
        }


        [HttpGet, MapToApiVersion("1.0")]
        [Route("getproduct")]
        public Product GetProduct(int productId)
        {
            var x = _product.GetProduct(productId);
            return x;
        }

        

    }
}
