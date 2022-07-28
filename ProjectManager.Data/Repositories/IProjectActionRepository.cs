namespace ProjectManager.Data.Repositories
{
	using System.Collections.Generic;
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

		/// <summary>
		///     Gets the recent active actions.
		/// </summary>
		/// <param name="actionCount">The amount of actions to return.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A list of recent active <see cref="ProjectAction"/>.</returns>
		Task<IList<ProjectAction>> GetRecentActiveActionsAsync(int actionCount, CancellationToken cancellationToken = default);

		/// <summary>
		///		Gets all active action owners.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A <see cref="IList{T}"/> contiaining the action owners.</returns>
		Task<IList<ActionOwner>> GetActiveActionOwnersAsync(CancellationToken cancellationToken = default);

		/// <summary>
		///		Gets all action statuses.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A dictionary of action statuses.</returns>
		Task<IDictionary<int, string>> GetActionStatusesAsync(CancellationToken cancellationToken = default);

		/// <summary>
		///		Gets all action priorities.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A dictionary of action priorities.</returns>
		Task<IDictionary<int, string>> GetActionPrioritiesAsync(CancellationToken cancellationToken = default);
	}
}
