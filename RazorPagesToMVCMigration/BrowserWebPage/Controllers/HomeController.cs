using BrowserWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using System.Diagnostics;

namespace BrowserWebPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Inventory()
        {
            ProductViewModel productViewModel = new ProductViewModel(1, 2, 3, "4", 5, new DateTime(2001-1-1), "8", "6");
            List<ProductViewModel> productViewModelList = new List<ProductViewModel>();
            productViewModelList.Add(productViewModel);
            return View("~/Views/Inventory/Inventory.cshtml", productViewModelList);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreateAccount()
        {
            return View("~/Views/Account/CreateAccount.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ServiceAPI.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AccountDetails()
        {
            return View("~/Views/Account/Details.cshtml");
        }
    }
}
