using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Models;
using UserManagementSystem.Services.Interfaces;
using UserManagementSystem.ViewModels;

namespace UserManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IUsersService usersService;
        private readonly IConfiguration config;

        public HomeController(
            ILogger<HomeController> logger,
            IUsersService userService,
            IConfiguration config)
        {
            this.logger = logger;
            this.usersService = userService;
            this.config = config;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadData()
        {
            IEnumerable<UserViewModel> fetchedUsers = await usersService
                .FetchUsers(this.config["ApiSettings:UsersUrl"]!);

            return View(nameof(Index), fetchedUsers);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
