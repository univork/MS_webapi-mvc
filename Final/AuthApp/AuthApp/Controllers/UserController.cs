using AuthApp.Models;
using AutoMapper;
using EF.Models;
using AuthApp.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginModel);
            }

            using var context = new AuthdbContext();
            var user = context.Users.FirstOrDefault(
                u => u.Email == userLoginModel.Email
            );
            if (user == null) { 
                return View();
            }

            if (PasswordUtil.VerifyPassword(userLoginModel.Password, user.Password))
            {
                List<Claim> claims = [new("user", userLoginModel.Email)];
                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "user", "role")));

                return RedirectToAction("Account", "User");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserCreateViewModel userCreateModel)
        {
            if(!ModelState.IsValid)
            {
                return View(userCreateModel);
            }

            User user = _mapper.Map<User>(userCreateModel);
            using var context = new AuthdbContext();
            context.Users.Add(user);
            context.SaveChanges();

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
            using AuthdbContext context = new AuthdbContext();
            var user = context.Users.FirstOrDefault(u => u.Email.Equals(userEmail));
            if (user == null) { 
                return RedirectToAction("Index", "Home");
            }
            UserReadViewModel userReadModel = _mapper.Map<UserReadViewModel>(user);
            return View(userReadModel);
        }
    }
}
