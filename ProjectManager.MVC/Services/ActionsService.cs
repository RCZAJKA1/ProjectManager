namespace ProjectManager.MVC.Services
{
    using FluentValidation;
    using Microsoft.Extensions.Logging;
    using ProjectManager.MVC.Models;
    using System.Threading.Tasks;
    using System;
    using FluentValidation.Results;
    using System.Diagnostics;

    /// <inheritdoc cref="IActionsService">
    public sealed class ActionsService : IActionsService
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="ActionsService"/> class.
        /// </summary>
        public ActionsService(ILogger<ActionsService> logger, IValidator<ActionViewModel> validator)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.Validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        internal ILogger<ActionsService> Logger { get; }

        /// <summary>
        ///     Gets the actions validator.
        /// </summary>
        internal IValidator<ActionViewModel> Validator { get; }

        /// <inheritdoc/>
        public async Task SaveActionAsync(ActionViewModel actionViewModel)
        {
            if (actionViewModel == null)
            {
                throw new ArgumentNullException(nameof(actionViewModel));
            }

            ValidationResult result = await this.Validator.ValidateAsync(actionViewModel);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            Debug.WriteLine("Finished saving the action.");
        }
    }
}
