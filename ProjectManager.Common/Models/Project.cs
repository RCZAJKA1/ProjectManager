namespace ProjectManager.Common.Models
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
        ///     Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     Gets or sets the owner.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        ///     Gets or sets the start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     Gets or sets the end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        ///     Gets or sets the due date.
        /// </summary>
        public DateTime DueDate { get; set; }
    }
}
