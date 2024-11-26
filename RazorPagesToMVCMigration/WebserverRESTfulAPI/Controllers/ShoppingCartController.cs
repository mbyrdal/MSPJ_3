using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceAPI.Models;
using System.Linq;

namespace ServiceAPI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private const string CartCookieKey = "Cart";

        public IActionResult Index()
        {
            // Retrieve ShoppingCart from cookies, or create a new one if it does not exist yet
            var cart = GetCartFromCookies() ?? new ShoppingCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(Product product)
        {
            var cart = GetCartFromCookies() ?? new ShoppingCart();

            // Add the product directly (no Quantity management)
            cart.Items.Add(product);

            // Update the total sale price
            cart.TotalPrice = cart.Items.Sum(i => i.Price);

            // Store the updated cart in cookies
            SaveCartToCookies(cart);

            // Redirect to Index (302)
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productID)
        {
            var cart = GetCartFromCookies();

            if (cart?.Items != null)
            {
                // Remove the first occurrence of the product with the matching ID
                var productToRemove = cart.Items.FirstOrDefault(i => i.ID == productID);
                if (productToRemove != null)
                {
                    cart.Items.Remove(productToRemove);

                    cart.TotalPrice = cart.Items.Sum(i => i.Price);

                    // Store the updated cart in cookies
                    SaveCartToCookies(cart);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ClearCart()
        {
            // Remove the cart cookie
            Response.Cookies.Delete(CartCookieKey);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CheckOut()
        {
            var cart = GetCartFromCookies();

            if (cart != null && cart.Items != null && cart.Items.Count > 0)
            {
                // Process checkout logic here (e.g., save order to DB)

                // Clear cart after checkout
                Response.Cookies.Delete(CartCookieKey);

                return RedirectToAction("OrderConfirmation");
            }

            // If cart is empty, redirect to cart page with an error
            TempData["Error"] = "Your shopping cart is empty!";
            return RedirectToAction(nameof(Index));
        }

        // Display an order confirmation page
        public IActionResult OrderConfirmation()
        {
            return View();
        }

        // Helper: Get cart from cookies
        private ShoppingCart GetCartFromCookies()
        {
            if (Request.Cookies.TryGetValue(CartCookieKey, out var cartJson))
            {
                return JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
            }
            return null;
        }

        // Helper: Save cart to cookies
        private void SaveCartToCookies(ShoppingCart cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            Response.Cookies.Append(CartCookieKey, cartJson, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7), // Cookie expires in 7 days
                HttpOnly = true,
                IsEssential = true
            });
        }
    }
}
