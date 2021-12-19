namespace ProjectManager.Data.Repositories
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using ProjectManager.Business.Models;

    /// <inheritdoc cref="IProjectActionRepository"/>
    public class ProjectActionRepository : IProjectActionRepository
    {
        /// <summary>
        ///     The sql connection string.
        /// </summary>
        private readonly string ConnectionString;

        /// <summary>
        ///     Creates a new instance of the <see cref="ProjectActionRepository"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectActionRepository(ILogger<ProjectActionRepository> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.ConnectionString = Environment.GetEnvironmentVariable("ProjectManagerConnectionString");
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        internal ILogger<ProjectActionRepository> Logger { get; }

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
                using SqlConnection connection = new SqlConnection(this.ConnectionString);
                using SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[usp_InsertAction]"
                };

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@dateOpened", projectAction.DateOpened),
                    new SqlParameter("@dateClosed", projectAction.DateClosed),
                    new SqlParameter("@dateDue", projectAction.DateDue),
                    new SqlParameter("@owner", projectAction.Owner),
                    new SqlParameter("@description", projectAction.Description),
                    new SqlParameter("@resolution", projectAction.Resolution),
                    new SqlParameter("@priority", projectAction.Priority),
                    new SqlParameter("@status", projectAction.Status)
                };

                command.Parameters.AddRange(parameters);

                if (command.Connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                }

                //TODO: execute stored procedure
            }
            catch (SqlException ex)
            {
                this.Logger.LogError("An error occurred while saving the action.", ex.Message);
                throw;
            }
        }
    }
}
