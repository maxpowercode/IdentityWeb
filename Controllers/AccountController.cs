using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IdentityWeb.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using IdentityWeb.Data;


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace IdentityWeb.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUserRole> _roleManager;

        public AccountController(
             ILogger<HomeController> logger
            ,UserManager<ApplicationUser> userManager
            ,RoleManager<ApplicationUserRole> roleManager)
        {
            _logger = logger;
            _userManager =userManager;
            _roleManager =roleManager;
        }

        public IActionResult Singin()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Singin(
            RegisterViewModel registerViewModel
            )
        {
            if(ModelState.IsValid)
            {
                var user=new ApplicationUser
                {
                    Email =registerViewModel.Email,
                    UserName =registerViewModel.Email,
                    NormalizedEmail=registerViewModel.Email,
                    NormalizedUserName =registerViewModel.Email
                };
                var restule =await _userManager.CreateAsync(user,registerViewModel.Password);
                if(restule.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(restule);
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult MarkLogin()
        {

            var UserClaims =new List<Claim>{
                new Claim(ClaimTypes.Name,"zhangshuai"),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var IdentityEntity =new ClaimsIdentity(UserClaims,CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(IdentityEntity));

            return Ok();
        }
        public IActionResult Logout()
        {


            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok();
        }
        public IActionResult Index()
        {
            return View();
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


        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
