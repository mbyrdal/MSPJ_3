using Microsoft.AspNetCore.Mvc;
using ClientWeb.DTOs;
using ClientWeb.Models;
using ClientWeb.Utilities;
using System.Linq;

namespace ClientWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        private const string CartSessionKey = "Cart";
        private readonly HttpClient _httpClient;
        private string _clientBaseURL = "https://localhost:7134/";

        public ShoppingCartController(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(_clientBaseURL);
        }

        // GET: ShoppingCart/Index -> Display the shopping cart
        public ActionResult Index()
        {
            // Fetch shopping cart using HttpContext session
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();
            return View(cart);
        }

        // POST: ShoppingCart/AddToCart -> Append a product to the Shopping Cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(string OEM)
        {
            using (_httpClient)
            {
                string getProductEndpoint = $"{_httpClient.BaseAddress.ToString()}api/Products/GetProductWithName/{OEM}";
                var tempProduct = await _httpClient.GetFromJsonAsync<ProductInventoryDTO>(getProductEndpoint);

                if(tempProduct != null && tempProduct.ItemAvailable == true)
                {
                    var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();

                    var productAlreadyInCart = cart.Items.FirstOrDefault(i => i.OEM == tempProduct.OEM);
                    if(productAlreadyInCart == null)
                    {
                        // Add the product to the Cart
                        cart.Items.Add(tempProduct);

                        // Update the total price of the Cart
                        cart.TotalPrice = cart.Items.Sum(i => i.Price);

                        // Save and update the Cart within the current session
                        HttpContext.Session.SetObjectAsJSON(CartSessionKey, cart);
                    }
                }

                // Redirect to the Cart overview page after adding.
                return RedirectToAction("Index");
            }
        }

        // Clear the cart
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CartSessionKey);
            return RedirectToAction(nameof(Index));
        }

        // POST: ShoppingCart/CheckOut -> Sell and remove products listed in the Cart
        public async Task<IActionResult> CheckOut()
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey);

            if (cart != null && cart.Items?.Count > 0)
            {
                // Handle order processing here (e.g., save to database)
                using (_httpClient)
                {
                    foreach (var product in cart.Items)
                    {
                        // Define Car API endpoint
                        var carEndpoint = $"{_httpClient.BaseAddress.ToString()}api/Cars/{product.CarID}";

                        // Fetch Car response object
                        var carResponse = await _httpClient.GetFromJsonAsync<Car>(carEndpoint);

                        // Prepare the payload
                        var soldProduct = new ProductInventoryDTO
                        {
                            ID = product.ID,
                            CarPartID = product.CarPartID,
                            CarID = product.CarID,
                            Name = product.Name,
                            OEM = product.OEM,
                            Price = product.Price,
                            DateAvailable = product.DateAvailable,
                            Condition = product.Condition,
                            ItemDescription = product.ItemDescription,
                            ItemAvailable = false // Mark as sold
                        };
                        
                        // Construct endpoint URL 
                        var productUpdateEndpoint = $"{_httpClient.BaseAddress.ToString()}api/Products/{soldProduct.OEM}/{soldProduct.Name}/{carResponse.VINNumber}";
                        var response = await _httpClient.PutAsJsonAsync($"{productUpdateEndpoint}", soldProduct);
                    }
                }
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

        // Remove a product from the cart
        [HttpPost]
        public IActionResult RemoveFromCart(string OEM)
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>(CartSessionKey);

            if (cart?.Items != null)
            {
                using (_httpClient)
                {
                    var productToRemove = cart.Items.FirstOrDefault(i => i.OEM == OEM);

                    if (productToRemove != null)
                    {
                        cart.Items.Remove(productToRemove);
                        cart.TotalPrice = cart.Items.Sum(i => i.Price);
                        HttpContext.Session.SetObjectAsJSON(CartSessionKey, cart);
                    }
                }
            }
            // Redirect back to the shopping cart page
            return RedirectToAction(nameof(Index));
        }
    }
}
