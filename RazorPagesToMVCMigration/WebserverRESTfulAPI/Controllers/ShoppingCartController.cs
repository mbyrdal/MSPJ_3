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
            var existingProduct = cart.Items.FirstOrDefault(i => i.ID == product.ID);

            if(existingProduct != null)
            {
                // CASE: Increment and/or handle duplicate Products
            }
            
            cart.Items.Add(product);

            // Update the total Sale price
            cart.TotalPrice = cart.Items.Sum(i => i.Price);

            // Store the updated cart to the current session (using SessionHelper utility method)
            HttpContext.Session.SetObjectAsJSON(CartSessionKey, cart);

            // Status 302: Redirects to Index()
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult RemoveFromCart(int productID)
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey);

            if(cart?.Items != null)
            {
                var productToRemove = cart.Items.FirstOrDefault(i => i.ID == productID);
                if(productToRemove != null)
                {
                    cart.Items.Remove(productToRemove);

                    cart.TotalPrice = cart.Items.Sum(i => i.Price);

                    // Using SessionHelper utility method
                    HttpContext.Session.SetObjectAsJSON(CartSessionKey, cart);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CartSessionKey);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CheckOut()
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey);
            
            if(cart != null && cart.Items != null && cart.Items.Count > 0)
            {
                // Process checkout logic here (e.g., save order to DB)

                // Clear cart after checkout
                HttpContext.Session.Remove(CartSessionKey);

                return RedirectToAction("OrderConfirmation");
            }

            // If cart is empty, redirect to cart page with an error
            TempData["Error"] = "Your shopping cart is empty!";
            return RedirectToAction(nameof(Index));
        }
        
        // Optional: Display an order confirmation page
        public IActionResult OrderConfirmation()
        {
            return View();
        }
    }
}
