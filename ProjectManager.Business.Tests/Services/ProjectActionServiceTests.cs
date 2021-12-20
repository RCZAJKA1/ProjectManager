namespace ProjectManager.Business.Tests.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using FluentValidation;
    using FluentValidation.Results;

    using Microsoft.Extensions.Logging;

    using Moq;

    using ProjectManager.Business.Models;
    using ProjectManager.Business.Services;
    using ProjectManager.Data.Repositories;

    using Xunit;

    public sealed class ProjectActionServiceTests
    {
        private MockRepository mockRepository;

        private Mock<ILogger<ProjectActionService>> mockLogger;
        private Mock<IValidator<ProjectAction>> mockValidator;
        private Mock<IProjectActionRepository> mockProjectActionRepository;

        public ProjectActionServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockLogger = this.mockRepository.Create<ILogger<ProjectActionService>>();
            this.mockValidator = this.mockRepository.Create<IValidator<ProjectAction>>();
            this.mockProjectActionRepository = this.mockRepository.Create<IProjectActionRepository>();
        }

        [Fact]
        public void Ctor_LoggerNull_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>("logger", () => new ProjectActionService(null, this.mockValidator.Object, this.mockProjectActionRepository.Object));

            Assert.Equal("Value cannot be null. (Parameter 'logger')", exception.Message);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Ctor_ValidatorNull_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>("validator", () => new ProjectActionService(this.mockLogger.Object, null, this.mockProjectActionRepository.Object));

            Assert.Equal("Value cannot be null. (Parameter 'validator')", exception.Message);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Ctor_RepositoryNull_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>("repository", () => new ProjectActionService(this.mockLogger.Object, this.mockValidator.Object, null));

            Assert.Equal("Value cannot be null. (Parameter 'repository')", exception.Message);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Ctor_ValidParametersl_CreatesNewInstance()
        {
            ProjectActionService service = this.CreateService();

            Assert.Equal(service.Logger, this.mockLogger.Object);
            Assert.Equal(service.Validator, this.mockValidator.Object);
            Assert.Equal(service.Repository, this.mockProjectActionRepository.Object);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task AddProjectActionAsync_ProjectActionNull_ThrowsAsync()
        {
            CancellationToken cancellationToken = new CancellationToken(false);

            ProjectActionService service = this.CreateService();

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>("projectAction", async () => await service.AddProjectActionAsync(null, cancellationToken).ConfigureAwait(false)).ConfigureAwait(false);

            Assert.Equal("Value cannot be null. (Parameter 'projectAction')", exception.Message);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task AddProjectActionAsync_ValidationFails_ThrowsAsync()
        {
            ProjectAction projectAction = new ProjectAction();
            CancellationToken cancellationToken = new CancellationToken(false);
            ValidationResult validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("testProperty1", "testError1"));

            this.mockValidator.Setup(x => x.ValidateAsync(It.Is<ProjectAction>(y => y == projectAction), It.Is<CancellationToken>(y => y == cancellationToken))).ReturnsAsync(validationResult);

            ProjectActionService service = this.CreateService();

            ValidationException exception = await Assert.ThrowsAsync<ValidationException>(async () => await service.AddProjectActionAsync(projectAction, cancellationToken).ConfigureAwait(false)).ConfigureAwait(false);

            Assert.Equal($"Validation failed: {Environment.NewLine} -- testProperty1: testError1 Severity: Error", exception.Message);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task AddProjectActionAsync_ValidationSuccessful_AddsProjectActionAsync()
        {
            ProjectAction projectAction = new ProjectAction();
            CancellationToken cancellationToken = new CancellationToken(false);
            ValidationResult validationResult = new ValidationResult();

            this.mockValidator.Setup(x => x.ValidateAsync(It.Is<ProjectAction>(y => y == projectAction), It.Is<CancellationToken>(y => y == cancellationToken))).ReturnsAsync(validationResult);
            this.mockProjectActionRepository.Setup(x => x.SaveActionAsync(It.Is<ProjectAction>(y => y == projectAction), It.Is<CancellationToken>(y => y == cancellationToken))).Returns(Task.CompletedTask);

            ProjectActionService service = this.CreateService();
            await service.AddProjectActionAsync(projectAction, cancellationToken).ConfigureAwait(false);

            this.mockRepository.VerifyAll();
        }

        private ProjectActionService CreateService()
        {
            return new ProjectActionService(
                this.mockLogger.Object,
                this.mockValidator.Object,
                this.mockProjectActionRepository.Object);
        }
    }
}
