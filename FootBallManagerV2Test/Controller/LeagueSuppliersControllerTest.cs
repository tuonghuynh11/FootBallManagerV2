﻿using FakeItEasy;
using FootBallManagerAPI.Controllers;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Models;
using FootBallManagerAPI.Repositories;
using FootBallManagerAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class LeagueSuppliersControllerTest
    {
        private readonly ILeagueSupplierRepository _leagueSupplierRepos;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly LeagueSupplierRepository _repo;
        public LeagueSuppliersControllerTest()
        {
            this._leagueSupplierRepos = A.Fake<ILeagueSupplierRepository>();
            var mock = GetFakeLeagueSupplierList(false).BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Leaguesuppliers).Returns(mock.Object);
            this._repo = new LeagueSupplierRepository(this._dbContextMock.Object);
        }
        private static List<Leaguesupplier> GetFakeLeagueSupplierList(bool isNull)
        {
            if (isNull)
                return new List<Leaguesupplier>();
            return new List<Leaguesupplier>() {
               new Leaguesupplier() { 
                   IdLeague = 1,
                   IdSupplier = 1,
                   Duration = 5,
                   StartDate = DateTime.Now,
                   EndDate = DateTime.Now.AddYears(10),
                   Status = 1,
                   IdLeagueNavigation = null,
                   IdSupplierNavigation = null 
               },
                new Leaguesupplier() {
                     IdLeague = 2,
                   IdSupplier = 2,
                   Duration = 5,
                   StartDate = DateTime.Now,
                   EndDate = DateTime.Now.AddYears(10),
                   Status = 1,
                   IdLeagueNavigation = null,
                   IdSupplierNavigation = null

                }


            };
        }

        #region GetLeaguesuppliers()
        [TestMethod]
        public async Task LeagueSuppliersController_GetLeaguesuppliers_ReturnOK()
        {
            //Arrange
            LeagueSupplierRepository res = new LeagueSupplierRepository(A.Fake<FootBallManagerV2Context>());

            var leagueSupplier = await _repo.GetAll();

            A.CallTo(() => _leagueSupplierRepos.GetAll()).Returns(leagueSupplier);

            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.GetLeaguesuppliers();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_GetLeaguesuppliers_ReturnsProblemResultOnException()
        {
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            LeagueSupplierRepository _repo;
            var mock = GetFakeLeagueSupplierList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Leaguesuppliers).Returns(mock.Object);
            _repo = new LeagueSupplierRepository(_dbContextMock.Object);

            await _repo.GetAll();
            // Arrange

            A.CallTo(() => _leagueSupplierRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new LeagueSuppliersController(_leagueSupplierRepos);

            // Act
            var result = await controller.GetLeaguesuppliers();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region GetLeaguesupplier(int idSupplier, int idLeague)
        [TestMethod]
        public async Task LeagueSuppliersController_GetLeaguesupplierById_ReturnOK()
        {
            //Arrange
            var leagueSupplier = await _repo.GetById(1, 1);

            A.CallTo(() => _leagueSupplierRepos.GetById(1, 1)).Returns(leagueSupplier);

            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.GetLeaguesupplier(1, 1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task LeagueSuppliersController_GetLeaguesupplierById_ReturnNotFound()
        {
            //Arrange

            A.CallTo(() => _leagueSupplierRepos.GetById(1, 1)).Returns<Leaguesupplier>(null);

            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.GetLeaguesupplier(1, 1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task LeagueSuppliersController_GetLeaguesupplierById_ReturnsProblemResultOnException()
        {
            //Arrange

            A.CallTo(() => _leagueSupplierRepos.GetById(1, 1)).Throws<Exception>();

            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);

            //Act
            var result = await _controller.GetLeaguesupplier(1, 1);



            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region UpdateLeaguesupplier(int idSupplier, int idLeague, Leaguesupplier leaguesupplier)
        [TestMethod]
        public async Task LeagueSuppliersController_UpdateLeaguesupplier_ReturnNoContent()
        {
            //Arrange
            var leagueSupplier = A.Fake<Leaguesupplier>();
            leagueSupplier.IdSupplier = 1;
            leagueSupplier.IdLeague = 1;
            await _repo.Update(leagueSupplier);
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.UpdateLeaguesupplier(leagueSupplier.IdSupplier, leagueSupplier.IdLeague, leagueSupplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_UpdateLeaguesupplier_ReturnBadRequest()
        {
            //Arrange
            var leagueSupplier = A.Fake<Leaguesupplier>();
            leagueSupplier.IdSupplier = 1;
            leagueSupplier.IdLeague = 1;
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.UpdateLeaguesupplier(2, 1, leagueSupplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task LeagueSuppliersController_UpdateLeaguesupplier_ReturnNotFound()
        {
            //Arrange
            var leagueSupplier = A.Fake<Leaguesupplier>();
            leagueSupplier.IdSupplier = 2;
            leagueSupplier.IdLeague = 2;
            A.CallTo(() => _leagueSupplierRepos.GetById(2, 2)).Throws<Exception>();
            A.CallTo(() => _leagueSupplierRepos.Update(leagueSupplier)).Throws<DbUpdateConcurrencyException>();
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);

            //Act
            var result = await _controller.UpdateLeaguesupplier(2, 2, leagueSupplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_UpdateLeaguesupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            var leagueSupplier = A.Fake<Leaguesupplier>();
            leagueSupplier.IdSupplier = 2;
            leagueSupplier.IdLeague = 2;

            A.CallTo(() => _leagueSupplierRepos.GetById(2, 2)).Returns(leagueSupplier);
            A.CallTo(() => _leagueSupplierRepos.Update(leagueSupplier)).Throws<DbUpdateConcurrencyException>();
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);


            //Act
            var result = await _controller.UpdateLeaguesupplier(2,2, leagueSupplier);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task LeagueSuppliersController_UpdateLeaguesupplier_ReturnsProblemResultOnNullObject()
        {
            //Arrange


            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);


            //Act
            var result = await _controller.UpdateLeaguesupplier(2, 2, null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region  PatchLeagueSupplier(int idSupplier, int idLeague, JsonPatchDocument leagueSupplierModel)
        [TestMethod]
        public async Task LeagueSuppliersController_PatchLeagueSupplier_ReturnNoContent()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("status", 2);
            await _repo.Patch(5, 5, update);
            A.CallTo(() => _leagueSupplierRepos.Patch(5, 5, update)).Returns(true);
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.PatchLeagueSupplier(5, 5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_PatchLeagueSupplier_ReturnBadRequest()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("status", 2);

            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            LeagueSupplierRepository _repo;
            var mock = GetFakeLeagueSupplierList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Leaguesuppliers).Returns(mock.Object);
            _repo = new LeagueSupplierRepository(_dbContextMock.Object);

            await _repo.Patch(5,5,update);
            A.CallTo(() => _leagueSupplierRepos.Patch(5, 5, update)).Returns(false);

            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);

            //Act
            var result = await _controller.PatchLeagueSupplier(5, 5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_PatchLeagueSupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("status", 2);
            A.CallTo(() => _leagueSupplierRepos.Patch(5, 5, update)).Throws<Exception>();
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);

            //Act
            var result = await _controller.PatchLeagueSupplier(5, 5, update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region CreateNewLeagueSupplier(Leaguesupplier leaguesupplier)
        [TestMethod]
        public async Task LeagueSuppliersController_CreateNewLeagueSupplier_ReturnOK()
        {
            //Arrange
            var newLeagueSupplier = new Leaguesupplier() { IdLeague = 1, IdSupplier = 1, Duration = 5, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(10), Status = 1, IdLeagueNavigation = null, IdSupplierNavigation = null };

            var leagueSupplier = await _repo.Create(newLeagueSupplier);
           A.CallTo(() => _leagueSupplierRepos.Create(newLeagueSupplier)).Returns(newLeagueSupplier.IdSupplier);
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.PostLeaguesupplier(newLeagueSupplier);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_CreateNewLeagueSupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            var leagueSupplier = A.Fake<Leaguesupplier>();
            leagueSupplier.IdSupplier = 1;
            leagueSupplier.IdLeague = 1;
            A.CallTo(() => _leagueSupplierRepos.Create(leagueSupplier)).Throws<Exception>();
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.PostLeaguesupplier(null);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion


        #region DeleteLeaguesupplier(int idSupplier, int idLeague)
        [TestMethod]
        public async Task LeagueSuppliersController_DeleteLeaguesupplier_ReturnNoContent()
        {
            //Arrange
            int IdSupplier = 1;
            int IdLeague = 1;
            await _repo.Delete(IdSupplier, IdLeague);
            A.CallTo(() => _leagueSupplierRepos.Delete(IdSupplier, IdLeague)).Returns(true);
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.DeleteLeaguesupplier(IdSupplier, IdLeague);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_DeleteLeaguesupplier_ReturnNotFound()
        {
            //Arrange
            int IdSupplier = 1;
            int IdLeague = 1;
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            LeagueSupplierRepository _repo;
            var mock = GetFakeLeagueSupplierList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Leaguesuppliers).Returns(mock.Object);
            _repo = new LeagueSupplierRepository(_dbContextMock.Object);

            await _repo.Delete(IdSupplier,IdLeague);
            A.CallTo(() => _leagueSupplierRepos.Delete(IdSupplier, IdLeague)).Returns(false);

            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);

            //Act
            var result = await _controller.DeleteLeaguesupplier(IdSupplier, IdLeague);


            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_DeleteLeaguesupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            int IdSupplier = 1;
            int IdLeague = 1;
            A.CallTo(() => _leagueSupplierRepos.Delete(IdSupplier, IdLeague)).Throws<Exception>();
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.DeleteLeaguesupplier(IdSupplier, IdLeague);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion
    }
}
