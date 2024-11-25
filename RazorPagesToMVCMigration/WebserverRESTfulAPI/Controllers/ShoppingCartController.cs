using Microsoft.AspNetCore.Mvc;
using ServiceAPI.Models;
using ServiceAPI.Utilities;

namespace ServiceAPI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private const string CartSessionKey = "Cart";

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(Product product)
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();

            var existingProduct = cart.Items.FirstOrDefault(i => i.Id == Id);

            if(existingProduct != null)
            {

            }
        }

        [HttpDelete]
        public IActionResult RemoveFromCart(string productID)
        {

        }

        public IActionResult ClearCart()
        {

        }

        public IActionResult CheckOut()
        {

        }    }
}
