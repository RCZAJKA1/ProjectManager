﻿namespace ProjectManager.Data.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using ProjectManager.Common.Models;

    /// <summary>
    ///     Handles data operations for project actions.
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        ///     Gets all projects tied to the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the projects tied to the specified user.</returns>
        Task<IEnumerable<Project>> GetProjectsForUserAsync(int userId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Searches for projects using the specified criteria.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="projectName">The project name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An <see cref="IList{T}"/> containing the projects that meet the specified criteria.</returns>
        Task<IList<Project>> SearchProjectsAsync(int userId, string projectName, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Adds the specified project to the database.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> that completed adding the project to the database.</returns>
        Task AddProjectAsync(Project project, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets all active project owners.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="IDictionary{TKey, TValue}"/> contianing the identifiers and names of each project owner.</returns>
        Task<IDictionary<int, string>> GetActiveProjectOwnersAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets all available project statuses.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="IDictionary{TKey, TValue}"/> contianing the identifiers and names of each project status.</returns>
        Task<IDictionary<int, string>> GetProjectStatusesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets all available project categories.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="IDictionary{TKey, TValue}"/> contianing the identifiers and names of each project category.</returns>
        Task<IDictionary<int, string>> GetProjectCategoriesAsync(CancellationToken cancellationToken = default);
    }
}
