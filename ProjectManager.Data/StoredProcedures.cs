namespace ProjectManager.Data
{
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	///     The list of all stored procedure names.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal static class StoredProcedures
	{
		/// <summary>
		///    [dbo].[usp_GetAllProjectsForUser]
		/// </summary>
		public static string GetAllProjectsForUser = "[dbo].[usp_GetAllProjectsForUser]";

		/// <summary>
		///     [dbo].[usp_GetRecentActiveProjects]
		/// </summary>
		public static string GetRecentActiveProjects = "[dbo].[usp_GetRecentActiveProjects]";

		/// <summary>
		///     [dbo].[usp_GetRecentActiveActions]
		/// </summary>
		public static string GetRecentActiveActions = "[dbo].[usp_GetRecentActiveActions]";

		/// <summary>
		///     [dbo].[usp_InsertProject]
		/// </summary>
		public static string InsertProject = "[dbo].[usp_InsertProject]";

		/// <summary>
		///     [dbo].[usp_GetActiveProjectOwners]
		/// </summary>
		public static string GetActiveProjectOwners = "[dbo].[usp_GetActiveProjectOwners]";

		/// <summary>
		///     [dbo].[usp_SearchProjectsForOwner]
		/// </summary>
		public static string SearchProjectsForOwner = "[dbo].[usp_SearchProjectsForOwner]";
	}
}
