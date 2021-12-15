namespace ProjectManager.MVC.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Handles requests for the projects page.
    /// </summary>
    public sealed class DecisionsController : Controller
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="DecisionsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DecisionsController(ILogger<DecisionsController> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        internal ILogger<DecisionsController> Logger { get; }

        /// <summary>
        ///     Redirects to the decisions page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the decisions page.</returns>
        [HttpGet]
        public async Task<IActionResult> Decisions()
        {
            return await Task.FromResult(this.View());
        }
    }
}
