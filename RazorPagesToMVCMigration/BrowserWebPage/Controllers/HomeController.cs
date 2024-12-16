using BrowserWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using BrowserWebPage.BusinessLogic;
using BrowserWebPage.BusinessLogic.Interfaces;
using BrowserWebPage.DTOs;
using BrowserWebPage.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;

namespace BrowserWebPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbHelper _dbHelper;
        private string _connectionString;
        private readonly IProductControl _productControl;
        private readonly ICarPartControl _carPartControl;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IProductControl productControl, ICarPartControl carPartControl)
        {
            _logger = logger;
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
            _productControl = productControl;
            _carPartControl = carPartControl;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Inventory()
        {
            // Initial list of products to display when the page loads (optional)
            List<Product> productList = new List<Product>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7134/api/Products/");
                productList = await client.GetFromJsonAsync<List<Product>>(client.BaseAddress.ToString()); // Fetch actual data
            }

            List<ProductInventoryViewModel> products = new List<ProductInventoryViewModel>();
            products = productList.Select(product => new ProductInventoryViewModel
            {
                ID = product.ID,
                Name = _carPartControl.GetCarPartName(product.CarPartID),
                OEM = product.OEM,
                Price = product.Price,
                Condition = product.Condition,
                ItemDescription = product.ItemDescription,
                ItemAvailable = product.ItemAvailable
            }).ToList();


            return View("~/Views/Inventory/Inventory.cshtml", products);
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
    }
}
