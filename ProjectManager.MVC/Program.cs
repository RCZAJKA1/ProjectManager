namespace ProjectManager.MVC
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;

    /// <summary>
    ///     The program class definition.
    /// </summary>
    internal sealed class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        /// <param name="args">The application arguments.</param>
        internal static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        ///     Creates and configures the <see cref="IHostBuilder"/>.
        /// </summary>
        /// <param name="args">The application arguments.</param>
        /// <returns>An <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
