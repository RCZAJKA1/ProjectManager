namespace ProjectManager.Data.Repositories
{
    using System;
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using ProjectManager.Data.Models;

    /// <inheritdoc cref="IProjectActionRepository"/>
    public sealed class ProjectActionRepository : IProjectActionRepository
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<ProjectActionRepository> _logger;

        /// <summary>
        ///     The sql connection string.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        ///     Creates a new instance of the <see cref="ProjectActionRepository"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectActionRepository(ILogger<ProjectActionRepository> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //TODO: uncomment
            this._connectionString = Environment.GetEnvironmentVariable("ProjectManagerConnectionString"); //?? throw new ArgumentNullException(nameof(this._connectionString));
        }

        /// <inheritdoc/>
        public async Task SaveActionAsync(ProjectAction projectAction, CancellationToken cancellationToken = default)
        {
            if (projectAction == null)
            {
                throw new ArgumentNullException(nameof(projectAction));
            }

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                //TODO: execute stored procedure
                await Task.FromResult(new NotImplementedException());

                //using SqlConnection connection = new SqlConnection(this._connectionString);
                //using SqlCommand command = new SqlCommand
                //{
                //    Connection = connection,
                //    CommandType = CommandType.StoredProcedure,
                //    CommandText = "[dbo].[usp_InsertAction]"
                //};

                //SqlParameter[] parameters = new SqlParameter[]
                //{
                //    new SqlParameter("@dateOpened", projectAction.DateOpened),
                //    new SqlParameter("@dateClosed", projectAction.DateClosed),
                //    new SqlParameter("@dateDue", projectAction.DateDue),
                //    new SqlParameter("@owner", projectAction.Owner),
                //    new SqlParameter("@description", projectAction.Description),
                //    new SqlParameter("@resolution", projectAction.Resolution),
                //    new SqlParameter("@priority", projectAction.Priority),
                //    new SqlParameter("@status", projectAction.Status)
                //};

                //command.Parameters.AddRange(parameters);

                //if (command.Connection.State != ConnectionState.Open)
                //{
                //    await connection.OpenAsync().ConfigureAwait(false);
                //}
            }
            catch (SqlException ex)
            {
                this._logger.LogError("An error occurred while saving the action to the database.", ex.Message);
                throw;
            }
        }
    }
}
