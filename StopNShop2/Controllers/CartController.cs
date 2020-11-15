using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Web;
using StopNShop2.Models;

namespace StopNShop2.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }

        /*[HttpPost]
        public IActionResult Buy(int id)
        {
            const string sessionKey = "Cart";
            var value = HttpContext.Session.GetString(sessionKey);
            ProductModel productModel = new ProductModel();
            if (string.IsNullOrEmpty(value))
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = productModel.find(id), Quantity = 1 });
                
            }
        }*/
    }
}
