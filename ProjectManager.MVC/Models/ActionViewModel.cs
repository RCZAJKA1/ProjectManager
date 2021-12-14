namespace ProjectManager.MVC.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Represents a project's action view model.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class ActionViewModel
    {
        public DateTime DateOpened { get; set; }

        public DateTime DateDue { get; set; }

        public DateTime DateClosed { get; set; }

        public ActionStatus Status { get; set; }

        public string Description { get; set; }

        public ActionPriority Priority { get; set; }

        public string Owner { get; set; }

        public string Resolution { get; set; }
    }

    public enum ActionStatus
    {
        Open = 0,
        Closed = 1,
        InProcess = 2,
        Unknown = 3
    }

    public enum ActionPriority
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Unknown = 3
    }
}
