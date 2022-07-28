namespace ProjectManager.MVC.Controllers
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	using ProjectManager.Business.Services;
	using ProjectManager.Data.Models;
	using ProjectManager.MVC.Models;

	/// <summary>
	///     Handles requests for the projects page.
	/// </summary>
	public sealed class ActionsController : Controller
	{
		/// <summary>
		///     The logger.
		/// </summary>
		private readonly ILogger<ActionsController> _logger;

		/// <summary>
		///     The project action service.
		/// </summary>
		private readonly IProjectActionService _projectActionService;

		/// <summary>
		///     THe cancellation token source.
		/// </summary>
		private readonly CancellationTokenSource _cancellationTokenSource;

		/// <summary>
		///     Creates a new instance of the <see cref="ActionsController"/> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public ActionsController(ILogger<ActionsController> logger, IProjectActionService actionsService)
		{
			this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this._projectActionService = actionsService ?? throw new ArgumentNullException(nameof(actionsService));
			this._cancellationTokenSource = new CancellationTokenSource();
		}

		/// <summary>
		///     Redirects to the actions page.
		/// </summary>
		/// <returns>An <see cref="IActionResult"/> representing the actions page.</returns>
		[HttpGet]
		public async Task<IActionResult> Actions()
		{
			this._logger.LogInformation("Entered GET method Acions().");

			ActionViewModel actionViewModel = new ActionViewModel
			{
				Actions = await this._projectActionService.GetRecentActiveActionsAsync(cancellationToken: this._cancellationTokenSource.Token).ConfigureAwait(false),
				Owners = await this._projectActionService.GetActiveActionOwnersAsync(this._cancellationTokenSource.Token).ConfigureAwait(false),
				Statuses = await this._projectActionService.GetActionStatusesAsync(this._cancellationTokenSource.Token).ConfigureAwait(false),
				Priorities = await this._projectActionService.GetActionPrioritiesAsync(this._cancellationTokenSource.Token).ConfigureAwait(false)
			};

			return this.View(actionViewModel);
		}

		/// <summary>
		///     Obtains the actions form submission and redirects back to actions.
		/// </summary>
		/// <returns>An <see cref="IActionResult"/> representing the actions page.</returns>
		[HttpPost]
		public async Task<IActionResult> AddAction(ProjectAction projectAction)
		{
			this._logger.LogInformation("Entered POST method Acions().");

			if (projectAction == null)
			{
				throw new ArgumentNullException(nameof(projectAction));
			}

			if (!this.ModelState.IsValid)
			{
				this._logger.LogInformation("The model state is invalid. Redirecting back to Actions.");

				return this.RedirectToAction("Actions");
			}

			// TODO: await this._projectActionService.ValidateAsync(projectAction).ConfigureAwait(false);

			await this._projectActionService.AddProjectActionAsync(projectAction, this._cancellationTokenSource.Token).ConfigureAwait(false);

			this.ViewBag.Success = $"Added action {projectAction.Description}";

			return this.View();
		}
	}
}
