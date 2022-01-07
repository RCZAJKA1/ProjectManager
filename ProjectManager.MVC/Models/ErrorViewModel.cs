namespace ProjectManager.MVC.Models
{
    /// <summary>
    ///     Represents the error view model.
    /// </summary>
    public sealed class ErrorViewModel
    {
        /// <summary>
        ///     Gets or sets the request identifier.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        ///     Gets the show request identifier.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}
