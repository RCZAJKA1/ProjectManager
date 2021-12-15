namespace ProjectManager.MVC.Services
{
    using ProjectManager.MVC.Models;
    using System.Threading.Tasks;

    /// <summary>
    ///     Handles business operations for actions.
    /// </summary>
    public interface IActionsService
    {
        /// <summary>
        ///     Saves an action.
        /// </summary>
        /// <param name="actionViewModel">The action view model.</param>
        /// <returns>The <see cref="Task"/> that completed saving the action.</returns>
        Task SaveActionAsync(ActionViewModel actionViewModel);
    }
}
