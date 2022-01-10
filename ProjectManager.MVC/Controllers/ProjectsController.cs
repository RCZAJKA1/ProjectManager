namespace ProjectManager.MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using ProjectManager.Business.Services;
    using ProjectManager.Common;
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
        private readonly ILogger<ProjectsController> logger;

        /// <summary>
        ///     The project service.
        /// </summary>
        private readonly IProjectService projectService;

        /// <summary>
        ///     The cancellation token source.
        /// </summary>
        private readonly CancellationTokenSource cancellationTokenSource;

        /// <summary>
        ///     Creates a new instance of the <see cref="ProjectsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="projectService">The project service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectsController(ILogger<ProjectsController> logger, IProjectService projectService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        ///     Displays the projects page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the projects page.</returns>
        [HttpGet]
        public async Task<IActionResult> Projects()
        {
            this.logger.LogInformation("Entered method Projects().");

            ProjectViewModel projectViewModel = new ProjectViewModel
            {
                Projects = new List<Project>(),
                Owners = await this.projectService.GetActiveProjectOwnersAsync(this.cancellationTokenSource.Token).ConfigureAwait(false),
                Statuses = await this.projectService.GetProjectStatusesAsync(this.cancellationTokenSource.Token).ConfigureAwait(false),
                Categories = await this.projectService.GetProjectCategoriesAsync(this.cancellationTokenSource.Token).ConfigureAwait(false)
            };

            return this.View(projectViewModel);
        }

        /// <summary>
        ///     Adds a new project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>An <see cref="IActionResult"/> representing the projects page.</returns>
        [HttpPost]
        public async Task<IActionResult> AddProject(Project project)
        {
            this.logger.LogInformation("Entered method AddProject().");

            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (!this.ModelState.IsValid)
            {
                this.logger.LogError("Model state invalid.");

                return this.RedirectToAction("Projects");
            }

            await this.projectService.AddProjectAsync(project, this.cancellationTokenSource.Token).ConfigureAwait(false);

            return this.RedirectToAction("Projects");
        }

        /// <summary>
        ///     Searches for projects using the specified criteria.
        /// </summary>
        /// <param name="name">The project name.</param>
        /// <returns>An <see cref="IActionResult"/> representing the search projects partial view.</returns>
        public async Task<IActionResult> SearchProjects(string name)
        {
            this.logger.LogInformation("Entered method SearchProjects().");

            // Empty excludes filter
            if (name == null)
            {
                name = String.Empty;
            }
            name.EnsureOnlyLettersAndNumbers();

            IList<Project> projects = await this.projectService.SearchProjectsAsync(name, this.cancellationTokenSource.Token).ConfigureAwait(false);
            ProjectSearchViewModel projectSearchViewModel = new ProjectSearchViewModel
            {
                Projects = projects
            };

            return this.PartialView("ProjectSearch", projectSearchViewModel);
        }

        /// <summary>
        ///     Deletes the project with the specified identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>An <see cref="IActionResult"/> representing the search projects partial view.</returns>
        public async Task<IActionResult> DeleteProject(int projectId, ProjectSearchViewModel projectSearchViewModel)
        {
            this.logger.LogInformation("Entered method SearchProjects().");

            if (projectId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(projectId));
            }

            await this.projectService.DeleteProjectAsync(projectId, this.cancellationTokenSource.Token).ConfigureAwait(false);

            projectSearchViewModel.Projects = projectSearchViewModel.Projects.ToList().Where(x => x.Id != projectId);

            // TODO: fix to update projects table after deleting a project
            return this.PartialView("ProjectSearch", projectSearchViewModel);
        }
    }
}
