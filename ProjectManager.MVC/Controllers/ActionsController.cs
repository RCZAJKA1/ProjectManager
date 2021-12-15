namespace ProjectManager.MVC.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using ProjectManager.MVC.Models;
    using ProjectManager.MVC.Services;

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
        public ActionsController(ILogger<ActionsController> logger, IActionsService actionsService)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.ActionsService = actionsService ?? throw new ArgumentNullException(nameof(actionsService));
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        internal ILogger<ActionsController> Logger { get; }

        /// <summary>
        ///     Gets the actions service.
        /// </summary>
        internal IActionsService ActionsService { get; }

        /// <summary>
        ///     Redirects to the actions page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the actions page.</returns>
        [HttpGet]
        public async Task<IActionResult> Actions()
        {
            return await Task.FromResult(this.View());
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

                return await Task.FromResult(this.RedirectToAction("Actions"));
            }

            await this.ActionsService.SaveActionAsync(actionViewModel).ConfigureAwait(false);

            this.ViewBag.Success = this.ModelState.IsValid ? $"Added action {actionViewModel.Description}" : $"{string.Join(',', this.ModelState.Values)}";

            return await Task.FromResult(this.View());
        }
    }
}
