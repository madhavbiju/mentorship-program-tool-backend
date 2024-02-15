using mentorship_program_tool.Controllers;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.StatusService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace mentorship_program_tool.Tests
{
    public class StatusServiceTests
    {
        private readonly Mock<IStatusService> _mockStatusService;
        private readonly StatusController _controller;

        public StatusServiceTests()
        {
            _mockStatusService = new Mock<IStatusService>();
            _controller = new StatusController(_mockStatusService.Object);
        }

        [Fact]
        public void GetStatus_ReturnsAllStatuses()
        {
            // Arrange
            _mockStatusService.Setup(service => service.GetStatus())
                .Returns(new List<Status> { new Status(), new Status() });

            // Act
            var result = _controller.GetStatuss();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Status>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public void GetStatusById_StatusExists_ReturnsStatus()
        {
            // Arrange
            int testStatusId = 1;
            _mockStatusService.Setup(service => service.GetStatusById(testStatusId))
                .Returns(new Status { StatusID = testStatusId });

            // Act
            var result = _controller.GetStatusById(testStatusId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Status>(okResult.Value);
            Assert.Equal(testStatusId, returnValue.StatusID);
        }

        [Fact]
        public void GetStatusById_StatusDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _mockStatusService.Setup(service => service.GetStatusById(It.IsAny<int>()))
                .Returns((Status)null);

            // Act
            var result = _controller.GetStatusById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AddStatus_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var statusToAdd = new Status { StatusID = 1 };

            // Act
            var result = _controller.AddStatus(statusToAdd);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetStatusById", createdAtActionResult.ActionName);
            Assert.Equal(statusToAdd.StatusID, ((Status)createdAtActionResult.Value).StatusID);
        }

        [Fact]
        public void UpdateStatus_MismatchedId_ReturnsBadRequest()
        {
            // Arrange
            var statusToUpdate = new Status { StatusID = 1 };

            // Act
            var result = _controller.UpdateStatus(2, statusToUpdate); // Mismatched ID

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }


    }
}
