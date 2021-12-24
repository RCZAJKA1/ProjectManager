namespace ProjectManager.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

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
        ///     Creates a new instance of the <see cref="ProjectService"/> class.
        /// </summary>
        /// <param name="projectRepository">The project repository.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectService(ILogger<ProjectService> logger, IProjectRepository projectRepository)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
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
    }
}
