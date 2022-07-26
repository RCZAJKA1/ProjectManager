namespace ProjectManager.Data.Models
{
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	///     Represents an action owner.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class ActionOwner
	{
		/// <summary>
		///     Gets or sets the owner identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Gets or sets the first name.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		///     Gets or sets the last name.
		/// </summary>
		public string LastName { get; set; }
	}
}
