using Microsoft.AspNetCore.Mvc;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> AddToCart(int productID)
        {
            var product = await _dbProduct.GetByIdentifierAsync(productID); // Retrieve the product asynchronously

            if (product != null && product.ItemAvailable)
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
                    var productViewModel = new ProductViewModel
                    {
                        ID = product.ID,
                        OEM = product.OEM,
                        Price = product.Price,
                        DateAvailable = product.DateAvailable,
                        Condition = product.Condition,
                        ItemDescription = product.ItemDescription,
                        ItemAvailable = product.ItemAvailable
                    };

                    // Update product availability in the database
                    await _dbProduct.UpdateEntityDTOAsync(productViewModel);
                }

                // Update the total price of the cart
                cart.TotalPrice = cart.Items.Sum(i => i.Price);

                // Save the updated cart to the session
                HttpContext.Session.SetObjectAsJSON(CartSessionKey, cart);
            }

            return RedirectToAction("Index", "ShoppingCart");
        }

        // Remove a product from the cart
        [HttpDelete]
        public async Task<JsonResult> RemoveFromCart(int productID)
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

                    // Update product availability in the database
                    productToRemove.ItemAvailable = true;
                    var productViewModel = new ProductViewModel
                    {
                        ID = productToRemove.ID,
                        OEM = productToRemove.OEM,
                        Price = productToRemove.Price,
                        DateAvailable = productToRemove.DateAvailable,
                        Condition = productToRemove.Condition,
                        ItemDescription = productToRemove.ItemDescription,
                        ItemAvailable = productToRemove.ItemAvailable
                    };

                    await _dbProduct.UpdateEntityDTOAsync(productViewModel);
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
        public async Task<IActionResult> CheckOut()
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey);

            if (cart != null && cart.Items?.Count > 0)
            {
                // Handle order processing asynchronously
                await ProcessOrderAsync(cart);

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

        // Simulated async order processing
        private async Task ProcessOrderAsync(ShoppingCart cart)
        {
            // Simulate saving the order to the database or processing logic
            await Task.Delay(500); // Replace with actual implementation
        }
    }
}
