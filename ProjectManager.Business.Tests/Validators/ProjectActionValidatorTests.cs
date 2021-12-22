namespace ProjectManager.Business.Tests.Validators
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using FluentValidation.Results;

    using Moq;

    using ProjectManager.Business.Enums;
    using ProjectManager.Business.Models;
    using ProjectManager.Business.Validators;

    using Xunit;

    [ExcludeFromCodeCoverage]
    public sealed class ProjectActionValidatorTests
    {
        private readonly MockRepository mockRepository;

        public ProjectActionValidatorTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private DateTime Now => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData((string)default)]
        [InlineData(null)]
        public async Task ValidateAsync_OwnerInvalid_ReturnsValidationFailure(string owner)
        {
            ProjectAction projectAction = this.CreateProjectAction();
            projectAction.Owner = owner;

            ProjectActionValidator validator = this.CreateProjectActionValidator();

            ValidationResult result = await validator.ValidateAsync(projectAction).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Equal("'Owner' must not be empty.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task ValidateAsync_OwnerContainsInvalidChars_ReturnsValidationFailure()
        {
            ProjectAction projectAction = this.CreateProjectAction();
            projectAction.Owner = "Jim123";

            ProjectActionValidator validator = this.CreateProjectActionValidator();

            ValidationResult result = await validator.ValidateAsync(projectAction).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Equal("The Owner must only contain letters.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task ValidateAsync_OwnerExceedsMaxChars_ReturnsValidationFailure()
        {
            ProjectAction projectAction = this.CreateProjectAction();
            projectAction.Owner = new string('a', 31);

            ProjectActionValidator validator = this.CreateProjectActionValidator();

            ValidationResult result = await validator.ValidateAsync(projectAction).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Equal("'Owner Length' must be less than or equal to '30'.", result.Errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData((string)default)]
        [InlineData(null)]
        public async Task ValidateAsync_DescriptionEmpty_ReturnsValidationFailure(string description)
        {
            ProjectAction projectAction = this.CreateProjectAction();
            projectAction.Description = description;

            ProjectActionValidator validator = this.CreateProjectActionValidator();

            ValidationResult result = await validator.ValidateAsync(projectAction).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Equal("'Description' must not be empty.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task ValidateAsync_DescriptionExceedsMaxChars_ReturnsValidationFailure()
        {
            ProjectAction projectAction = this.CreateProjectAction();
            projectAction.Description = new string('a', 256);

            ProjectActionValidator validator = this.CreateProjectActionValidator();

            ValidationResult result = await validator.ValidateAsync(projectAction).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Equal("'Description Length' must be less than or equal to '255'.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task ValidateAsync_IdExceedsMin_ReturnsValidationFailure()
        {
            ProjectAction projectAction = this.CreateProjectAction();
            projectAction.Id = -1;

            ProjectActionValidator validator = this.CreateProjectActionValidator();

            ValidationResult result = await validator.ValidateAsync(projectAction).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Equal("'Id Value' must be greater than '0'.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task ValidateAsync_DateDueExceedsMin_ReturnsValidationFailure()
        {
            ProjectAction projectAction = this.CreateProjectAction();
            projectAction.DateDue = new DateTime(1776, DateTimeKind.Utc);

            ProjectActionValidator validator = this.CreateProjectActionValidator();

            ValidationResult result = await validator.ValidateAsync(projectAction).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Equal("'Date Due Value' must be greater than or equal to '12/31/1899 7:00:00 PM'.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task ValidateAsync_DateOpenedExceedsMin_ReturnsValidationFailure()
        {
            ProjectAction projectAction = this.CreateProjectAction();
            projectAction.DateOpened = new DateTime(1776, DateTimeKind.Utc);

            ProjectActionValidator validator = this.CreateProjectActionValidator();

            ValidationResult result = await validator.ValidateAsync(projectAction).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Equal("'Date Opened Value' must be greater than or equal to '12/31/1899 7:00:00 PM'.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task ValidateAsync_DateOpenedExceedsMax_ReturnsValidationFailure()
        {
            ProjectAction projectAction = this.CreateProjectAction();
            projectAction.DateOpened = new DateTime(3000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            projectAction.DateClosed = projectAction.DateOpened.Value.AddDays(1);

            ProjectActionValidator validator = this.CreateProjectActionValidator();

            ValidationResult result = await validator.ValidateAsync(projectAction).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Equal("'Date Opened Value' must be less than or equal to '12/19/2021 12:00:00 AM'.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task ValidateAsync_DateClosedHasValueAndDateOpenedEmpty_ReturnsValidationFailure()
        {
            ProjectAction projectAction = this.CreateProjectAction();
            projectAction.DateOpened = null;

            ProjectActionValidator validator = this.CreateProjectActionValidator();

            ValidationResult result = await validator.ValidateAsync(projectAction).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Equal("The DateOpened must exist if the DateClosed exists.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task ValidateAsync_DateClosedExceedsMax_ReturnsValidationFailure()
        {
            ProjectAction projectAction = this.CreateProjectAction();
            projectAction.DateClosed = DateTime.Now.AddDays(4);

            ProjectActionValidator validator = this.CreateProjectActionValidator();

            ValidationResult result = await validator.ValidateAsync(projectAction).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Equal($"'Date Closed Value' must be less than or equal to '{this.Now}'.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task ValidateAsync_DateClosedBeforeDateOpened_ReturnsValidationFailure()
        {
            ProjectAction projectAction = this.CreateProjectAction();
            projectAction.DateClosed = DateTime.Now.AddDays(-5);
            projectAction.DateOpened = DateTime.Now.AddDays(-1);

            ProjectActionValidator validator = this.CreateProjectActionValidator();

            ValidationResult result = await validator.ValidateAsync(projectAction).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Equal("The DateClosed must occur on or after the DateOpened.", result.Errors[0].ErrorMessage);
        }

        private ProjectActionValidator CreateProjectActionValidator()
        {
            return new ProjectActionValidator();
        }

        private ProjectAction CreateProjectAction()
        {
            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

            return new ProjectAction
            {
                Id = 123,
                DateClosed = now,
                DateDue = now.AddDays(-1),
                DateOpened = now.AddDays(-2),
                Description = "testDescription",
                Owner = "testOwner",
                Priority = ActionPriority.High,
                Resolution = "testResolution",
                Status = ActionStatus.Open
            };
        }
    }
}
