namespace ProjectManager.MVC.Models
{
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;

	using ProjectManager.Data.Models;

	/// <summary>
	///     Represents the action view model.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class ActionViewModel
	{
		/// <summary>
		///     Gets or sets the actions.
		/// </summary>
		public IList<ProjectAction> Actions { get; set; }

		/// <summary>
		///     Gets or sets the action owners.
		/// </summary>
		public IList<ActionOwner> Owners { get; set; }

		/// <summary>
		///     Gets or sets the action statuses.
		/// </summary>
		public IDictionary<int, string> Statuses { get; set; }

		/// <summary>
		///		Gets or sets the action priorities.
		/// </summary>
		public IDictionary<int, string> Priorities { get; set; }

		/// <summary>
		///		Gets or sets the projects.
		/// </summary>
		public IDictionary<int, string> Projects { get; set; }
	}
}
