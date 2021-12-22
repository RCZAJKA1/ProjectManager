namespace ProjectManager.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using ProjectManager.Common.Models;
    using ProjectManager.Data.Repositories;

    /// <inheritdoc cref="IProjectService"/>
    public sealed class ProjectService : IProjectService
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="ProjectService"/> class.
        /// </summary>
        /// <param name="projectRepository">The project repository.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectService(IProjectRepository projectRepository)
        {
            this.ProjectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        /// <summary>
        ///     Gets the project repository.
        /// </summary>
        internal IProjectRepository ProjectRepository { get; }

        /// <inheritdoc />
        public async Task<IList<Project>> GetProjectsForUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            if (userId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            IEnumerable<Project> projects = await this.ProjectRepository.GetProjectsForUserAsync(userId, cancellationToken).ConfigureAwait(false);
            return projects.ToList();
        }
    }
}
