using Azure;
using FakeItEasy;
using FootBallManagerAPI.Controllers;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Models;
using FootBallManagerAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using FootBallManagerAPI.Repositories;
using Moq;
using MockQueryable.Moq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class UserRoleControllerTest
    {
        private readonly IUserRolesRepository _userRoleRepos;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly UserRolesRepository _repo;
        public UserRoleControllerTest() {
            this._userRoleRepos = A.Fake<IUserRolesRepository>();
            var mock = GetFakeUserRoleList(false).BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Userroles).Returns(mock.Object);
            this._repo = new UserRolesRepository(this._dbContextMock.Object);
        }
        private static List<Userrole> GetFakeUserRoleList(bool isNull)
        {
            if(isNull) return new List<Userrole>();
            return new List<Userrole>() {
                new Userrole() {
                   Id = 1 ,Role="admin",Users=null

                },
                new Userrole() {
                   Id = 2 ,Role="president",Users=null

                }


            };
        }

        [TestMethod]

        #region GetUserroles()
        public async Task UserRoleController_GetAllUserRole_ReturnOK()
        {
            //Arrange
            UserRolesRepository res = new UserRolesRepository(A.Fake<FootBallManagerV2Context>());

            var userRoles = await _repo.GetAll();

            A.CallTo(() => _userRoleRepos.GetAll()).Returns(userRoles);

            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.GetUserroles();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UserRoleController_GetAllUserRole_ReturnsProblemResultOnException()
        {
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            UserRolesRepository _repo;
            var mock = GetFakeUserRoleList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Userroles).Returns(mock.Object);
            _repo = new UserRolesRepository(_dbContextMock.Object);
            await _repo.GetAll();
            // Arrange

            A.CallTo(() => _userRoleRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new UserrolesController(_userRoleRepos);

            // Act
            var result = await controller.GetUserroles();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region GetUserrole(int id)
        [TestMethod]
        public async Task UserRoleController_GetUserRoleById_ReturnOK()
        {
            //Arrange
            var userRole = new Userrole() { Id = 1 ,Role="admin",Users=null};

            A.CallTo(() => _userRoleRepos.GetById(1)).Returns(userRole);

            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.GetUserrole(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task UserRoleController_GetUserRoleById_ReturnNotFound()
        {
            //Arrange
         
            A.CallTo(() => _userRoleRepos.GetById(1)).Returns<Userrole>(null);

            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.GetUserrole(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task UserRoleController_GetUserRoleById_ReturnsProblemResultOnException()
        {
            //Arrange

            A.CallTo(() => _userRoleRepos.GetById(1)).Throws<Exception>();

            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.GetUserrole(1);

            //Assert
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region UpdateUserrole(int id, Userrole userrole)
        [TestMethod]
        public async Task UserRoleController_UpdateUserRole_ReturnNoContent()
        {
            //Arrange
            Userrole userRole =await _userRoleRepos.GetById(0) ;
            await _repo.Update(userRole);
            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.UpdateUserrole(0, userRole);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task UserRoleController_UpdateUserRole_ReturnBadRequest()
        {
            //Arrange
            Userrole userRole = await _userRoleRepos.GetById(0);

            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.UpdateUserrole(3, userRole);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task UserRoleController_UpdateUserRole_ReturnNotFound()
        {
            //Arrange
            Userrole userRole = await _userRoleRepos.GetById(2);
            userRole.Id = 2;
            A.CallTo(() => _userRoleRepos.GetById(2)).Throws<Exception>();
            A.CallTo(()=>_userRoleRepos.Update(userRole)).Throws<DbUpdateConcurrencyException>();
            var _controller = new UserrolesController(_userRoleRepos);

            //Act
            var result = await _controller.UpdateUserrole(2, userRole);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UserRoleController_UpdateUserrole_ReturnsProblemResultOnException()
        {
            //Arrange
            Userrole userRole = await _userRoleRepos.GetById(2);
            userRole.Id = 2;
            A.CallTo(() => _userRoleRepos.GetById(2)).Returns(userRole);
            A.CallTo(() => _userRoleRepos.Update(userRole)).Throws<DbUpdateConcurrencyException>();
            var _controller = new UserrolesController(_userRoleRepos);

            //Act
            var result = await _controller.UpdateUserrole(2, userRole);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region PatchUserRole(int id, JsonPatchDocument updateUserRole)
        [TestMethod]
        public async Task UserRoleController_PatchUserRole_ReturnNoContent()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("ROLE","player");
            A.CallTo(() => _userRoleRepos.Patch(5,update)).Returns(true);
            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.PatchUserRole(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task UserRoleController_PatchUserRole_ReturnNotFound()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            UserRolesRepository _repo;
            var mock = GetFakeUserRoleList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Userroles).Returns(mock.Object);
            _repo = new UserRolesRepository(_dbContextMock.Object);
            await _repo.Patch(5,update);

            update.Replace("ROLE", "player");
            A.CallTo(() => _userRoleRepos.Patch(5, update)).Returns(false);
            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.PatchUserRole(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UserRoleController_PatchUserRole_ReturnsProblemResultOnException()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("ROLE", "player");
            A.CallTo(() => _userRoleRepos.Patch(5, update)).Throws<Exception>();
            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.PatchUserRole(5, update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region CreateNewUserrole(Userrole userrole)
        [TestMethod]
        public async Task UserRoleController_CreateNewUserrole_ReturnOK()
        {
            //Arrange
            Userrole newUserRole =  new Userrole() { Id = 1, Role = "admin", Users = null };
            A.CallTo(() => _userRoleRepos.Create(newUserRole)).Returns(await _repo.Create(newUserRole));
            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.CreateNewUserrole(newUserRole);
            
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task UserRoleController_CreateNewUserrole_ReturnsProblemResultOnException()
        {
            //Arrange
            Userrole newUserRole = A.Fake<Userrole>();
            A.CallTo(() => _userRoleRepos.Create(newUserRole)).Throws<Exception>();
            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.CreateNewUserrole(newUserRole);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion


        #region DeleteUserrole(int id)
        [TestMethod]
        public async Task UserRoleController_DeleteUserrole_ReturnNoContent()
        {
            //Arrange
            int idUserRole = 1;
            await _repo.Delete(idUserRole);
            A.CallTo(() => _userRoleRepos.Delete(idUserRole)).Returns(true);
            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.DeleteUserrole(idUserRole);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task UserRoleController_DeleteUserrole_ReturnNotFound()
        {
            //Arrange
            int idUserRole = 1;
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            UserRolesRepository _repo;
            var mock = GetFakeUserRoleList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Userroles).Returns(mock.Object);
            _repo = new UserRolesRepository(_dbContextMock.Object);
            await _repo.Delete(1);
            A.CallTo(() => _userRoleRepos.Delete(idUserRole)).Returns(false);
            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.DeleteUserrole(idUserRole);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UserRoleController_DeleteUserrole_ReturnsProblemResultOnException()
        {
            //Arrange
            int idUserRole = 1;
            A.CallTo(() => _userRoleRepos.Delete(idUserRole)).Throws<Exception>();
            var _controller = new UserrolesController(_userRoleRepos);
            //Act
            var result = await _controller.DeleteUserrole(idUserRole);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion
    }
}
