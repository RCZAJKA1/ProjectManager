﻿namespace ProjectManager.MVC.Controllers
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
        public IActionResult Actions()
        {
            this._logger.LogInformation("Entered GET method Acions().");

            return this.View();
        }

        /// <summary>
        ///     Obtains the actions form submission and redirects back to actions.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the actions page.</returns>
        [HttpPost]
        public async Task<IActionResult> Actions(ActionViewModel actionViewModel)
        {
            this._logger.LogInformation("Entered POST method Acions().");

            if (actionViewModel == null)
            {
                throw new ArgumentNullException(nameof(actionViewModel));
            }

            if (!this.ModelState.IsValid)
            {
                this._logger.LogInformation("The model state is invalid. Redirecting back to Actions.");

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

            await this._projectActionService.AddProjectActionAsync(action, this._cancellationTokenSource.Token).ConfigureAwait(false);

            this.ViewBag.Success = $"Added action {actionViewModel.Description}";

            return this.View();
        }
    }
}
