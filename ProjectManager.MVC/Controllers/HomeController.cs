namespace ProjectManager.MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using ProjectManager.MVC.Models;

    using System;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult Projects()
        {
            return this.View();
        }

        public IActionResult Actions()
        {
            return this.View();
        }

        public IActionResult Decisions()
        {
            return this.View();
        }

        public IActionResult Risks()
        {
            return this.View();
        }

        public IActionResult Assumptions()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
