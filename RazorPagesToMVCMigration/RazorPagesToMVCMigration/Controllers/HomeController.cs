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

        public IActionResult Inventory()
        {
            /*
            List<Car> allCars = (List<Car>)_carRepository.GetAll();
            return View("~/Views/Inventory/Inventory.cshtml", allCars);
            */
            List<Product> allProducts = _productRepository.GetAll().ToList();
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
