namespace ProjectManager.MVC.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Handles requests for the risks page.
    /// </summary>
    public sealed class RisksController : Controller
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<RisksController> _logger;

        /// <summary>
        ///     Creates a new instance of the <see cref="RisksController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RisksController(ILogger<RisksController> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Redirects to the risks page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the risks page.</returns>
        [HttpGet]
        public IActionResult Risks()
        {
            this._logger.LogInformation("Entered GET method Risks().");

            return this.View();
        }
    }
}
