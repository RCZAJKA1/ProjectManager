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
        ///     Creates a new instance of the <see cref="AssumptionsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AssumptionsController(ILogger<AssumptionsController> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        internal ILogger<AssumptionsController> Logger { get; }

        /// <summary>
        ///     Displays the assumptions page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the assumptions page.</returns>
        [HttpGet]
        public IActionResult Assumptions()
        {
            return this.View();
        }
    }
}
