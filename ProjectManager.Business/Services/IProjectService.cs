namespace ProjectManager.Business.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using ProjectManager.Common.Models;

    /// <summary>
    ///     Handles business operations for projects.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        ///     Gets all projects tied to the current user.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>All projects tied to the current user.</returns>
        Task<IList<Project>> GetProjectsForUserAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///      Adds the specified project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> that completed adding the new project.</returns>
        Task AddProjectAsync(Project project, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets all active project owners.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="IDictionary{TKey, TValue}"/> containing the identifiers and names of each active project owner.</returns>
        Task<IDictionary<int, string>> GetActiveProjectOwnersAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets all project statuses.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="IDictionary{TKey, TValue}"/> contianing the identifiers and names of each project status.</returns>
        Task<IDictionary<int, string>> GetProjectStatusesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets all project categories.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="IDictionary{TKey, TValue}"/> contianing the identifiers and names of each project category.</returns>
        Task<IDictionary<int, string>> GetProjectCategoriesAsync(CancellationToken cancellationToken = default);
    }
}
