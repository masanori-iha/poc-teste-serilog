using DM2.Serilog.POC.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;

namespace DM2.Serilog.POC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("The first log Information");

            for (int i = 0; i < 100; i++)
            {

                if(i == 56)
                {
                    //throw new Exception("This is our demo exception");
                }
                else
                {
                    _logger.LogInformation("The value of i is {iLoopCountValue}", i);
                }

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