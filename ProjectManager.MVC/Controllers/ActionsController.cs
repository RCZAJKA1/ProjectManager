namespace ProjectManager.MVC.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using ProjectManager.Business.Models;
    using ProjectManager.Business.Services;
    using ProjectManager.MVC.Models;

    /// <summary>
    ///     Handles requests for the projects page.
    /// </summary>
    public sealed class ActionsController : Controller
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="ActionsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ActionsController(ILogger<ActionsController> logger, IProjectActionService actionsService)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.ActionsService = actionsService ?? throw new ArgumentNullException(nameof(actionsService));

            this.CancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        internal ILogger<ActionsController> Logger { get; }

        /// <summary>
        ///     Gets the actions service.
        /// </summary>
        internal IProjectActionService ActionsService { get; }

        /// <summary>
        ///     Gets the cancellation token source.
        /// </summary>
        private CancellationTokenSource CancellationTokenSource { get; }

        /// <summary>
        ///     Redirects to the actions page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the actions page.</returns>
        [HttpGet]
        public IActionResult Actions()
        {
            return this.View();
        }

        /// <summary>
        ///     Obtains the actions form submission and redirects back to actions.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the actions page.</returns>
        [HttpPost]
        public async Task<IActionResult> Actions(ActionViewModel actionViewModel)
        {
            if (actionViewModel == null)
            {
                throw new ArgumentNullException(nameof(actionViewModel));
            }

            if (!this.ModelState.IsValid)
            {
                this.Logger.LogInformation("The model state is invalid. Redirecting back to Actions.");

                return this.RedirectToAction("Actions");
            }

            ProjectAction action = new ProjectAction
            {
                DateOpened = actionViewModel.DateOpened,
                DateClosed = actionViewModel.DateClosed,
                DateDue = actionViewModel.DateDue,
                Owner = actionViewModel.Owner,
                Description = actionViewModel.Description,
                Priority = actionViewModel.Priority,
                Resolution = actionViewModel.Resolution,
                Status = actionViewModel.Status
            };

            await this.ActionsService.AddProjectActionAsync(action, this.CancellationTokenSource.Token).ConfigureAwait(false);

            this.ViewBag.Success = $"Added action {actionViewModel.Description}";

            return this.View();
        }
    }
}
