namespace ProjectManager.Data.DataAccess
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Provides access to the database.
    /// </summary>
    public interface ISqlDataAccessor
    {
        /// <summary>
        ///     Queries the database by executing the specified stored procoedure and returns the resulting data set.
        /// </summary>
        /// <typeparam name="T">The model to return.</typeparam>
        /// <typeparam name="U">The type of parameters.</typeparam>
        /// <param name="storedProcedure">The stored procedure to execute.</param>
        /// <param name="parameters">The stored procedure parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the data returned from the query result.</returns>
        Task<IEnumerable<T>> QueryDataAsync<T, U>(string storedProcedure, U parameters, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Queries the database by executing the specified stored procoedure without parameters and returns the resulting data set.
        /// </summary>
        /// <typeparam name="T">The model to return.</typeparam>
        /// <param name="storedProcedure">The stored procedure to execute.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the data returned from the query result.</returns>
        Task<IEnumerable<T>> QueryDataParameterlessAsync<T>(string storedProcedure, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Saves data to the database by executing the specified stored procoedure.
        /// </summary>
        /// <typeparam name="T">The model containing the data to save.</typeparam>
        /// <param name="storedProcedure">The stored procedure to execute.</param>
        /// <param name="parameters">The stored procedure parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> that completed saving the data to the database.</returns>
        Task SaveDataAsync<T>(string storedProcedure, T parameters, CancellationToken cancellationToken = default);
    }
}