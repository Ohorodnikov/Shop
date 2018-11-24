using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Models;
using Shop.Extentions;
using Shop.Infrastructure;
using Shop.Service.Service;
using Shop.ViewModel;

namespace Shop.Controllers
{
    [Produces("application/json")]
    [Route("api/Admin")]
    [ApiVersion("1.0")]
    //[Authorize]
    public class AdminController : Controller
    {
        readonly IPurchaseService _purchase;
        readonly IProductService _product;
        readonly ISession _session;
        readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        public const string UserKey = "USER";

        public AdminController(IProductService product, IPurchaseService purchase, 
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _product = product;
            _purchase = purchase;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _session = httpContextAccessor.HttpContext.Session;
            //var u = _userManager.FindByEmailAsync(@"email@email.com").GetAwaiter().GetResult();
            //_userManager.AddToRoleAsync(u, "Admin").GetAwaiter().GetResult();
            //AddAdmin();
            //InitRoles();
        }

        private void InitRoles()
        {
            try
            {
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();
            }
            catch (Exception)
            {

            }
            
        }

        private async void AddAdmin()
        {
            var user = new User
            {
                Email = @"email@email.com",
                UserName = @"email@email.com"
            };
            
            // добавляем пользователя
            var result = await _userManager.CreateAsync(user, @"1$Qazwsxedc");
            
           
                       
        }

        [HttpGet, MapToApiVersion("1.0")]
        [Route("register")]
        public async Task<bool> Register(string email, string password)
        {
            var user = new User
            {
                Email = email,
                UserName = email
            };
            
            var result = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, "User");
            return result.Succeeded;
        }

        [HttpGet, MapToApiVersion("1.0")]
        [Route("GetAllPurchases")]        
        public IEnumerable<PurchaseViewModel> GetAllPurchases(DateTime lastDate)
        {
            var user = _session.Get<User>(UserKey);
            if (user == null)
            {
                return null;
            }
            var viewModel = new List<PurchaseViewModel>();
            var purchases = _purchase.GetPurchases(p => p.DateTime >= lastDate);
            foreach (var purchase in purchases)
            {
                if (purchase.ProductPurchase.Count == 0)
                {
                    continue;
                }
                var model = new PurchaseViewModel
                {
                    DateTime = purchase.DateTime,
                    Id = purchase.Id,
                    User = purchase.User,

                    //Products = purchase.ProductPurchase,
                    TotalPrice = purchase.ProductPurchase.Sum(p => p.Count * p.Product.Price)
                };
                model.User.Purchases = null;
                viewModel.Add(model);
            }

            Excel.WriteData(viewModel, "sales");
            return viewModel;            
        }

        [HttpGet, MapToApiVersion("1.0")]
        [Route("GetPurchase")]
        public PurchaseViewModel GetPurchase(int purchaseId)
        {
            var purchase = _purchase.GetPurchase(purchaseId);
            var model = new PurchaseViewModel
            {
                DateTime = purchase.DateTime,
                Id = purchase.Id,
                User = purchase.User,
                Products = purchase.ProductPurchase,
                TotalPrice = purchase.ProductPurchase.Sum(p => p.Count * p.Product.Price)
            };
            foreach (var p in model.Products)
            {
                p.Purchase = null;
                p.Product.ProductPurchase = null;
            }
            model.User.Purchases = null;
            return model;
        }

        //private async Task<User> GetCurrentUser()
        //{
        //    return await _userManager.GetUserAsync(HttpContext.User);
        //}

        [HttpGet, MapToApiVersion("1.0")]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IList<string>> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, true, false);           
            
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(email);
                _session.Set(UserKey, user);
                var roles = await _userManager.GetRolesAsync(user);
                return roles;
               
            }           
            return new List<string>();
        }
    }
}