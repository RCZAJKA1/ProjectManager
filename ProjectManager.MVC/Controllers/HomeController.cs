namespace ProjectManager.MVC.Controllers
{
    using System;
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using ProjectManager.MVC.Models;

    /// <summary>
    ///     Handles requests for the home page.
    /// </summary>
    public sealed class HomeController : Controller
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        ///     Creates a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Displays the home page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the home page.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            this._logger.LogInformation("Entered GET method Index().");

            return this.View();
        }

        /// <summary>
        ///     Displays the error page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the error page.</returns>
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            this._logger.LogInformation("Entered GET method Error().");

            ErrorViewModel errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
            };

            return this.View(errorViewModel);
        }
    }
}
