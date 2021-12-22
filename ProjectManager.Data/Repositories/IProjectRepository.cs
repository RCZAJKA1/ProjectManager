namespace ProjectManager.Data.Repositories
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
        /// <returns>All projects tied to the specified user.</returns>
        Task<IEnumerable<Project>> GetProjectsForUserAsync(int userId, CancellationToken cancellationToken = default);
    }
}
