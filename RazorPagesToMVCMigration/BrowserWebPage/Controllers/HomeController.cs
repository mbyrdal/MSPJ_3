using BrowserWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _dbHelper = new DbHelper(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inventory()
        {
            // Initial list of products to display when the page loads (optional)
            List<ProductViewModel> productViewModelList = new List<ProductViewModel>(); // Fetch actual data here
            return View("~/Views/Inventory/Inventory.cshtml", productViewModelList);
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
