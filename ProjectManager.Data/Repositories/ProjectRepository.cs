namespace ProjectManager.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using ProjectManager.Common.Models;

    /// <inheritdoc cref="IProjectRepository"/>
    public sealed class ProjectRepository : IProjectRepository
    {
        /// <summary>
        ///     The sql connection string.
        /// </summary>
        private readonly string ConnectionString;

        /// <summary>
        ///     Creates a new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        public ProjectRepository(ILogger<ProjectRepository> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.ConnectionString = Environment.GetEnvironmentVariable("ProjectManagerConnectionString");
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        internal ILogger<ProjectRepository> Logger { get; }

        /// <inheritdoc/>
        public async Task<IEnumerable<Project>> GetProjectsForUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            if (userId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            cancellationToken.ThrowIfCancellationRequested();

            IList<Project> projects = new List<Project>();

            try
            {
                using SqlConnection connection = new SqlConnection(this.ConnectionString);
                using SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[usp_GetAllProjectsForUser]"
                };

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@userId", userId)
                };

                command.Parameters.AddRange(parameters);

                if (command.Connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                }

                using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

                while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                {
                    try
                    {
                        Project project = new Project
                        {
                            Id = reader.GetInt32("ProjectId"),
                            Name = reader.GetString("ProjectName"),
                            Description = reader.GetString("ProjectDesc"),
                            Owner = reader.GetString("ProjectOwner"),
                            Status = reader.GetString("ProjectStatus"),
                            StartDate = reader.GetDateTime("ProjectStartDate"),
                            EndDate = reader.GetDateTime("ProjectEndDate"),
                            DueDate = reader.GetDateTime("ProjectDueDate")
                        };

                        projects.Add(project);
                    }
                    catch (InvalidCastException exception)
                    {
                        this.Logger.LogError($"An error occurred while parsing the sql data reader values: {exception.Message}");
                        throw;
                    }
                }
            }
            catch (SqlException ex)
            {
                this.Logger.LogError("An error occurred while saving the action.", ex.Message);
                throw;
            }

            return projects;
        }
    }
}
