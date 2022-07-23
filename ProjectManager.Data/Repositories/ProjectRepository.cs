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

	using ProjectManager.Data.Models;

	/// <inheritdoc cref="IProjectRepository"/>
	public sealed class ProjectRepository : IProjectRepository
	{
		/// <summary>
		///     The logger.
		/// </summary>
		private readonly ILogger<ProjectRepository> logger;

		/// <summary>
		///     The sql connection string.
		/// </summary>
		private readonly string connectionString;

		/// <summary>
		///     Creates a new instance of the <see cref="ProjectRepository"/> class.
		/// </summary>
		public ProjectRepository(ILogger<ProjectRepository> logger)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.connectionString = Environment.GetEnvironmentVariable("ProjectManagerConnectionString");
		}

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
				using SqlConnection connection = new SqlConnection(this.connectionString);
				using SqlCommand command = new SqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = StoredProcedures.GetAllProjectsForUser
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
							OwnerFirstName = reader.GetString(columns[7]),
							OwnerLastName = reader.GetString(columns[8])
						};

						projects.Add(project);

						this.logger.LogInformation($"Project {project.Id} returned from the database.");
					}
					catch (Exception exception)
					{
						this.logger.LogError($"An error occurred while parsing the sql data reader values: {exception.Message}");
						throw;
					}
				}
			}
			catch (SqlException ex)
			{
				this.logger.LogError("A SQL error occurred while getting projects for user.", ex.Message);
				throw;
			}

			return projects;
		}

		/// <inheritdoc/>
		public async Task<IEnumerable<Project>> GetRecentActiveProjectsAsync(int projectCount, CancellationToken cancellationToken = default)
		{
			if (projectCount < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(projectCount));
			}

			cancellationToken.ThrowIfCancellationRequested();

			IList<Project> projects = new List<Project>();

			try
			{
				using SqlConnection connection = new SqlConnection(this.connectionString);
				using SqlCommand command = new SqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = StoredProcedures.GetRecentActiveProjects
				};

				SqlParameter[] parameters = new SqlParameter[]
				{
					new SqlParameter("@projectCount", projectCount)
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
							Description = reader.IsDBNull(columns[2]) ? (string)null : reader.GetString(columns[2]),
							StartDate = reader.IsDBNull(columns[3]) ? (DateTime?)null : reader.GetDateTime(columns[3]),
							EndDate = reader.IsDBNull(columns[4]) ? (DateTime?)null : reader.GetDateTime(columns[4]),
							DueDate = reader.IsDBNull(columns[5]) ? (DateTime?)null : reader.GetDateTime(columns[5]),
							Status = reader.GetString(columns[6]),
							OwnerFirstName = reader.GetString(columns[7]),
							OwnerLastName = reader.GetString(columns[8])
						};

						projects.Add(project);

						this.logger.LogInformation($"Project {project.Id} returned from the database.");
					}
					catch (Exception exception)
					{
						this.logger.LogError($"An error occurred while parsing the sql data reader values: {exception.Message}");
						throw;
					}
				}
			}
			catch (SqlException ex)
			{
				this.logger.LogError("A SQL error occurred while getting projects for user.", ex.Message);
				throw;
			}

			return projects;
		}

		/// <inheritdoc/>
		public async Task AddProjectAsync(Project project, CancellationToken cancellationToken = default)
		{
			this.logger.LogInformation("Entered method ProjectRepository.AddProjectAsync().");

			if (project == null)
			{
				throw new ArgumentNullException(nameof(project));
			}

			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				using SqlConnection connection = new SqlConnection(this.connectionString);
				using SqlCommand command = new SqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = StoredProcedures.InsertProject
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
				this.logger.LogError("A SQL error occurred while adding the project.", ex.Message);
				throw;
			}
		}

		/// <inheritdoc/>
		public async Task<IEnumerable<ProjectOwner>> GetActiveProjectOwnersAsync(CancellationToken cancellationToken = default)
		{
			this.logger.LogInformation("Entered method ProjectRepository.GetActiveProjectOwnersAsync().");

			cancellationToken.ThrowIfCancellationRequested();

			List<ProjectOwner> projectOwners = new List<ProjectOwner>();

			try
			{
				using SqlConnection connection = new SqlConnection(this.connectionString);
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
						string ownerLastName = reader.GetString(columns[1]);
						string ownerFirstName = reader.GetString(columns[2]);
						ProjectOwner projectOwner = new ProjectOwner
						{
							Id = ownerId,
							FirstName = ownerFirstName,
							LastName = ownerLastName
						};
						projectOwners.Add(projectOwner);
					}
					catch (Exception exception)
					{
						this.logger.LogError($"An error occurred while parsing the sql data reader values: {exception.Message}");
						throw;
					}
				}
			}
			catch (SqlException ex)
			{
				this.logger.LogError("A SQL error occurred while reading the the active project owners.", ex.Message);
				throw;
			}

			return projectOwners;
		}

		/// <inheritdoc/>
		public async Task<IDictionary<int, string>> GetProjectStatusesAsync(CancellationToken cancellationToken = default)
		{
			this.logger.LogInformation("Entered method ProjectRepository.GetProjectStatusesAsync().");

			cancellationToken.ThrowIfCancellationRequested();

			IDictionary<int, string> projectStatuses = new Dictionary<int, string>();

			try
			{
				using SqlConnection connection = new SqlConnection(this.connectionString);
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
						this.logger.LogError($"An error occurred while parsing the sql data reader values: {exception.Message}");
						throw;
					}
				}
			}
			catch (SqlException ex)
			{
				this.logger.LogError("A SQL error occurred while reading the project statuses.", ex.Message);
				throw;
			}

			return projectStatuses;
		}

		/// <inheritdoc/>
		public async Task<IDictionary<int, string>> GetProjectCategoriesAsync(CancellationToken cancellationToken = default)
		{
			this.logger.LogInformation("Entered method ProjectRepository.GetProjectCategoriesAsync().");

			cancellationToken.ThrowIfCancellationRequested();

			IDictionary<int, string> projectCategories = new Dictionary<int, string>();

			try
			{
				using SqlConnection connection = new SqlConnection(this.connectionString);
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
						this.logger.LogError($"An error occurred while parsing the sql data reader values: {exception.Message}");
						throw;
					}
				}
			}
			catch (SqlException ex)
			{
				this.logger.LogError("A SQL error occurred while reading the project categories.", ex.Message);
				throw;
			}

			return projectCategories;
		}

		/// <inheritdoc/>
		public async Task<IList<Project>> SearchProjectsForUserAsync(int userId, string projectName, CancellationToken cancellationToken = default)
		{
			this.logger.LogInformation("Entered method ProjectRepository.SearchProjectsForUserAsync().");

			cancellationToken.ThrowIfCancellationRequested();

			IList<Project> projects = new List<Project>();
			try
			{
				using SqlConnection connection = new SqlConnection(this.connectionString);
				using SqlCommand command = new SqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = StoredProcedures.SearchProjectsForOwner
				};

				SqlParameter[] parameters = new SqlParameter[]
				{
					new SqlParameter("@ownerId", userId),
					new SqlParameter("@name", projectName)
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
							Description = reader.IsDBNull(columns[2]) ? null : reader.GetString(columns[2]),
							StartDate = reader.IsDBNull(columns[3]) ? (DateTime?)null : reader.GetDateTime(columns[3]),
							EndDate = reader.IsDBNull(columns[4]) ? (DateTime?)null : reader.GetDateTime(columns[4]),
							DueDate = reader.IsDBNull(columns[5]) ? (DateTime?)null : reader.GetDateTime(columns[5]),
							Status = reader.IsDBNull(columns[6]) ? null : reader.GetString(columns[6]),
							OwnerFirstName = reader.IsDBNull(columns[7]) ? null : reader.GetString(columns[7]),
							OwnerLastName = reader.IsDBNull(columns[8]) ? null : reader.GetString(columns[8])
						};

						projects.Add(project);
					}
					catch (Exception exception)
					{
						this.logger.LogError($"An error occurred while parsing the sql data reader values: {exception.Message}");
						throw;
					}
				}
			}
			catch (SqlException ex)
			{
				this.logger.LogError("A SQL error occurred while searching for projects.", ex.Message);
				throw;
			}

			return projects;
		}

		/// <inheritdoc/>
		public async Task DeleteProjectAsync(int projectId, CancellationToken cancellationToken = default)
		{
			this.logger.LogInformation("Entered method ProjectRepository.DeleteProjectAsync().");

			if (projectId < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(projectId));
			}

			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				using SqlConnection connection = new SqlConnection(this.connectionString);
				using SqlCommand command = new SqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = "[dbo].[usp_DeleteProject]"
				};

				SqlParameter[] parameters = new SqlParameter[]
				{
					new SqlParameter("@projectId", projectId)
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
				this.logger.LogError("A SQL error occurred while deleting the project.", ex.Message);
				throw;
			}
		}
	}
}
