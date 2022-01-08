namespace ProjectManager.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using FluentValidation;
    using FluentValidation.Results;

    using Microsoft.Extensions.Logging;

    using ProjectManager.Common;
    using ProjectManager.Common.Models;
    using ProjectManager.Data.Repositories;

    /// <inheritdoc cref="IProjectService"/>
    public sealed class ProjectService : IProjectService
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<ProjectService> logger;

        /// <summary>
        ///     The project repository.
        /// </summary>
        private readonly IProjectRepository projectRepository;

        /// <summary>
        ///     The project validator.
        /// </summary>
        private readonly IValidator<Project> validator;

        /// <summary>
        ///     Creates a new instance of the <see cref="ProjectService"/> class.
        /// </summary>
        /// <param name="projectRepository">The project repository.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectService(ILogger<ProjectService> logger, IProjectRepository projectRepository, IValidator<Project> validator)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <inheritdoc />
        public async Task AddProjectAsync(Project project, CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation("Entered method ProjectService.AddProjectAsync().");

            project.ThrowIfNull();

            ValidationResult result = await this.validator.ValidateAsync(project, cancellationToken).ConfigureAwait(false);
            if (!result.IsValid)
            {
                this.logger.LogError("The project failed validation.", result.Errors);
                throw new ValidationException(result.Errors);
            }

            await this.projectRepository.AddProjectAsync(project, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IList<Project>> GetProjectsForUserAsync(CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation("Entered method ProjectService.GetProjectsForUserAsync().");

            cancellationToken.ThrowIfCancellationRequested();

            // TODO: obtain user id from system
            int userId = 1;

            IEnumerable<Project> projects = await this.projectRepository.GetProjectsForUserAsync(userId, cancellationToken).ConfigureAwait(false);

            return projects.ToList();
        }

        /// <inheritdoc />
        public async Task<IDictionary<int, string>> GetActiveProjectOwnersAsync(CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation("Entered method ProjectService.GetActiveProjectOwnersAsync().");

            cancellationToken.ThrowIfCancellationRequested();

            return await this.projectRepository.GetActiveProjectOwnersAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IDictionary<int, string>> GetProjectStatusesAsync(CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation("Entered method ProjectService.GetProjectStatusesAsync().");

            cancellationToken.ThrowIfCancellationRequested();

            return await this.projectRepository.GetProjectStatusesAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IDictionary<int, string>> GetProjectCategoriesAsync(CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation("Entered method ProjectService.GetProjectCategoriesAsync().");

            cancellationToken.ThrowIfCancellationRequested();

            return await this.projectRepository.GetProjectCategoriesAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IList<Project>> SearchProjectsAsync(string name, CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation("Entered method ProjectService.SearchProjectsAsync().");

            name.ThrowIfNull();

            // TODO: obtain user id from system
            int userId = 2;

            return await this.projectRepository.SearchProjectsAsync(userId, name, cancellationToken).ConfigureAwait(false);
        }
    }
}
