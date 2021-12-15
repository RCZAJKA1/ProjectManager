namespace ProjectManager.MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///     Handles requests for the projects page.
    /// </summary>
    public sealed class ProjectsController : Controller
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="ProjectsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectsController(ILogger<ProjectsController> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        internal ILogger<ProjectsController> Logger { get; }

        /// <summary>
        ///     Displays the projects page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the projects page.</returns>
        [HttpGet]
        public async Task<IActionResult> Projects()
        {
            return await Task.FromResult(this.View());
        }        
    }
}
