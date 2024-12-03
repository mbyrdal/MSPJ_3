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
            var product = _dbProduct.GetByIdentifier(productID); // Retrieve the product from the database

            if (product != null && product.ItemAvailable == true)
            {
                var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();

                // Check if the product is already in the cart
                var existingProduct = cart.Items.FirstOrDefault(i => i.ID == product.ID);
                if (existingProduct == null)
                {
                    cart.Items.Add(product); // Add product to cart

                    // Update product availability
                    product.ItemAvailable = false;

                    // Convert Product to ProductViewModel for updating
                    var productViewModel = new Product
                    {
                        ID = product.ID,
                        OEM = product.OEM,
                        Price = product.Price,
                        DateAvailable = product.DateAvailable,
                        Condition = product.Condition,
                        ItemDescription = product.ItemDescription,
                        ItemAvailable = product.ItemAvailable
                    };

                    // Call UpdateEntity with the correct ViewModel
                    _dbProduct.UpdateEntity(productViewModel); // Update product availability in the database
                }

                // Update the total price of the cart
                cart.TotalPrice = cart.Items.Sum(i => i.Price);

                // Save the updated cart to the session
                HttpContext.Session.SetObjectAsJSON(CartSessionKey, cart);
            }

            return RedirectToAction("Index", "ShoppingCart"); // Redirect to the cart page
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
