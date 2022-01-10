namespace ProjectManager.MVC.Models
{
    using System.Collections.Generic;

    using ProjectManager.Common.Models;

    /// <summary>
    ///     Represents the project search view model.
    /// </summary>
    public sealed class ProjectSearchViewModel
    {
        /// <summary>
        ///     Gets or sets the projects.
        /// </summary>
        public IEnumerable<Project> Projects { get; set; }
    }
}
