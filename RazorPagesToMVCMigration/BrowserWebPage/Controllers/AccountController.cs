using Microsoft.AspNetCore.Mvc;
using BrowserWebPage.Models;

namespace BrowserWebPage.Controllers
{
    public class AccountController : Controller
    {
        // Mock data (replace with database logic later)
        private static Account _account = new Account
        {
            FirstName = "Mads",
            LastName = "Nielsen",
            Address = "Hovedvej 123, 2800 Kongens Lyngby",
            PhoneNum = "12345678",
            Email = "mads@example.com"
        };

        [HttpGet]
        public IActionResult Details()
        {
            return View("Views\\Account\\Details.cshtml");
        }

        [HttpPost]
        public IActionResult Update(Account account)
        {
            if (ModelState.IsValid)
            {
                // Save updated data logic (e.g., save to database)
                _account = account;

                TempData["Message"] = "Dine konto oplysninger er blevet opdateret!";
                return RedirectToAction("Details");
            }

            return View("Details", account);
        }
    }
}
