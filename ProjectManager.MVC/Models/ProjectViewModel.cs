namespace ProjectManager.MVC.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using ProjectManager.Data.Models;

    /// <summary>
    ///     Represents the action view model.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class ProjectViewModel
    {
        /// <summary>
        ///     Gets or sets the projects.
        /// </summary>
        public IList<Project> Projects { get; set; }

        /// <summary>
        ///     Gets or sets all active project owners.
        /// </summary>
        public IEnumerable<ProjectOwner> Owners { get; set; }

        /// <summary>
        ///     Gets or sets all available statuses.
        /// </summary>
        public IDictionary<int, string> Statuses { get; set; }

        /// <summary>
        ///     Gets or sets all available categories.
        /// </summary>
        public IDictionary<int, string> Categories { get; set; }
    }
}
