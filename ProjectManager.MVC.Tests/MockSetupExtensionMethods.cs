namespace ProjectManager.MVC.Tests
{
	using System;

	using Microsoft.Extensions.Logging;

	using Moq;

	/// <summary>
	///     Extension methods for convenient mock setups.
	/// </summary>
	public static class MockSetupExtensionMethods
	{
		/// <summary>
		///     Sets up the mock logger.
		/// </summary>
		/// <typeparam name="T">The logger of type.</typeparam>
		/// <param name="mockLogger">The mock logger.</param>
		/// <param name="logLevel">The log level.</param>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="message">The log message.</param>
		/// <param name="exception">The exception thrown, if applicable.</param>
		public static void SetupLog<T>(this Mock<ILogger<T>> mockLogger, LogLevel logLevel, EventId eventId, string message, Exception exception)
		{
			if (mockLogger == null)
			{
				throw new ArgumentNullException(nameof(mockLogger));
			}

			mockLogger.Setup(x => x.Log(
				logLevel,
				eventId,
				It.Is<It.IsAnyType>((testObject, testType) => testObject.ToString() == message && testType.Name == "FormattedLogValues"),
				exception,
				It.IsAny<Func<It.IsAnyType, Exception, string>>()));
		}
	}
}