namespace ProjectManager.Business.Services
{
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	using ProjectManager.Data.Models;

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

		/// <summary>
		///     Gets the most recent active actions.
		/// </summary>
		/// <param name="actionCount">The amount of actions to return.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A list of the most recent active <see cref="ProjectAction"/>.</returns>
		Task<IList<ProjectAction>> GetRecentActiveActionsAsync(int actionCount = 20, CancellationToken cancellationToken = default);

		/// <summary>
		///		Gets the active action owners.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A list of active <see cref="ActionOwner"/>.</returns>
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

		/// <summary>
		///		Gets all actions using the specified search options.
		/// </summary>
		/// <param name="actionSearchOptions">The action search options.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A list of <see cref="ProjectAction"/> that meet the specified search critieria.</returns>
		Task<IList<ProjectAction>> GetActionsAsync(ActionSearchOptions actionSearchOptions, CancellationToken cancellationToken = default);
	}
}
