namespace ProjectManager.Data.Models
{
	using System;
	using System.Diagnostics.CodeAnalysis;

	using ProjectManager.Data.Enums;

	/// <summary>
	///		The options used to filter project actions.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class ActionSearchOptions
	{
		/// <summary>
		///     Gets or sets the project identifier.
		/// </summary>
		public int ProjectId { get; set; }

		/// <summary>
		///     Gets or sets the opened date.
		/// </summary>
		public DateTime? DateOpened { get; set; }

		/// <summary>
		///     Gets or sets the due date.
		/// </summary>
		public DateTime? DateDue { get; set; }

		/// <summary>
		///     Gets or sets the closed date.
		/// </summary>
		public DateTime? DateClosed { get; set; }

		/// <summary>
		///     Gets or sets the status.
		/// </summary>
		public ActionStatus Status { get; set; }

		/// <summary>
		///     Gets or sets the description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		///     Gets or sets the priority.
		/// </summary>
		public ActionPriority Priority { get; set; }

		/// <summary>
		///     Gets or sets the owner identifier.
		/// </summary>
		public int OwnerId { get; set; }

		/// <summary>
		///     Gets or sets the resolution.
		/// </summary>
		public string Resolution { get; set; }
	}
}
