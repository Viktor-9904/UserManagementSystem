using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserManagementSystem.Models;
using UserManagementSystem.Services;
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
            List<UserViewModel> fetchedUsers = await usersService
                .FetchUsersAsync(this.config["ApiSettings:UsersUrl"]!);

            return View(nameof(Index), fetchedUsers);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAll(List<UserViewModel> users)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index)); // todo: notify client for invalid input
            }

            bool deleteSuccess = await this.usersService.DeleteAllUsersAsync();
            bool saveSuccess = await this.usersService.SaveAllUsersAsync(users);

            if (!saveSuccess && !deleteSuccess)
            {
                //return
            }

            return View(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
  