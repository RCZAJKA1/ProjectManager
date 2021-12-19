namespace ProjectManager.Business.Services
{
    using FluentValidation;
    using System.Threading.Tasks;
    using System;
    using FluentValidation.Results;
    using System.Diagnostics;

    using ProjectAction = Models.ProjectAction;
    using System.Threading;
    using Microsoft.Extensions.Logging;
    using ProjectManager.Data.Repositories;

    /// <inheritdoc cref="IProjectActionService">
    public sealed class ProjectActionService : IProjectActionService
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="ProjectActionService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="validator">The project action validator.</param>
        /// <param name="actionsRepository">The project action repository.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectActionService(ILogger<ProjectActionService> logger, IValidator<ProjectAction> validator, IProjectActionRepository repository)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.Validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        internal ILogger<ProjectActionService> Logger { get; }

        /// <summary>
        ///     Gets the project action validator.
        /// </summary>
        internal IValidator<ProjectAction> Validator { get; }

        /// <summary>
        ///     Gets the project action repository.
        /// </summary>
        internal IProjectActionRepository Repository { get; }

        /// <inheritdoc/>
        public async Task AddProjectActionAsync(ProjectAction projectAction, CancellationToken cancellationToken = default)
        {
            if (projectAction == null)
            {
                throw new ArgumentNullException(nameof(projectAction));
            }

            ValidationResult result = await this.Validator.ValidateAsync(projectAction);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            //await this.Repository.SaveActionAsync(projectAction, cancellationToken);

            Debug.WriteLine("Finished saving the action.");
        }
    }
}
