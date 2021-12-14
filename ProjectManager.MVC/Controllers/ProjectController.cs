namespace ProjectManager.MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using ProjectManager.MVC.Models;

    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class ProjectController : Controller
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<ProjectController> logger;

        /// <summary>
        ///     Creates a new instance of the <see cref="ProjectController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectController(ILogger<ProjectController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Redirects to the projects page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the projects page.</returns>
        [HttpGet]
        public async Task<IActionResult> Projects()
        {
            return await Task.FromResult(this.View());
        }

        /// <summary>
        ///     Redirects to the actions page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the actions page.</returns>
        [HttpGet]
        public async Task<IActionResult> Actions()
        {
            return await Task.FromResult(this.View());
        }

        /// <summary>
        ///     Obtains the actions form submission and redirects back to actions.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the actions page.</returns>
        [HttpPost]
        public async Task<IActionResult> Actions(ActionViewModel actionViewModel)
        {
            this.ViewBag.Success = $"Added action {actionViewModel.Description}";

            return await Task.FromResult(this.View());
        }

        /// <summary>
        ///     Redirects to the decisions page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the decisions page.</returns>
        [HttpGet]
        public async Task<IActionResult> Decisions()
        {
            return await Task.FromResult(this.View());
        }

        /// <summary>
        ///     Redirects to the risks page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the risks page.</returns>
        [HttpGet]
        public async Task<IActionResult> Risks()
        {
            return await Task.FromResult(this.View());
        }

        /// <summary>
        ///     Redirects to the assumptions page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the assumptions page.</returns>
        [HttpGet]
        public async Task<IActionResult> Assumptions()
        {
            return await Task.FromResult(this.View());
        }

        /// <summary>
        ///     Redirects to the error page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the error page.</returns>
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return await Task.FromResult(this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }));
        }
    }
}
