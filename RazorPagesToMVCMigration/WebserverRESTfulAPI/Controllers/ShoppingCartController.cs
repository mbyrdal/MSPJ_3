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
            // Retrieve ShoppingCart from a session, or create a new one if it does not exist yet
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(Product product)
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();

            // Check if a Product already exists in the ShoppingCart instance
            var existingProduct = cart.Items.FirstOrDefault(i => i.ID == ID);

            if(existingProduct != null)
            {
                // CASE: Increment and/or handle duplicate Products
            }
            
            cart.Items.Add(product);

            // Update the total Sale price
            cart.TotalPrice = cart.Items.Sum(i => i.Price);

            // Store the updated cart to the current session
            HttpContext.Session.SetObjectAsJSON(CartSessionKey, cart);

            // Status 302: Redirects to Index()
            return RedirectToAction(nameof(Index));
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

        }    
    }
}
