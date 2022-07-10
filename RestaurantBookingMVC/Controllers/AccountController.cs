using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantLibrary.Connection;
using RestaurantLibrary.Models;
using RestaurantLibrary.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantBookingMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register registerUser)
        {
            if(ModelState.IsValid)
            {
                var isEmailTaken = await _userManager.FindByEmailAsync(registerUser.Email.ToUpper());

                if(isEmailTaken!=null)
                {
                    ModelState.AddModelError("", "Email is already taken.");
                }

                var user = new User()
                {
                    Email = registerUser.Email,
                    Firstname = registerUser.Firstname,
                    Lastname = registerUser.Lastname,
                    UserName=registerUser.Email,
                };

                var result = await _userManager.CreateAsync(user, registerUser.Password);

                if(result.Succeeded)
                {
                    var claims = new List<Claim>();

                    Claim emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email);
                    Claim nameClaim = new Claim("Name", $"{user.Firstname} {user.Lastname}");

                    claims.Add(emailClaim);
                    claims.Add(nameClaim);

                    await _userManager.AddClaimsAsync(user, claims);

                    if(user.Email == "bookingadmin@gmail.com")
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                    }

                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(registerUser);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login, string returnUrl=null)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

                if(result.Succeeded)
                {
                    var user = _userManager.FindByEmailAsync(login.Email);

                    var identity = new ClaimsIdentity("CookieAuth");
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("CookieAuth", principal);

                    if (returnUrl == null || returnUrl == "/")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToPage(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Wrong email or password.");
                }
            }

            return View(login);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
