namespace ProjectManager.Business.Validators
{
	using System;
	using System.Text.RegularExpressions;

	using FluentValidation;

	using ProjectManager.Data.Models;

	/// <summary>
	///     The validator for the project action.
	/// </summary>
	public sealed class ProjectActionValidator : AbstractValidator<ProjectAction>
	{
		/// <summary>
		///     The min date.
		/// </summary>
		private static readonly DateTime MinDate = new DateTime(1900, 1, 1);

		/// <summary>
		///     The current date.
		/// </summary>
		private static readonly DateTime CurrentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

		/// <summary>
		///     Defines the validation rules for the project action.
		/// </summary>
		public ProjectActionValidator()
		{
			// Nullable
			this.When(x => x.Id.HasValue, () =>
			{
				this.RuleFor(x => x.Id.Value).GreaterThan(0);
			});

			this.When(x => !string.IsNullOrEmpty(x.Description), () =>
			{
				this.RuleFor(x => x.Description.Length).LessThanOrEqualTo(255);
				this.RuleFor(x => x.Description).Must(MustBeAlphanumeric);
			});

			this.RuleFor(x => x.DateOpened.Value)
				.NotNull()
				.GreaterThanOrEqualTo(MinDate)
				.LessThanOrEqualTo(CurrentDate);

			this.When(x => x.DateDue.HasValue, () =>
			{
				this.RuleFor(x => x.DateDue.Value).GreaterThanOrEqualTo(MinDate);
			});

			this.When(x => x.DateClosed.HasValue, () =>
			{
				this.RuleFor(x => x.DateClosed.Value)
					.GreaterThanOrEqualTo(x => x.DateOpened)
					.WithMessage("The DateClosed must occur on or after the DateOpened.");
			});

			this.RuleFor(x => x.ProjectId).NotEmpty().GreaterThan(0);
			this.RuleFor(x => x.OwnerId).NotEmpty().GreaterThan(0);
			this.RuleFor(x => x.Status).NotEmpty();
			this.RuleFor(x => (int)x.Priority).NotEmpty().GreaterThan(0);

			this.When(x => !string.IsNullOrEmpty(x.Resolution), () =>
			{
				this.RuleFor(x => x.Resolution.Length).LessThanOrEqualTo(255);
				this.RuleFor(x => x.Resolution).Must(MustBeAlphanumeric);
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
			Regex regex = new Regex(@"^[a-zA-Z\s]+$");
			return regex.IsMatch(name);
		}

		/// <summary>
		///     Ensures the value only contains alphanumeric characters.
		/// </summary>
		/// <param name="str">The string.</param>
		/// <returns><c>true</c> if the name only contains valid alphanumeric characters, otherwise <c>false</c>.</returns>
		private static bool MustBeAlphanumeric(string str)
		{
			Regex regex = new Regex(@"^[a-zA-Z0-9\s]+$");
			return regex.IsMatch(str);
		}
	}
}
