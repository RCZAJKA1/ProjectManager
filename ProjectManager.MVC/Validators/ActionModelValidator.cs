namespace ProjectManager.MVC.Validators
{
using FluentValidation;
using ProjectManager.MVC.Models;
using System.Text.RegularExpressions;
using System;

/// <summary>
///     The validator for the action model.
/// </summary>
    internal sealed class ActionModelValidator : AbstractValidator<ActionViewModel>
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
        ///     Defines the validation rules for the action model.
        /// </summary>
        public ActionModelValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            this.RuleFor(x => x.Owner).NotEmpty().Must(MustBeValidName).WithMessage("The owner must only contain letters.")
                .DependentRules(() =>
                {
                    this.RuleFor(x => x.Owner.Length).LessThanOrEqualTo(30);
                });
            this.RuleFor(x => x.Description).NotEmpty()
                .DependentRules(() =>
                {
                    this.RuleFor(x => x.Description.Length).LessThanOrEqualTo(255);
                });
            // TODO: fix - doens't have to be opened or closed or due when created
            this.RuleFor(x => x.DateOpened).NotEmpty().GreaterThanOrEqualTo(this.MinDateLocal).LessThanOrEqualTo(DateTime.Now)
                .DependentRules(() =>
                {
                    this.RuleFor(x => new { x.DateOpened, x.DateClosed }).Must(y => MustBeLessThanOrEqualToDateClosed(y.DateOpened, y.DateClosed));
                });
        }

        /// <summary>
        ///     Ensures the date opened occurred on or before the date closed.
        /// </summary>
        /// <param name="dateOpened">The date opened.</param>
        /// <param name="dateClosed">The date closed.</param>
        /// <returns><c>true</c> if the date opened occurred on or before the date opened, otherwise <c>false</c>.</returns>
        private static bool MustBeLessThanOrEqualToDateClosed(DateTime dateOpened, DateTime dateClosed)
        {
            return dateOpened <= dateClosed;
        }

        /// <summary>
        ///     Ensures the name only contains valid characters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if the name only contains valid characters, otherwise <c>false</c>.</returns>
        private static bool MustBeValidName(string name)
        {
            Regex regex = new Regex(@"^[a-z][A-Z]+$");
            return regex.IsMatch(name);
        }
    }
}
