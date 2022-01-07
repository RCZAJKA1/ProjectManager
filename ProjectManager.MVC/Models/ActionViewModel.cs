namespace ProjectManager.MVC.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using ProjectManager.Business.Enums;

    /// <summary>
    ///     Represents the action view model.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class ActionViewModel
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        ///     Gets or sets the opened date.
        /// </summary>
        public DateTime? DateOpened { get; set; }

        /// <summary>
        ///     Gets or sets the due date.
        /// </summary>
        public DateTime? DateDue { get; set; }

        /// <summary>
        ///     Gets or sets the closed date.
        /// </summary>
        public DateTime? DateClosed { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        public ActionStatus Status { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the priority.
        /// </summary>
        public ActionPriority Priority { get; set; }

        /// <summary>
        ///     Gets or sets the owner.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        ///     Gets or sets the resolution.
        /// </summary>
        public string Resolution { get; set; }
    }
}
