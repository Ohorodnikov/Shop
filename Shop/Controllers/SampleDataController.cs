using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Service.Service;
using Shop.Data.Models;
using Shop.Service.Repository;
using Shop.Data;

namespace Shop.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        IPurchaseService _purchase;
        IProductService _product;

        public SampleDataController(IProductService product, IPurchaseService purchase)
        {
            _product = product;
            _purchase = purchase;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet]
        public IEnumerable<Category> Categories()
        {
            return _product.GetCategories();
        }

        [HttpGet]
        public IEnumerable<Product> Products(int categoryId)
        {
            return _product.GetProducts(categoryId);
        }
        

        [HttpGet]
        public Product GetProduct(int productId)
        {
            var x = _product.GetProduct(productId);
            return x;
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
