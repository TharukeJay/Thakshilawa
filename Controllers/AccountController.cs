using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using XYZLaundry.Data;
using XYZLaundry.Models;
using XYZLaundry.ViewModels;

namespace XYZLaundry.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = loggerFactory.CreateLogger<AccountController>();            
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var existingUserWithSameEmail = await _userManager.FindByEmailAsync(registerViewModel.Email);

                if (existingUserWithSameEmail == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = registerViewModel.Email,
                        Email = registerViewModel.Email,
                        FirstName = registerViewModel.FirstName,
                        LastName = registerViewModel.LastName,
                        DateCreated = DateTime.Now.ToString()
                    };

                    var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.FirstName));
                        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, user.LastName));
                        await _userManager.AddToRoleAsync(user, "User");

                        // Create a customer object for this login
                        var customer = new Customer
                        {
                            Name = registerViewModel.FirstName + ' ' + registerViewModel.LastName,
                            Email = registerViewModel.Email,
                            CreatedBy = registerViewModel.Email,
                            CreatedOn = DateTime.Now
                        };

                        _context.Customers.Add(customer);
                        await _context.SaveChangesAsync();                        

                        return RedirectToLocal(returnUrl);
                    }

                    AddErrors(result);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There is an already registered user with that email.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(registerViewModel);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
