namespace ProjectManager.MVC.Tests.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Moq;

    using ProjectManager.Business.Services;
    using ProjectManager.Data.Models;
    using ProjectManager.MVC.Controllers;
    using ProjectManager.MVC.Models;

    using Xunit;

    public sealed class ActionsControllerTests
    {
        private readonly MockRepository mockRepository;

        private readonly Mock<ILogger<ActionsController>> mockLogger;
        private readonly Mock<IProjectActionService> mockProjectActionService;

        public ActionsControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockLogger = this.mockRepository.Create<ILogger<ActionsController>>();
            this.mockProjectActionService = this.mockRepository.Create<IProjectActionService>();
        }

        [Fact]
        public void Ctor_LoggerNull_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>("logger", () => new ActionsController(null, this.mockProjectActionService.Object));

            Assert.Equal("Value cannot be null. (Parameter 'logger')", exception.Message);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Ctor_ProjectActionServiceNull_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>("actionsService", () => new ActionsController(this.mockLogger.Object, null));

            Assert.Equal("Value cannot be null. (Parameter 'actionsService')", exception.Message);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Ctor_ValidParameters_CreatesInstance()
        {
            ActionsController actionsController = this.CreateActionsController();

            Assert.NotNull(actionsController);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Actions_NoLogic_ReturnsActionsView()
        {
            ActionsController controller = this.CreateActionsController();

            IActionResult result = controller.Actions();

            Assert.NotNull(result);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            Assert.Empty(viewResult.ViewData);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Actions_ActionViewModelNull_ThrowsAsync()
        {
            ActionsController controller = this.CreateActionsController();

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>("actionViewModel", async () => await controller.Actions(null).ConfigureAwait(false)).ConfigureAwait(false);

            Assert.Equal("Value cannot be null. (Parameter 'actionViewModel')", exception.Message);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Actions_ModelStateInvalid_RedirectsAsync()
        {
            ActionViewModel viewModel = new ActionViewModel();

            ActionsController controller = this.CreateActionsController();
            controller.ModelState.AddModelError("testKey1", "testError1");

            this.mockLogger.Setup(x => x.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                It.Is<EventId>(y => y == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "The model state is invalid. Redirecting back to Actions." && @type.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()));

            IActionResult result = await controller.Actions(viewModel).ConfigureAwait(false);

            Assert.NotNull(result);
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Actions", redirectToActionResult.ActionName);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Actions_AddsProjectAction_ReturnsActionViewAsync()
        {
            ActionViewModel viewModel = new ActionViewModel();
            ProjectAction projectAction = new ProjectAction();
            CancellationToken cancellationToken = new CancellationToken(false);

            ActionsController controller = this.CreateActionsController();

            this.mockProjectActionService.Setup(x => x.AddProjectActionAsync(It.IsAny<ProjectAction>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            IActionResult result = await controller.Actions(viewModel).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);

            this.mockRepository.VerifyAll();
        }

        private ActionsController CreateActionsController()
        {
            return new ActionsController(this.mockLogger.Object, this.mockProjectActionService.Object);
        }
    }
}
