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
        ///     The logger.
        /// </summary>
        private readonly ILogger<ProjectsController> _logger;

        /// <summary>
        ///     The project service.
        /// </summary>
        private readonly IProjectService _projectService;

        /// <summary>
        ///     The cancellation token source.
        /// </summary>
        private readonly CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        ///     Creates a new instance of the <see cref="ProjectsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="projectService">The project service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectsController(ILogger<ProjectsController> logger, IProjectService projectService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            this._cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        ///     Displays the projects page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the projects page.</returns>
        [HttpGet]
        public async Task<IActionResult> Projects()
        {
            this._logger.LogInformation("Entered method Projects().");

            //IList<Project> projects = await this._projectService.GetProjectsForUserAsync(this._cancellationTokenSource.Token).ConfigureAwait(false);

            // TODO: remove stubbed in database return
            DateTime now = DateTime.Now;
            IList<Project> projects = new List<Project>
            {
                new Project
                {
                    Name = "testProjectName",
                    Description = "testProjectDescription",
                    Id = 1,
                    Owner = "testProjectOwner",
                    Status = "Active",
                    StartDate = now.AddDays(-7),
                    EndDate = now,
                    DueDate = now
                }
            };

            ProjectViewModel projectViewModel = new ProjectViewModel
            {
                Projects = projects
            };

            return this.View(projectViewModel);
        }
    }
}
