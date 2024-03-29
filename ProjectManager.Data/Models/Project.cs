﻿namespace ProjectManager.Data.Models
{
	using System;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	///     Represents a project.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class Project
	{
		/// <summary>
		///     Gets or sets the identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Gets or sets the description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		///     Gets or sets the status identifier.
		/// </summary>
		public int StatusId { get; set; }

		/// <summary>
		///     Gets or sets the status.
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		///     Gets or sets the owner identifier.
		/// </summary>
		public int OwnerId { get; set; }

		/// <summary>
		///     Gets or sets the owner first name.
		/// </summary>
		public string OwnerFirstName { get; set; }

		/// <summary>
		///     Gets or sets the owner last name.
		/// </summary>
		public string OwnerLastName { get; set; }

		/// <summary>
		///     Gets or sets the category identifier.
		/// </summary>
		public int CategoryId { get; set; }

		/// <summary>
		///     Gets or sets the category.
		/// </summary>
		public string Category { get; set; }

		/// <summary>
		///     Gets or sets the start date.
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		///     Gets or sets the end date.
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		///     Gets or sets the due date.
		/// </summary>
		public DateTime? DueDate { get; set; }
	}
}
