namespace ProjectManager.MVC.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Handles requests for the projects page.
    /// </summary>
    public sealed class DecisionsController : Controller
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<DecisionsController> _logger;

        /// <summary>
        ///     Creates a new instance of the <see cref="DecisionsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DecisionsController(ILogger<DecisionsController> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Redirects to the decisions page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the decisions page.</returns>
        [HttpGet]
        public IActionResult Decisions()
        {
            this._logger.LogInformation("Entered GET method Decisions().");

            return this.View();
        }
    }
}
