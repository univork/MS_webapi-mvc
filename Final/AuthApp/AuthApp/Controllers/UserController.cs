using AuthApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AuthApp.BL;
using AuthApp.Filters;

namespace AuthApp.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserBL userBL;

        public UserController(IMapper mapper, UserBL userBL)
        {
            _mapper = mapper;
            this.userBL = userBL;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginModel)
        {
            if (!ModelState.IsValid)
                return View(userLoginModel);

            if (!userBL.Login(userLoginModel))
                return View(userLoginModel);

            List<Claim> claims = [new("user", userLoginModel.Email)];
            await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "user", "role")));

            return RedirectToAction("Account", "User");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserCreateViewModel userCreateModel)
        {
            if(!ModelState.IsValid)
                return View(userCreateModel);

            userBL.Register(userCreateModel);
            return RedirectToAction("Login", "User");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [Authorize]
        public IActionResult Account() {
            var userEmail = HttpContext.User.Identity.Name;
            UserReadViewModel? userReadModel = userBL.Account(userEmail);
            if (userReadModel == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(userReadModel);
        }
    }
}
