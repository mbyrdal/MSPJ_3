using Microsoft.AspNetCore.Mvc;
using RazorPagesToMVCMigration.DAL.Repository;
using RazorPagesToMVCMigration.DAL;
using RazorPagesToMVCMigration.Models;
using System.Diagnostics;

namespace RazorPagesToMVCMigration.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarRepository _carRepository; // For rendering Cars from dbo.Cars
        private readonly ProductRepository _productRepository; // For rendering Products from dbo.Products

        public HomeController(IConfiguration configuration)
        {
            _carRepository = new CarRepository(configuration);
            _productRepository = new ProductRepository(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

       /* public async Task<IActionResult> Inventory()
        {
            List<Product> allProducts = (await _productRepository.GetAllAsync()).ToList();
            return View("~/Views/Inventory/Inventory.cshtml", allProducts);
        }*/

        public async Task<IActionResult> Inventory(string oemNumber)
        {
            List<Product> allProducts;

            // If oemNumber is provided, filter products by OEM number
            if (string.IsNullOrEmpty(oemNumber))
            {
                allProducts = (await _productRepository.GetAllAsync()).ToList();
            }
            else
            {
                // Filter products by oemNumber if a search term is provided
                allProducts = (await _productRepository.GetAllAsync())
                    .Where(p => p.OEM.Contains(oemNumber, StringComparison.OrdinalIgnoreCase)) // Case-insensitive search for OEM number
                    .ToList();
            }

            return View("~/Views/Inventory/Inventory.cshtml", allProducts);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
