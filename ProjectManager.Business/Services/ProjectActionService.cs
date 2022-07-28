namespace ProjectManager.Business.Services
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Threading;
	using System.Threading.Tasks;

	using FluentValidation;
	using FluentValidation.Results;

	using Microsoft.Extensions.Logging;

	using ProjectManager.Data.Models;
	using ProjectManager.Data.Repositories;

	/// <inheritdoc cref="IProjectActionService">
	public sealed class ProjectActionService : IProjectActionService
	{
		/// <summary>
		///     The logger.
		/// </summary>
		private readonly ILogger<ProjectActionService> _logger;

		/// <summary>
		///     The project action validator.
		/// </summary>
		private readonly IValidator<ProjectAction> _validator;

		/// <summary>
		///     The project action repository.
		/// </summary>
		private readonly IProjectActionRepository _actionRepository;

		/// <summary>
		///     Creates a new instance of the <see cref="ProjectActionService"/> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="validator">The project action validator.</param>
		/// <param name="repository">The project action repository.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public ProjectActionService(ILogger<ProjectActionService> logger, IValidator<ProjectAction> validator, IProjectActionRepository repository)
		{
			this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
			this._actionRepository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		/// <inheritdoc/>
		public async Task AddProjectActionAsync(ProjectAction projectAction, CancellationToken cancellationToken = default)
		{
			this._logger.LogInformation("Entered method AddProjectActionAsync().");

			if (projectAction == null)
			{
				throw new ArgumentNullException(nameof(projectAction));
			}

			// TODO: convert date time timezones

			ValidationResult result = await this._validator.ValidateAsync(projectAction, cancellationToken).ConfigureAwait(false);
			if (!result.IsValid)
			{
				this._logger.LogError("The action failed validation.", result.Errors);
				throw new ValidationException(result.Errors);
			}

			await this._actionRepository.SaveActionAsync(projectAction, cancellationToken);

			Debug.WriteLine("Finished saving the action.");
		}

		/// <inheritdoc/>
		public async Task<IList<ProjectAction>> GetRecentActiveActionsAsync(int actionCount = 20, CancellationToken cancellationToken = default)
		{
			this._logger.LogInformation("Entered method GetRecentActiveActionsAsync().");

			if (actionCount < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(actionCount));
			}

			IList<ProjectAction> recentActions = await this._actionRepository.GetRecentActiveActionsAsync(actionCount, cancellationToken).ConfigureAwait(false);

			return recentActions;
		}

		/// <inheritdoc/>
		public async Task<IList<ActionOwner>> GetActiveActionOwnersAsync(CancellationToken cancellationToken = default)
		{
			this._logger.LogInformation("Entered method GetActiveActionOwnersAsync().");

			return await this._actionRepository.GetActiveActionOwnersAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}
