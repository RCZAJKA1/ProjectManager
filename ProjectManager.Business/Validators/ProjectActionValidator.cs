namespace ProjectManager.Business.Validators
{
    using System;
    using System.Text.RegularExpressions;

    using FluentValidation;

    using ProjectManager.Business.Models;

    /// <summary>
    ///     The validator for the project action.
    /// </summary>
    public sealed class ProjectActionValidator : AbstractValidator<ProjectAction>
    {
        /// <summary>
        ///     The min date in UTC.
        /// </summary>
        private static readonly DateTime MinDateUtc = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///     The min date in local time.
        /// </summary>
        private readonly DateTime MinDateLocal = TimeZoneInfo.ConvertTimeFromUtc(MinDateUtc, TimeZoneInfo.Local);

        /// <summary>
        ///     Defines the validation rules for the project action.
        /// </summary>
        public ProjectActionValidator()
        {
            this.When(x => x.Id.HasValue, () =>
            {
                this.RuleFor(x => x.Id.Value).NotEmpty().GreaterThan(0);
            });

            this.RuleFor(x => x.Owner).NotEmpty()
                .DependentRules(() =>
                {
                    this.RuleFor(x => x.Owner).Must(MustBeValidName).WithMessage("The Owner must only contain letters.");
                    this.RuleFor(x => x.Owner.Length).LessThanOrEqualTo(30);
                });

            this.RuleFor(x => x.Description).NotEmpty()
                .DependentRules(() =>
                {
                    this.RuleFor(x => x.Description.Length).LessThanOrEqualTo(255);
                });

            this.When(x => x.DateOpened.HasValue, () =>
            {
                this.RuleFor(x => x.DateOpened.Value).NotEmpty().GreaterThanOrEqualTo(this.MinDateLocal);
            });

            this.When(x => x.DateClosed.HasValue, () =>
            {
                this.RuleFor(x => x.DateOpened.Value).NotEmpty().WithMessage("The DateOpened must exist if the DateClosed exists.");
                this.RuleFor(x => new { x.DateOpened, x.DateClosed }).Must(y => MustBeGreaterThanOrEqualToDateOpened(y.DateClosed.Value, y.DateOpened.Value));
            });
        }

        /// <summary>
        ///     Ensures the date closed occurred on or after the date opened.
        /// </summary>
        /// <param name="dateClosed">The date closed.</param>
        /// <param name="dateOpened">The date opened.</param>
        /// <returns><c>true</c> if the date closed occurred on or after the date opened, otherwise <c>false</c>.</returns>
        private static bool MustBeGreaterThanOrEqualToDateOpened(DateTime dateClosed, DateTime dateOpened)
        {
            return dateClosed >= dateOpened;
        }

        /// <summary>
        ///     Ensures the name only contains valid characters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if the name only contains valid characters, otherwise <c>false</c>.</returns>
        private static bool MustBeValidName(string name)
        {
            Regex regex = new Regex(@"^[a-zA-Z]+$");
            return regex.IsMatch(name);
        }
    }
}
