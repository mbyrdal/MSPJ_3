using ClientWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ClientWeb.DTOs;
using ClientWeb.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;

namespace ClientWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbHelper _dbHelper;
        private string _connectionString;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Inventory()
        {
            // Create list to contain products
            List<ProductInventoryDTO> fetchedProductsWithNames = new List<ProductInventoryDTO>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7134/api/Products/");
                string endpoint = client.BaseAddress.ToString() + "withNames";
                fetchedProductsWithNames = await client.GetFromJsonAsync<List<ProductInventoryDTO>>(endpoint); // Fetch actual data
            }

            List<ProductInventoryDTO> products = new List<ProductInventoryDTO>();
            products = fetchedProductsWithNames.Select(product => new ProductInventoryDTO
            {
                ID = product.ID,
                Name = product.Name,
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
            List<ProductDTO> searchResults = SearchProductByCriteria(partName);

            // Return the updated partial view with search results
            return PartialView("_SearchResults", searchResults);
        }

        // Method to search for products by both OEM and ItemDescription
        private List<ProductDTO> SearchProductByCriteria(string partName)
        {
            List<ProductDTO> searchResults = new List<ProductDTO>();

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
