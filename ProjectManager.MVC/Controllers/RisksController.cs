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
        ///     Creates a new instance of the <see cref="RisksController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RisksController(ILogger<RisksController> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        internal ILogger<RisksController> Logger { get; }

        /// <summary>
        ///     Redirects to the risks page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the risks page.</returns>
        [HttpGet]
        public IActionResult Risks()
        {
            return this.View();
        }
    }
}
