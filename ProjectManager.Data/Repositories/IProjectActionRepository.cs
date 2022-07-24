namespace ProjectManager.Data.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;

    using ProjectManager.Data.Models;

    /// <summary>
    ///     Handles data operations for project actions.
    /// </summary>
    public interface IProjectActionRepository
    {
        /// <summary>
        ///     Saves the specified project action to the database.
        /// </summary>
        /// <param name="projectAction">The project action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> that completed saving the action.</returns>
        Task SaveActionAsync(ProjectAction projectAction, CancellationToken cancellationToken = default);
    }
}
