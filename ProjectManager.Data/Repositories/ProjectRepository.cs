namespace ProjectManager.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using ProjectManager.Common.Models;

    /// <inheritdoc cref="IProjectRepository"/>
    public sealed class ProjectRepository : IProjectRepository
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<ProjectRepository> _logger;

        /// <summary>
        ///     The sql connection string.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        ///     Creates a new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        public ProjectRepository(ILogger<ProjectRepository> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._connectionString = Environment.GetEnvironmentVariable("ProjectManagerConnectionString");
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Project>> GetProjectsForUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            this._logger.LogInformation("Entered method GetProjectsForUserAsync().");

            if (userId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            cancellationToken.ThrowIfCancellationRequested();

            IList<Project> projects = new List<Project>();

            try
            {
                using SqlConnection connection = new SqlConnection(this._connectionString);
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
                        List<string> columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                        Project project = new Project
                        {
                            Id = reader.GetInt32(columns[0]),
                            Name = reader.GetString(columns[1]),
                            Description = reader.GetString(columns[2]),
                            StartDate = reader.IsDBNull(columns[3]) ? (DateTime?)null : reader.GetDateTime(columns[3]),
                            EndDate = reader.IsDBNull(columns[4]) ? (DateTime?)null : reader.GetDateTime(columns[4]),
                            DueDate = reader.IsDBNull(columns[5]) ? (DateTime?)null : reader.GetDateTime(columns[5]),
                            Status = reader.GetString(columns[6]),
                            Owner = reader.GetString(columns[7])
                        };

                        projects.Add(project);
                    }
                    catch (Exception exception)
                    {
                        this._logger.LogError($"An error occurred while parsing the sql data reader values: {exception.Message}");
                        throw;
                    }
                }
            }
            catch (SqlException ex)
            {
                this._logger.LogError("An error occurred while saving the action.", ex.Message);
                throw;
            }

            return projects;
        }

        /// <inheritdoc/>
        public async Task AddProjectAsync(Project project, CancellationToken cancellationToken = default)
        {
            this._logger.LogInformation("Entered method AddProjectAsync().");

            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                using SqlConnection connection = new SqlConnection(this._connectionString);
                using SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[usp_InsertProject]"
                };

                SqlParameter[] parameters = new SqlParameter[]
                {
                    // TODO: support nulls for datetimes in database

                    new SqlParameter("@name", project.Name),
                    new SqlParameter("@description", project.Description),
                    new SqlParameter("@statusId", project.StatusId),
                    new SqlParameter("@ownerId", project.OwnerId),
                    new SqlParameter("@startDate", project.StartDate),
                    new SqlParameter("@endDate", project.EndDate),
                    new SqlParameter("@dueDate", project.DueDate),
                    new SqlParameter("@categoryId", project.CategoryId)
                };

                command.Parameters.AddRange(parameters);

                if (command.Connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                }

                await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (SqlException ex)
            {
                this._logger.LogError("An error occurred while adding the project.", ex.Message);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<IDictionary<int, string>> GetActiveProjectOwnersAsync(CancellationToken cancellationToken = default)
        {
            this._logger.LogInformation("Entered method GetActiveProjectOwnersAsync().");

            cancellationToken.ThrowIfCancellationRequested();

            IDictionary<int, string> projectOwners = new Dictionary<int, string>();

            try
            {
                using SqlConnection connection = new SqlConnection(this._connectionString);
                using SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[usp_GetActiveProjectOwners]"
                };

                if (command.Connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                }

                using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

                while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                {
                    try
                    {
                        List<string> columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                        int ownerId = reader.GetInt32(columns[0]);
                        string ownerName = reader.GetString(columns[1]);
                        projectOwners.Add(ownerId, ownerName);
                    }
                    catch (Exception exception)
                    {
                        this._logger.LogError($"An error occurred while parsing the sql data reader values: {exception.Message}");
                        throw;
                    }
                }
            }
            catch (SqlException ex)
            {
                this._logger.LogError("An error occurred while reading the project owners.", ex.Message);
                throw;
            }

            return projectOwners;
        }

        /// <inheritdoc/>
        public async Task<IDictionary<int, string>> GetProjectStatusesAsync(CancellationToken cancellationToken = default)
        {
            this._logger.LogInformation("Entered method GetProjectStatusesAsync().");

            cancellationToken.ThrowIfCancellationRequested();

            IDictionary<int, string> projectStatuses = new Dictionary<int, string>();

            try
            {
                using SqlConnection connection = new SqlConnection(this._connectionString);
                using SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[usp_GetProjectStatuses]"
                };

                if (command.Connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                }

                using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

                while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                {
                    try
                    {
                        List<string> columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                        int statusId = reader.GetInt32(columns[0]);
                        string statusDesc = reader.GetString(columns[1]);
                        projectStatuses.Add(statusId, statusDesc);
                    }
                    catch (Exception exception)
                    {
                        this._logger.LogError($"An error occurred while parsing the sql data reader values: {exception.Message}");
                        throw;
                    }
                }
            }
            catch (SqlException ex)
            {
                this._logger.LogError("An error occurred while reading the project statuses.", ex.Message);
                throw;
            }

            return projectStatuses;
        }

        /// <inheritdoc/>
        public async Task<IDictionary<int, string>> GetProjectCategoriesAsync(CancellationToken cancellationToken = default)
        {
            this._logger.LogInformation("Entered method GetProjectCategoriesAsync().");

            cancellationToken.ThrowIfCancellationRequested();

            IDictionary<int, string> projectCategories = new Dictionary<int, string>();

            try
            {
                using SqlConnection connection = new SqlConnection(this._connectionString);
                using SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[usp_GetProjectCategories]"
                };

                if (command.Connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                }

                using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

                while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                {
                    try
                    {
                        List<string> columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                        int categoryId = reader.GetInt32(columns[0]);
                        string categoryDesc = reader.GetString(columns[1]);
                        projectCategories.Add(categoryId, categoryDesc);
                    }
                    catch (Exception exception)
                    {
                        this._logger.LogError($"An error occurred while parsing the sql data reader values: {exception.Message}");
                        throw;
                    }
                }
            }
            catch (SqlException ex)
            {
                this._logger.LogError("An error occurred while reading the project categories.", ex.Message);
                throw;
            }

            return projectCategories;
        }
    }
}
