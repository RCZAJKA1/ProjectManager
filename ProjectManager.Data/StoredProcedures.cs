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

		/// <summary>
		///     [dbo].[usp_GetActiveActionOwners]
		/// </summary>
		public static string GetActiveActionOwners = "[dbo].[usp_GetActiveActionOwners]";

		/// <summary>
		///     [dbo].[usp_GetActionStatuses]
		/// </summary>
		public static string GetActionStatuses = "[dbo].[usp_GetActionStatuses]";

		/// <summary>
		///     [dbo].[usp_GetActionPriorities]
		/// </summary>
		public static string GetActionPriorities = "[dbo].[usp_GetActionPriorities]";

		/// <summary>
		///     [dbo].[usp_GetActionPriorities]
		/// </summary>
		public static string InsertAction = "[dbo].[usp_InsertAction]";

		/// <summary>
		///     [dbo].[usp_GetActions]
		/// </summary>
		public static string GetActions = "[dbo].[usp_GetActions]";
	}
}
