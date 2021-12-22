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
        ///     Gets all projects tied to the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>All projects tied to the specified user.</returns>
        Task<IList<Project>> GetProjectsForUserAsync(int userId, CancellationToken cancellationToken = default);
    }
}
