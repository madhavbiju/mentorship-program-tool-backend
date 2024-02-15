using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.RoleService;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Tests
{
    public class RoleServiceTests
    {
        private readonly RoleService _roleService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public RoleServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _roleService = new RoleService(_mockUnitOfWork.Object);
        }

        [Fact]
        public void GetRoles_ReturnsAllRoles()
        {
            // Arrange
            var roles = new List<Role>
            {
                new Role { RoleID = 1, RoleName = "Admin" },
                new Role { RoleID = 2, RoleName = "User" }
            };
            _mockUnitOfWork.Setup(u => u.Role.GetAll()).Returns(roles);

            // Act
            var result = _roleService.GetRoles();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.RoleName == "Admin");
            Assert.Contains(result, r => r.RoleName == "User");
        }

        [Fact]
        public void GetRoleById_ValidId_ReturnsRole()
        {
            // Arrange
            int roleId = 1;
            var role = new Role { RoleID = roleId, RoleName = "Admin" };
            _mockUnitOfWork.Setup(u => u.Role.GetById(roleId)).Returns(role);

            // Act
            var result = _roleService.GetRoleById(roleId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(roleId, result.RoleID);
            Assert.Equal("Admin", result.RoleName);
        }


        [Fact]
        public void UpdateRole_ExistingRole_UpdatesRole()
        {
            // Arrange
            int roleId = 1;
            var existingRole = new Role { RoleID = roleId, RoleName = "ExistingRole" };
            var updatedRole = new Role { RoleID = roleId, RoleName = "UpdatedRole" };
            _mockUnitOfWork.Setup(u => u.Role.GetById(roleId)).Returns(existingRole);

            // Act
            _roleService.UpdateRole(roleId, updatedRole);

            // Assert
            _mockUnitOfWork.Verify(u => u.Complete(), Times.Once);
            Assert.Equal("UpdatedRole", existingRole.RoleName);
        }

        [Fact]
        public void DeleteRole_ExistingRole_DeletesRole()
        {
            // Arrange
            int roleId = 1;
            var roleToDelete = new Role { RoleID = roleId, RoleName = "RoleToDelete" };
            _mockUnitOfWork.Setup(u => u.Role.GetById(roleId)).Returns(roleToDelete);

            // Act
            _roleService.DeleteRole(roleId);

            // Assert
            _mockUnitOfWork.Verify(u => u.Role.Delete(It.IsAny<Role>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.Complete(), Times.Once);
        }
    }
}
