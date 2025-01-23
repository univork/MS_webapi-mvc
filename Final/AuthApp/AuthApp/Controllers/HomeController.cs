using AuthApp.Models;
using AutoMapper;
using EF.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuthApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;

        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            using var context = new AuthdbContext();
            User user = context.Users.First();
            UserReadViewModel userModel = _mapper.Map<UserReadViewModel>(user);
            return View(userModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
