namespace ProjectManager.MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using ProjectManager.Business.Services;
    using ProjectManager.Common.Models;
    using ProjectManager.MVC.Models;

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
        public ProjectsController(ILogger<ProjectsController> logger, IProjectService projectService)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.ProjectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            this.CancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        internal ILogger<ProjectsController> Logger { get; }

        /// <summary>
        ///     Gets the projects service.
        /// </summary>
        internal IProjectService ProjectService { get; }

        /// <summary>
        ///     Gets the cancellation token source.
        /// </summary>
        private CancellationTokenSource CancellationTokenSource { get; }

        /// <summary>
        ///     Displays the projects page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the projects page.</returns>
        [HttpGet]
        public async Task<IActionResult> Projects()
        {
            IList<Project> projects = await this.ProjectService.GetProjectsForUserAsync(this.CancellationTokenSource.Token).ConfigureAwait(false);

            ProjectViewModel projectViewModel = new ProjectViewModel
            {
                Projects = projects
            };

            return this.View(projectViewModel);
        }
    }
}
