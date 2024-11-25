using Microsoft.AspNetCore.Mvc;
using BrowserWebPage.Models;

namespace BrowserWebPage.Controllers
{
    public class AccountController : Controller
    {
        // Default account data (used as mock data for demonstration purposes)
        private static Account DefaultAccount = new Account
        {
            FirstName = "Mads",
            LastName = "Nielsen",
            Address = "Hovedvej 123, 2800 Kongens Lyngby",
            PhoneNum = "12345678",
            Email = "mads@example.com",
            ListOfOrders = new List<string> { "Order1", "Order2", "Order3" }
        };

        // GET: /Account/Details
        [HttpGet]
        public IActionResult Details()
        {
            // Always return the default account
            return View(DefaultAccount);
        }

        // POST: /Account/Update
        [HttpPost]
        public IActionResult Update(Account account)
        {
            if (ModelState.IsValid)
            {
                // Simulate saving updated data (in real scenario, save to database)
                DefaultAccount = account;

                // Display a success message
                TempData["Message"] = "Dine konto oplysninger er blevet opdateret!";
                return RedirectToAction("Details");
            }

            // If validation fails, return the view with the current data
            return View("Details", account);
        }
    }
}
