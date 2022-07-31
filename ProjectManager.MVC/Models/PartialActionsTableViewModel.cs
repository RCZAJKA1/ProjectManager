namespace ProjectManager.MVC.Models
{
	using System.Collections.Generic;

	using ProjectManager.Data.Models;

	/// <summary>
	///		The view model for the partial actions table view.
	/// </summary>
	public sealed class PartialActionsTableViewModel
	{
		/// <summary>
		///		Gets or sets the actions.
		/// </summary>
		public IList<ProjectAction> Actions { get; set; }
	}
}
