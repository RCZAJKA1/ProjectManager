namespace ProjectManager.MVC
{
	using System;
	using System.IO;

	using FluentValidation;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;

	using ProjectManager.Business.Services;
	using ProjectManager.Business.Validators;
	using ProjectManager.Data.Models;
	using ProjectManager.Data.Repositories;

	/// <summary>
	///     The application startup that handles web configuration and dependency injection.
	/// </summary>
	public class Startup
	{
		/// <summary>
		///     Creates a new instance of the <see cref="Startup"/> class.
		/// </summary>
		/// <param name="configuration"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		/// <summary>
		///     Gets the configuration.
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		///     This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services">The services collection.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			services
				.AddTransient<IProjectService, ProjectService>()
				.AddTransient<IProjectActionService, ProjectActionService>()
				.AddTransient<IProjectRepository, ProjectRepository>()
				.AddTransient<IProjectActionRepository, ProjectActionRepository>()
				.AddTransient<IValidator<Project>, ProjectValidator>()
				.AddTransient<IValidator<ProjectAction>, ProjectActionValidator>();
		}

		/// <summary>
		///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app">The application builder.</param>
		/// <param name="env">The web host environment.</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			string rootPath = Path.GetPathRoot(Environment.SystemDirectory);
			loggerFactory.AddFile($"{rootPath}\\Logs\\Log.txt");

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Projects}/{action=Projects}/{id?}");
			});
		}
	}
}
