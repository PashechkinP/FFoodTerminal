using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FFoodTerminal.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
            public IActionResult Index() => View();
        
        //public async Task<IActionResult> IndexAsync()
        //{

        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
