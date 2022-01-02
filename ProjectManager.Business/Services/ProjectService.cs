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

    using ProjectManager.Common.Models;
    using ProjectManager.Data.Repositories;

    /// <inheritdoc cref="IProjectService"/>
    public sealed class ProjectService : IProjectService
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<ProjectService> _logger;

        /// <summary>
        ///     The project repository.
        /// </summary>
        private readonly IProjectRepository _projectRepository;

        /// <summary>
        ///     The project validator.
        /// </summary>
        private readonly IValidator<Project> _validator;

        /// <summary>
        ///     Creates a new instance of the <see cref="ProjectService"/> class.
        /// </summary>
        /// <param name="projectRepository">The project repository.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectService(ILogger<ProjectService> logger, IProjectRepository projectRepository, IValidator<Project> validator)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <inheritdoc />
        public async Task AddProjectAsync(Project project, CancellationToken cancellationToken = default)
        {
            this._logger.LogInformation("Entered method AddProjectAsync().");

            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            ValidationResult result = await this._validator.ValidateAsync(project, cancellationToken).ConfigureAwait(false);
            if (!result.IsValid)
            {
                this._logger.LogError("The project failed validation.", result.Errors);
                throw new ValidationException(result.Errors);
            }

            await this._projectRepository.AddProjectAsync(project, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IList<Project>> GetProjectsForUserAsync(CancellationToken cancellationToken = default)
        {
            this._logger.LogInformation("Entered method GetProjectsForUserAsync().");

            cancellationToken.ThrowIfCancellationRequested();

            int userId = 1; // stubbed in for known user id

            IEnumerable<Project> projects = await this._projectRepository.GetProjectsForUserAsync(userId, cancellationToken).ConfigureAwait(false);

            return projects.ToList();
        }

        /// <inheritdoc />
        public async Task<IDictionary<int, string>> GetActiveProjectOwnersAsync(CancellationToken cancellationToken = default)
        {
            this._logger.LogInformation("Entered method GetActiveProjectOwnersAsync().");

            cancellationToken.ThrowIfCancellationRequested();

            return await this._projectRepository.GetActiveProjectOwnersAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IDictionary<int, string>> GetProjectStatusesAsync(CancellationToken cancellationToken = default)
        {
            this._logger.LogInformation("Entered method GetProjectStatusesAsync().");

            cancellationToken.ThrowIfCancellationRequested();

            return await this._projectRepository.GetProjectStatusesAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IDictionary<int, string>> GetProjectCategoriesAsync(CancellationToken cancellationToken = default)
        {
            this._logger.LogInformation("Entered method GetProjectCategoriesAsync().");

            cancellationToken.ThrowIfCancellationRequested();

            return await this._projectRepository.GetProjectCategoriesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
