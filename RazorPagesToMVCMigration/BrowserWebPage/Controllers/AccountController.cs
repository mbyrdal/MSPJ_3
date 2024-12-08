using Microsoft.AspNetCore.Mvc;
using BrowserWebPage.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ServiceAPI.Models;
using ServiceAPI.DTOs;

namespace BrowserWebPage.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        // Inject SignInManager<ApplicationUser>
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // Handle external login (OAuth2)
        [HttpGet]
        public IActionResult ExternalLogin(string provider)
        {
            // Redirect to the OAuth2 provider's authorization endpoint
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        // ExternalLoginCallback to handle the callback after the OAuth2 login
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            // Example: Redirect to a registration page with pre-filled information
            return RedirectToAction("RegisterExternal", new { email });
        }

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if(user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return View("~/Views/Account/Login.cshtml", model);
        }
    }
}
