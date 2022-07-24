namespace ProjectManager.Data.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;

    using Dapper;

    using Microsoft.Extensions.Logging;

    /// <inheritdoc cref="ISqlDataAccessor"/>
    internal sealed class SqlDataAccessor : ISqlDataAccessor
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<SqlDataAccessor> logger;

        /// <summary>
        ///     Creates a new instance of the <see cref="SqlDataAccessor"/> class.
        /// </summary>
        public SqlDataAccessor(ILogger<SqlDataAccessor> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public Task<IEnumerable<T>> QueryDataAsync<T, U>(string storedProcedure, U parameters, CancellationToken cancellationToken = default)
        {
            if (storedProcedure == null)
            {
                throw new ArgumentNullException(nameof(storedProcedure));
            }
            if (string.IsNullOrEmpty(storedProcedure))
            {
                throw new ArgumentException("The argument cannot be empty or only contain white space.", nameof(storedProcedure));
            }
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            cancellationToken.ThrowIfCancellationRequested();

            return this.QueryDataImplAsync<T, U>(storedProcedure, parameters);
        }

        /// <inheritdoc />
        public Task<IEnumerable<T>> QueryDataParameterlessAsync<T>(string storedProcedure, CancellationToken cancellationToken = default)
        {
            if (storedProcedure == null)
            {
                throw new ArgumentNullException(nameof(storedProcedure));
            }
            if (string.IsNullOrEmpty(storedProcedure))
            {
                throw new ArgumentException("The argument cannot be empty or only contain white space.", nameof(storedProcedure));
            }

            cancellationToken.ThrowIfCancellationRequested();

            return this.QueryDataParameterlessImplAsync<T>(storedProcedure);
        }

        /// <inheritdoc />
        public Task SaveDataAsync<T>(string storedProcedure, T parameters, CancellationToken cancellationToken = default)
        {
            if (storedProcedure == null)
            {
                throw new ArgumentNullException(nameof(storedProcedure));
            }
            if (string.IsNullOrEmpty(storedProcedure))
            {
                throw new ArgumentException("The argument cannot be empty or only contain white space.", nameof(storedProcedure));
            }
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            cancellationToken.ThrowIfCancellationRequested();

            return this.SaveDataImplAsync(storedProcedure, parameters);
        }

        /// <summary>
        ///     Queries the database by executing the specified stored procoedure and returns the resulting data set.
        /// </summary>
        /// <typeparam name="T">The model to return.</typeparam>
        /// <typeparam name="U">The type of parameters.</typeparam>
        /// <param name="storedProcedure">The stored procedure to execute.</param>
        /// <param name="parameters">The stored procedure parameters.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the data returned from the query result.</returns>
        private async Task<IEnumerable<T>> QueryDataImplAsync<T, U>(string storedProcedure, U parameters)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("ProjectManagerConnectionString"));
                return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"An error occurred while querying the database: {ex.Message}");
                throw;
            }
        }

        private async Task<IEnumerable<T>> QueryDataParameterlessImplAsync<T>(string storedProcedure)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("ProjectManagerConnectionString"));
                return await connection.QueryAsync<T>(storedProcedure, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"An error occurred while querying the database: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        ///     Saves data to the database by executing the specified stored procoedure.
        /// </summary>
        /// <typeparam name="T">The model containing the data to save.</typeparam>
        /// <param name="storedProcedure">The stored procedure to execute.</param>
        /// <param name="parameters">The stored procedure parameters.</param>
        /// <returns>The <see cref="Task"/> that completed saving the data to the database.</returns>
        private async Task SaveDataImplAsync<T>(string storedProcedure, T parameters)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("ProjectManagerConnectionString"));
                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"An error occurred while saving to the database: {ex.Message}");
                throw;
            }
        }
    }
}
