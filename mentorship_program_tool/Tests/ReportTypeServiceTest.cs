using Xunit;
using Moq;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using mentorship_program_tool.Services.ReportTypeService;
using System.Collections.Generic;
using System.Linq;

namespace mentorship_program_tool.Tests
{
    public class ReportTypeServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ReportTypeService _reportTypeService;

        public ReportTypeServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _reportTypeService = new ReportTypeService(_mockUnitOfWork.Object);
        }

        [Fact]
        public void GetReportType_ReturnsAllReportTypes()
        {
            // Arrange
            var reportTypes = new List<ReportType>
            {
                new ReportType { ReportTypeID = 1, ReportName = "Type1" },
                new ReportType { ReportTypeID = 2, ReportName = "Type2" }
            };

            _mockUnitOfWork.Setup(u => u.ReportType.GetAll()).Returns(reportTypes.AsQueryable());

            // Act
            var result = _reportTypeService.GetReportType();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetReportTypeById_ValidId_ReturnsReportType()
        {
            // Arrange
            var reportType = new ReportType { ReportTypeID = 1, ReportName = "Type1" };

            _mockUnitOfWork.Setup(u => u.ReportType.GetById(1)).Returns(reportType);

            // Act
            var result = _reportTypeService.GetReportTypeById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Type1", result.ReportName);
        }


        [Fact]
        public void UpdateReportType_ValidId_UpdatesReportType()
        {
            // Arrange
            var existingReportType = new ReportType { ReportTypeID = 1, ReportName = "OldType" };
            var reportTypeDto = new ReportType { ReportName = "UpdatedType" };

            _mockUnitOfWork.Setup(u => u.ReportType.GetById(1)).Returns(existingReportType);

            // Act
            _reportTypeService.UpdateReportType(1, reportTypeDto);

            // Assert
            _mockUnitOfWork.Verify(u => u.Complete(), Times.Once);
            Assert.Equal("UpdatedType", existingReportType.ReportName);
        }

        [Fact]
        public void DeleteReport_ValidId_DeletesReportType()
        {
            // Arrange
            var reportType = new ReportType { ReportTypeID = 1, ReportName = "TypeToDelete" };

            _mockUnitOfWork.Setup(u => u.ReportType.GetById(1)).Returns(reportType);

            // Act
            _reportTypeService.DeleteReport(1);

            // Assert
            _mockUnitOfWork.Verify(u => u.ReportType.Delete(It.IsAny<ReportType>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.Complete(), Times.Once);
        }
    }
}
