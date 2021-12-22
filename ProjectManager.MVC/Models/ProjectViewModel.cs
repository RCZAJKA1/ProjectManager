namespace ProjectManager.MVC.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using ProjectManager.Common.Models;

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
    }
}
