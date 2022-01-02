namespace ProjectManager.Business.Validators
{

    using FluentValidation;

    using ProjectManager.Common.Models;

    /// <summary>
    ///     The validator for the project.
    /// </summary>
    public sealed class ProjectValidator : AbstractValidator<Project>
    {
        ///// <summary>
        /////     The min date.
        ///// </summary>
        //private static readonly DateTime MinDate = new DateTime(1900, 1, 1);

        ///// <summary>
        /////     The current date.
        ///// </summary>
        //private static readonly DateTime CurrentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

        ///// <summary>
        /////     Defines the validation rules for the project.
        ///// </summary>
        //public ProjectValidator()
        //{
        //    // Required
        //    this.RuleFor(x => x.Owner).NotEmpty()
        //        .DependentRules(() =>
        //        {
        //            this.RuleFor(x => x.Owner).Must(MustBeValidName).WithMessage("The Owner must only contain letters.");
        //            this.RuleFor(x => x.Owner.Length).LessThanOrEqualTo(30);
        //        });
        //    this.RuleFor(x => x.Description).NotEmpty()
        //        .DependentRules(() =>
        //        {
        //            this.RuleFor(x => x.Description.Length).LessThanOrEqualTo(255);
        //        });

        //    // Nullable
        //    this.When(x => x.Id.HasValue, () =>
        //    {
        //        this.RuleFor(x => x.Id.Value).GreaterThan(0);
        //    });
        //    this.When(x => x.DateDue.HasValue, () =>
        //    {
        //        this.RuleFor(x => x.DateDue.Value).GreaterThanOrEqualTo(MinDate);
        //    });
        //    this.When(x => x.DateOpened.HasValue, () =>
        //    {
        //        this.RuleFor(x => x.DateOpened.Value).GreaterThanOrEqualTo(MinDate);
        //        this.RuleFor(x => x.DateOpened.Value).LessThanOrEqualTo(CurrentDate);
        //    });
        //    this.When(x => x.DateClosed.HasValue, () =>
        //    {
        //        this.RuleFor(x => x.DateOpened).NotEmpty().WithMessage("The DateOpened must exist if the DateClosed exists.");
        //        this.RuleFor(x => x.DateClosed.Value).LessThanOrEqualTo(CurrentDate);
        //    });
        //    this.When(x => x.DateClosed.HasValue && x.DateOpened.HasValue, () =>
        //    {
        //        this.RuleFor(x => new { x.DateClosed, x.DateOpened }).Must(y => MustBeGreaterThanOrEqualToDateOpened(y.DateClosed.Value, y.DateOpened.Value)).WithMessage("The DateClosed must occur on or after the DateOpened.");
        //    });
        //}

        ///// <summary>
        /////     Ensures the date closed occurred on or after the date opened.
        ///// </summary>
        ///// <param name="dateClosed">The date closed.</param>
        ///// <param name="dateOpened">The date opened.</param>
        ///// <returns><c>true</c> if the date closed occurred on or after the date opened, otherwise <c>false</c>.</returns>
        //private static bool MustBeGreaterThanOrEqualToDateOpened(DateTime dateClosed, DateTime dateOpened)
        //{
        //    return dateClosed >= dateOpened;
        //}

        ///// <summary>
        /////     Ensures the name only contains valid characters.
        ///// </summary>
        ///// <param name="name">The name.</param>
        ///// <returns><c>true</c> if the name only contains valid characters, otherwise <c>false</c>.</returns>
        //private static bool MustBeValidName(string name)
        //{
        //    Regex regex = new Regex(@"^[a-zA-Z\s]+$");
        //    return regex.IsMatch(name);
        //}
    }
}
