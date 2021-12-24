namespace ProjectManager.MVC.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Handles requests for the assumptions page.
    /// </summary>
    public sealed class AssumptionsController : Controller
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<AssumptionsController> _logger;

        /// <summary>
        ///     Creates a new instance of the <see cref="AssumptionsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AssumptionsController(ILogger<AssumptionsController> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Displays the assumptions page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the assumptions page.</returns>
        [HttpGet]
        public IActionResult Assumptions()
        {
            this._logger.LogInformation("Entered GET method Assumptions().");

            return this.View();
        }
    }
}
