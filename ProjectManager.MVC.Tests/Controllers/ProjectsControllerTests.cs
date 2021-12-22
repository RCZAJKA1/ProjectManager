namespace ProjectManager.MVC.Tests.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Moq;

    using ProjectManager.MVC.Controllers;

    using Xunit;

    [ExcludeFromCodeCoverage]
    public sealed class ProjectsControllerTests
    {
        private readonly MockRepository mockRepository;

        private readonly Mock<ILogger<ProjectsController>> mockLogger;

        public ProjectsControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockLogger = this.mockRepository.Create<ILogger<ProjectsController>>();
        }

        [Fact]
        public void Ctor_LoggerNull_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>("logger", () => new ProjectsController(null));

            Assert.Equal("Value cannot be null. (Parameter 'logger')", exception.Message);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Ctor_ValidParameters_CreatesInstance()
        {
            ProjectsController projectsController = this.CreateProjectsController();

            Assert.Same(projectsController.Logger, this.mockLogger.Object);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Projects_NoLogic_ReturnsProjectsView()
        {
            ProjectsController controller = this.CreateProjectsController();

            IActionResult result = await controller.Projects();

            Assert.NotNull(result);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            Assert.Empty(viewResult.ViewData);

            this.mockRepository.VerifyAll();
        }

        private ProjectsController CreateProjectsController()
        {
            return new ProjectsController(this.mockLogger.Object);
        }
    }
}
