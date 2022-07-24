namespace ProjectManager.MVC.Tests.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	using Moq;

	using ProjectManager.Business.Services;
	using ProjectManager.Data.Models;
	using ProjectManager.MVC.Controllers;

	using Xunit;

	public sealed class ProjectsControllerTests
	{
		private readonly MockRepository mockRepository;

		private readonly Mock<ILogger<ProjectsController>> mockLogger;
		private readonly Mock<IProjectService> mockProjectService;

		public ProjectsControllerTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockLogger = this.mockRepository.Create<ILogger<ProjectsController>>();
			this.mockProjectService = this.mockRepository.Create<IProjectService>();
		}

		[Fact]
		public void Ctor_LoggerNull_Throws()
		{
			ArgumentNullException exception = Assert.Throws<ArgumentNullException>("logger", () => new ProjectsController(null, this.mockProjectService.Object));

			Assert.Equal("Value cannot be null. (Parameter 'logger')", exception.Message);

			this.mockRepository.VerifyAll();
		}

		[Fact]
		public void Ctor_ProjectServiceNull_Throws()
		{
			ArgumentNullException exception = Assert.Throws<ArgumentNullException>("projectService", () => new ProjectsController(this.mockLogger.Object, null));

			Assert.Equal("Value cannot be null. (Parameter 'projectService')", exception.Message);

			this.mockRepository.VerifyAll();
		}

		[Fact]
		public void Ctor_ValidParameters_CreatesInstance()
		{
			ProjectsController projectsController = this.CreateProjectsController();

			Assert.NotNull(projectsController);

			this.mockRepository.VerifyAll();
		}

		[Fact]
		public async Task Projects_GetsProjects_ReturnsProjectsView()
		{
			CancellationToken cancellationToken = new CancellationToken(false);
			IList<Project> projects = new List<Project>();
			IList<ProjectOwner> owners = new List<ProjectOwner>();
			IDictionary<int, string> statuses = new Dictionary<int, string>();
			IDictionary<int, string> categories = new Dictionary<int, string>();

			this.mockLogger.SetupLog(LogLevel.Information, 0, "Entered method Projects().", null);

			this.mockProjectService.Setup(x => x.GetRecentActiveProjectsAsync(It.Is<int>(y => y == 20), It.IsAny<CancellationToken>())).ReturnsAsync(projects);
			this.mockProjectService.Setup(x => x.GetActiveProjectOwnersAsync(It.IsAny<CancellationToken>())).ReturnsAsync(owners);
			this.mockProjectService.Setup(x => x.GetProjectStatusesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(statuses);
			this.mockProjectService.Setup(x => x.GetProjectCategoriesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(categories);

			ProjectsController controller = this.CreateProjectsController();
			IActionResult result = await controller.Projects();

			Assert.NotNull(result);
			ViewResult viewResult = Assert.IsType<ViewResult>(result);
			Assert.Empty(viewResult.ViewData);

			this.mockRepository.VerifyAll();
		}

		private ProjectsController CreateProjectsController()
		{
			return new ProjectsController(this.mockLogger.Object, this.mockProjectService.Object);
		}
	}
}
