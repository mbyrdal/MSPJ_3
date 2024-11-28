using Microsoft.AspNetCore.Mvc;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System.Linq;

namespace ServiceAPI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private const string CartSessionKey = "Cart";

        private readonly DbProduct _dbProduct;

        public ShoppingCartController(DbProduct dbProduct)
        {
            _dbProduct = dbProduct;
        }

        // Display the shopping cart
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();
            return View(cart);
        }



        // Add a product to the cart
        [HttpPost]
        public IActionResult AddToCart(int productID)
        {
            var product = _dbProduct.GetByIdentifier(productID);
            // Assuming _dbContext is injected to access your database

            if (product != null && product.ItemAvailable == true)
            {
                var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();

                // Check if product is already in cart
                var existingProduct = cart.Items.FirstOrDefault(i => i.ID == product.ID);
                if (existingProduct == null)
                {
                    cart.Items.Add(product); // Add product to cart
                    product.ItemAvailable = false;
                    _dbProduct.UpdateEntity(product); // Update product availability in database
                }

                // Update the total price
                cart.TotalPrice = cart.Items.Sum(i => i.Price);

                // Save updated cart to session
                HttpContext.Session.SetObjectAsJSON(CartSessionKey, cart);
            }

            return RedirectToAction("Index", "ShoppingCart"); // Redirect to the cart page or wherever you need
        }


        // Remove a product from the cart
        [HttpDelete]
        public JsonResult RemoveFromCart(int productID)
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey);

            if (cart?.Items != null)
            {
                var productToRemove = cart.Items.FirstOrDefault(i => i.ID == productID);
                if (productToRemove != null)
                {
                    cart.Items.Remove(productToRemove);
                    cart.TotalPrice = cart.Items.Sum(i => i.Price);
                    HttpContext.Session.SetObjectAsJSON(CartSessionKey, cart);
                }
            }

            return Json(new { success = true, message = "Product removed from cart", cart });
        }

        // Clear the cart
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CartSessionKey);
            return RedirectToAction(nameof(Index));
        }

        // Checkout
        public IActionResult CheckOut()
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey);

            if (cart != null && cart.Items?.Count > 0)
            {
                // Handle order processing here (e.g., save to database)

                // After processing, clear the cart
                HttpContext.Session.Remove(CartSessionKey);

                return RedirectToAction("OrderConfirmation");
            }

            TempData["Error"] = "Your shopping cart is empty!";
            return RedirectToAction(nameof(Index));
        }

        // Order confirmation page
        public IActionResult OrderConfirmation()
        {
            return View();
        }
    }
}
