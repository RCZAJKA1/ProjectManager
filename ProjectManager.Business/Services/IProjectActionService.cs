namespace ProjectManager.Business.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    using ProjectManager.Business.Models;

    /// <summary>
    ///     Handles business operations for project actions.
    /// </summary>
    public interface IProjectActionService
    {
        /// <summary>
        ///     Adds a project action.
        /// </summary>
        /// <param name="projectAction">The project action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> that completed adding the project action.</returns>
        Task AddProjectActionAsync(ProjectAction projectAction, CancellationToken cancellationToken = default);
    }
}
