using AirLinesMVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace AirLinesMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userName = User.Identity.Name;
                string role = User.Claims.ToArray()[4].Value;
                string secretKey = "My Name is James, James Bond 007";
                HttpClient httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:5191/api/Auth/") };
                string token = await httpClient.GetStringAsync($"{userName}/{role}/{secretKey}");
                HttpContext.Session.SetString("token", token);

            }
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
    }
}
