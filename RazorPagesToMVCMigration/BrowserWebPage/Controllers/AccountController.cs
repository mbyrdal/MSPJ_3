//using Microsoft.AspNetCore.Mvc;
//using ClientWeb.Models;

//exits if for use later otherwise delete

//namespace ClientWeb.Controllers
//{
//    public class AccountController : Controller
//    {
        
//        // GET: /Account/Details
//        [HttpGet]
//        public IActionResult Details()
//        {
//            // Always return the default account
//            return View(DefaultAccount);
//        }

//        // POST: /Account/Update
//        [HttpPost]
//        public IActionResult Update(Account account)
//        {
//            if (ModelState.IsValid)
//            {
//                // Simulate saving updated data (in real scenario, save to database)
//                DefaultAccount = account;

//                // Display a success message
//                TempData["Message"] = "Dine konto oplysninger er blevet opdateret!";
//                return RedirectToAction("Details");
//            }

//            // If validation fails, return the view with the current data
//            return View("Details", account);
//        }
//    }
//}
