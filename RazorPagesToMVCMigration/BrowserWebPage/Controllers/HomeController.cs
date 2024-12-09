using BrowserWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ServiceAPI.BusinessLogic;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace BrowserWebPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbHelper _dbHelper;
        private string _connectionString;
        private readonly IProductControl _productControl;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IProductControl productControl)
        {
            _logger = logger;
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
            _productControl = productControl;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inventory()
        {
            // Initial list of products to display when the page loads (optional)
            List<Product> productList = new List<Product>(); // Fetch actual data here
            productList = _productControl.GetAllProducts();
            return View("~/Views/Inventory/Inventory.cshtml", productList);
        }

        public IActionResult CreateAccount()
        {
            return View("~/Views/Account/CreateAccount.cshtml");
        }

        public IActionResult AccountDetails()
        {
            return View("~/Views/Account/Details.cshtml");
        }

        // This method handles the AJAX request for searching spare parts
        [HttpGet]
        public IActionResult SearchParts(string partName)
        {
            // Get products based on the search term for both OEM and part name (ItemDescription)
            List<ProductViewModel> searchResults = SearchProductByCriteria(partName);

            // Return the updated partial view with search results
            return PartialView("_SearchResults", searchResults);
        }

        // Method to search for products by both OEM and ItemDescription
        private List<ProductViewModel> SearchProductByCriteria(string partName)
        {
            List<ProductViewModel> searchResults = new List<ProductViewModel>();

            if (!string.IsNullOrEmpty(partName))
            {
                // You can modify this query to include searching by both OEM and ItemDescription
                var query = @"
            SELECT CarPartID, SaleID, OEM, Price, DateAvailable, Condition, ItemDescription
            FROM Products
            WHERE ItemDescription LIKE @partName OR OEM LIKE @partName";

                // Perform a case-insensitive search for both ItemDescription and OEM
                var result = _dbHelper.ExecuteQuery(query, new SqlParameter("@partName", $"%{partName}%"));
                searchResults = result.ToList();
            }

            return searchResults;
        }

        [HttpGet]
        public IActionResult ShoppingCart()
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>("Cart") ?? new ShoppingCart();

            // Fetch a product using its ID (replace this with actual database logic)
            var product = new Product
            {
                ID = productId,
                CarPartID = 101,
                CarID = 202,
                OEM = "OEM123",
                Price = 100.50m,
                DateAvailable = DateTime.Now,
                Condition = "New",
                ItemDescription = "High-quality car part",
                ItemAvailable = true
            };

            cart.Items.Add(product);
            cart.TotalPrice = cart.Items.Sum(item => item.Price);
            HttpContext.Session.SetObjectAsJSON("Cart", cart);

            return RedirectToAction("ShoppingCart");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>("Cart");

            if (cart != null)
            {
                var product = cart.Items.FirstOrDefault(p => p.ID == productId);
                if (product != null)
                {
                    cart.Items.Remove(product);
                    cart.TotalPrice = cart.Items.Sum(item => item.Price);
                    HttpContext.Session.SetObjectAsJSON("Cart", cart);
                }
            }

            return RedirectToAction("ShoppingCart");
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("ShoppingCart");
        }

        [HttpPost]
        public IActionResult CheckOut()
        {
            var cart = HttpContext.Session.GetObjectFromJSON<ShoppingCart>("Cart");

            if (cart != null && cart.Items.Any())
            {
                // Process the checkout (e.g., save the order to a database)
                HttpContext.Session.Remove("Cart");
                return RedirectToAction("OrderConfirmation");
            }

            TempData["Error"] = "Your cart is empty!";
            return RedirectToAction("ShoppingCart");
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }
    }
}
