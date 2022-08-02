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

	using ProjectManager.Data.Enums;
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
				using SqlConnection connection = new SqlConnection(this._connectionString);
				using SqlCommand command = new SqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = StoredProcedures.InsertAction
				};

				SqlParameter[] parameters = new SqlParameter[]
				{
					new SqlParameter("@dateOpened", projectAction.DateOpened),
					new SqlParameter("@dateClosed", projectAction.DateClosed),
					new SqlParameter("@dateDue", projectAction.DateDue),
					new SqlParameter("@ownerId", projectAction.OwnerId),
					new SqlParameter("@description", projectAction.Description),
					new SqlParameter("@resolution", projectAction.Resolution),
					new SqlParameter("@priorityId", (int)projectAction.Priority),
					new SqlParameter("@statusId", projectAction.Status),
					new SqlParameter("@projectId", projectAction.ProjectId)
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
				this._logger.LogError("An error occurred while saving the action to the database.", ex.Message);
				throw;
			}
		}

		/// <inheritdoc/>
		public async Task<IList<ProjectAction>> GetRecentActiveActionsAsync(int actionCount, CancellationToken cancellationToken = default)
		{
			if (actionCount < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(actionCount));
			}

			cancellationToken.ThrowIfCancellationRequested();

			IList<ProjectAction> actions = new List<ProjectAction>();

			try
			{
				using SqlConnection connection = new SqlConnection(this._connectionString);
				using SqlCommand command = new SqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = StoredProcedures.GetRecentActiveActions
				};

				SqlParameter[] parameters = new SqlParameter[]
				{
                    // TODO: add support for all dbo.ACTIONS parameters
                    new SqlParameter("@actionCount", actionCount)
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

						ProjectAction action = new ProjectAction
						{
							Id = reader.GetInt32(columns[0]),
							Description = reader.IsDBNull(columns[1]) ? null : reader.GetString(columns[1]),
							Priority = (ActionPriority)reader.GetInt32(columns[2]),
							ProjectId = reader.GetInt32(columns[3]),
							DateOpened = reader.IsDBNull(columns[4]) ? (DateTime?)null : reader.GetDateTime(columns[4]),
							DateClosed = reader.IsDBNull(columns[5]) ? (DateTime?)null : reader.GetDateTime(columns[5]),
							DateDue = reader.IsDBNull(columns[6]) ? (DateTime?)null : reader.GetDateTime(columns[6]),
							Status = (ActionStatus)reader.GetInt32(columns[7]),
							Resolution = reader.GetString(columns[8]),
							OwnerFirstName = reader.GetString(columns[9]),
							OwnerLastName = reader.GetString(columns[10])
						};

						actions.Add(action);

						this._logger.LogDebug($"Action {action.Id} returned from the database.");
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
				this._logger.LogError("A SQL error occurred while getting projects for user.", ex.Message);
				throw;
			}

			return actions;
		}

		/// <inheritdoc/>
		public async Task<IList<ActionOwner>> GetActiveActionOwnersAsync(CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			IList<ActionOwner> actionOwners = new List<ActionOwner>();

			try
			{
				using SqlConnection connection = new SqlConnection(this._connectionString);
				using SqlCommand command = new SqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = StoredProcedures.GetActiveActionOwners
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

						ActionOwner actionOwner = new ActionOwner
						{
							Id = reader.GetInt32(columns[0]),
							FirstName = reader.GetString(columns[1]),
							LastName = reader.GetString(columns[2])
						};

						actionOwners.Add(actionOwner);

						this._logger.LogDebug($"Action owner {actionOwner.Id} returned from the database.");
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
				this._logger.LogError("A SQL error occurred while getting projects for user.", ex.Message);
				throw;
			}

			return actionOwners;
		}

		/// <inheritdoc/>
		public async Task<IDictionary<int, string>> GetActionStatusesAsync(CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			IDictionary<int, string> actionStatuses = new Dictionary<int, string>();

			try
			{
				using SqlConnection connection = new SqlConnection(this._connectionString);
				using SqlCommand command = new SqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = StoredProcedures.GetActionStatuses
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

						int id = reader.GetInt32(columns[0]);
						string desc = reader.GetString(columns[1]);
						actionStatuses.Add(id, desc);

						this._logger.LogDebug($"Action status {id} returned from the database.");
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
				this._logger.LogError("A SQL error occurred while getting projects for user.", ex.Message);
				throw;
			}

			return actionStatuses;
		}

		/// <inheritdoc/>
		public async Task<IDictionary<int, string>> GetActionPrioritiesAsync(CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			IDictionary<int, string> actionPriorities = new Dictionary<int, string>();

			try
			{
				using SqlConnection connection = new SqlConnection(this._connectionString);
				using SqlCommand command = new SqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = StoredProcedures.GetActionPriorities
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

						int id = reader.GetInt32(columns[0]);
						string desc = reader.GetString(columns[1]);
						actionPriorities.Add(id, desc);

						this._logger.LogDebug($"Action priority {id} returned from the database.");
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
				this._logger.LogError("A SQL error occurred while getting projects for user.", ex.Message);
				throw;
			}

			return actionPriorities;
		}

		/// <inheritdoc/>
		public async Task<IList<ProjectAction>> SearchActionsAsync(ActionSearchOptions actionSearchOptions, CancellationToken cancellationToken = default)
		{
			this._logger.LogInformation("Entered method ProjectActionRepository.SearchActionsAsync().");

			cancellationToken.ThrowIfCancellationRequested();

			IList<ProjectAction> actions = new List<ProjectAction>();
			try
			{
				using SqlConnection connection = new SqlConnection(this._connectionString);
				using SqlCommand command = new SqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = StoredProcedures.GetActions
				};

				SqlParameter[] parameters = new SqlParameter[]
				{
					// TODO: support remaining criteria

					//new SqlParameter("@dateOpened", actionSearchOptions.DateOpened),
					//new SqlParameter("@dateClosed", actionSearchOptions.DateClosed),
					//new SqlParameter("@dateDue", actionSearchOptions.DateDue),
					new SqlParameter("@ownerId", actionSearchOptions.OwnerId),
					new SqlParameter("@description", actionSearchOptions.Description)
					//new SqlParameter("@resolution", actionSearchOptions.Resolution),
					//new SqlParameter("@priorityId", actionSearchOptions.Priority),
					//new SqlParameter("@statusId", actionSearchOptions.Status),
					//new SqlParameter("@projectId", actionSearchOptions.ProjectId)
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

						ProjectAction action = new ProjectAction
						{
							Id = reader.GetInt32(columns[0]),
							Description = reader.IsDBNull(columns[1]) ? null : reader.GetString(columns[1]),
							DateOpened = reader.IsDBNull(columns[2]) ? (DateTime?)null : reader.GetDateTime(columns[2]),
							DateClosed = reader.IsDBNull(columns[3]) ? (DateTime?)null : reader.GetDateTime(columns[3]),
							DateDue = reader.IsDBNull(columns[4]) ? (DateTime?)null : reader.GetDateTime(columns[4]),
							OwnerId = reader.GetInt32(columns[5]),
							Priority = (ActionPriority)reader.GetInt32(columns[6]),
							ProjectId = reader.GetInt32(columns[7]),
							Resolution = reader.IsDBNull(columns[8]) ? null : reader.GetString(columns[8]),
							Status = (ActionStatus)reader.GetInt32(columns[9]),
							OwnerFirstName = reader.GetString(columns[10]),
							OwnerLastName = reader.GetString(columns[11])
						};

						actions.Add(action);
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
				this._logger.LogError("A SQL error occurred while searching for projects.", ex.Message);
				throw;
			}

			return actions;
		}
	}
}
