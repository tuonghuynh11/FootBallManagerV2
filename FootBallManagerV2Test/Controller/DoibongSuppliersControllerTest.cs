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
    public class DoibongSuppliersControllerTest
    {
        private readonly IDoiBongSupplierRepository _doibongSupplierRepos;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly DoiBongSupplierRepository _repo;
        public DoibongSuppliersControllerTest()
        {
            this._doibongSupplierRepos = A.Fake<IDoiBongSupplierRepository>();
            var mock = GetFakeDoiBongSupplierList(false).BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Doibongsuppliers).Returns(mock.Object);
            this._repo = new DoiBongSupplierRepository(this._dbContextMock.Object);
        }
        private static List<Doibongsupplier> GetFakeDoiBongSupplierList(bool isNull)
        {
            if (isNull)
                return new List<Doibongsupplier>();
            return new List<Doibongsupplier>() {
              new Doibongsupplier() { 
                  IdDoiBong = "atm",
                  IdSupplier = 1, Duration = 5,
                  StartDate = DateTime.Now, 
                  EndDate = DateTime.Now.AddYears(10), 
                  Status = 1,
                  IdDoiBongNavigation=null,
                  IdSupplierNavigation=null
              },
             new Doibongsupplier() {
                  IdDoiBong = "mc",
                  IdSupplier = 1, Duration = 5,
                  StartDate = DateTime.Now,
                  EndDate = DateTime.Now.AddYears(10),
                  Status = 1,
                  IdDoiBongNavigation=null,
                  IdSupplierNavigation=null
              },


            };
        }

        #region GetDoibongsuppliers()
        [TestMethod]
        public async Task DoibongSuppliersController_GetDoibongsuppliers_ReturnOK()
        {
            //Arrange
            DoiBongSupplierRepository res = new DoiBongSupplierRepository(A.Fake<FootBallManagerV2Context>());

            var doibongSupplier = await _repo.GetAll();

            A.CallTo(() => _doibongSupplierRepos.GetAll()).Returns(doibongSupplier);

            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);
            //Act
            var result = await _controller.GetDoibongsuppliers();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
        [TestMethod]
        public async Task DoibongSuppliersController_GetDoibongsuppliers_ReturnOK_EmptyList()
        {

            //context
             Mock<FootBallManagerV2Context> _dbContextMock;
             DoiBongSupplierRepository _repo;
            var mock = GetFakeDoiBongSupplierList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Doibongsuppliers).Returns(mock.Object);
            _repo = new DoiBongSupplierRepository(_dbContextMock.Object);

            //Arrange
            DoiBongSupplierRepository res = new DoiBongSupplierRepository(A.Fake<FootBallManagerV2Context>());

            var doibongSupplier = await _repo.GetAll();

            A.CallTo(() => _doibongSupplierRepos.GetAll()).Returns(doibongSupplier);

            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);
            //Act
            var result = await _controller.GetDoibongsuppliers();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
        [TestMethod]
        public async Task DoibongSuppliersController_GetDoibongsuppliers_ReturnsProblemResultOnException()
        {
            // Arrange

            A.CallTo(() => _doibongSupplierRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new DoibongSuppliersController(_doibongSupplierRepos);

            // Act
            var result = await controller.GetDoibongsuppliers();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region GetDoibongsupplier(int idSupplier, string idDoiBong)
        [TestMethod]
        public async Task DoibongSuppliersController_GetDoibongsupplierById_ReturnOK()
        {
            //Arrange
            var doibongSupplier = await _repo.GetById(1, "atm");
            A.CallTo(() => _doibongSupplierRepos.GetById(1,"atm")).Returns(doibongSupplier);

            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);
            //Act
            var result = await _controller.GetDoibongsupplier(1, "atm");

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task DoibongSuppliersController_GetDoibongsupplierById_ReturnNotFound()
        {
            //Arrange

            A.CallTo(() => _doibongSupplierRepos.GetById(1,"atm")).Returns<Doibongsupplier>(null);

            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);
            //Act
            var result = await _controller.GetDoibongsupplier(1, "atm");

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task DoibongSuppliersController_GetDoibongsupplierById_ReturnsProblemResultOnException()
        {
            //Arrange

            A.CallTo(() => _doibongSupplierRepos.GetById(1, "atm")).Throws<Exception>();

            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);
            //Act
            var result = await _controller.GetDoibongsupplier(1, "atm");


            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region UpdateDoibongsupplier(int idSupplier, string idDoiBong, Doibongsupplier doibongsupplier)
        [TestMethod]
        public async Task DoibongSuppliersController_UpdateDoibongsupplier_ReturnNoContent()
        {
            //Arrange
            var doibongSupplier = A.Fake<Doibongsupplier>();
            doibongSupplier.IdSupplier = 1;
            doibongSupplier.IdDoiBong = "atm";
            await _repo.Update(doibongSupplier);
            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);
            //Act
            var result = await _controller.UpdateDoibongsupplier(doibongSupplier.IdSupplier, doibongSupplier.IdDoiBong,doibongSupplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DoibongSuppliersController_UpdateDoibongsupplier_ReturnBadRequest()
        {
            //Arrange
            var doibongSupplier = A.Fake<Doibongsupplier>();
            doibongSupplier.IdSupplier = 1;
            doibongSupplier.IdDoiBong = "atm";
            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);
            //Act
            var result = await _controller.UpdateDoibongsupplier(2,"mc", doibongSupplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task DoibongSuppliersController_UpdateDoibongsupplier_ReturnNotFound()
        {
            //Arrange
            var doibongSupplier = A.Fake<Doibongsupplier>();
            doibongSupplier.IdSupplier = 2;
            doibongSupplier.IdDoiBong = "atm";
            A.CallTo(() => _doibongSupplierRepos.GetById(2,"atm")).Throws<Exception>();
            A.CallTo(() => _doibongSupplierRepos.Update(doibongSupplier)).Throws<DbUpdateConcurrencyException>();
            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);

            //Act
            var result = await _controller.UpdateDoibongsupplier(2, "atm", doibongSupplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DoibongSuppliersController_UpdateDoibongsupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            var doibongSupplier = A.Fake<Doibongsupplier>();
            doibongSupplier.IdSupplier = 2;
            doibongSupplier.IdDoiBong = "atm";

            A.CallTo(() => _doibongSupplierRepos.GetById(2, "atm")).Returns(doibongSupplier);
            A.CallTo(() => _doibongSupplierRepos.Update(doibongSupplier)).Throws<DbUpdateConcurrencyException>();
            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);


            //Act
            var result = await _controller.UpdateDoibongsupplier(2, "atm", doibongSupplier);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DoibongSuppliersController_UpdateDoibongsupplier_ReturnsProblemResultOnNullObject()
        {
            //Arrange


            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);


            //Act
            var result = await _controller.UpdateDoibongsupplier(2, "atm", null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region PatchDoiBongSupplier(int idSupplier, string idDoiBong, JsonPatchDocument doibongSupplierModel)
        [TestMethod]
        public async Task DoibongSuppliersController_PatchDoiBongSupplier_ReturnNoContent()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("status", 2);
            A.CallTo(() => _doibongSupplierRepos.Patch(5,"atm", update)).Returns(true);
            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);
            //Act
            var result = await _controller.PatchDoiBongSupplier(5, "atm", update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DoibongSuppliersController_PatchDoiBongSupplier_ReturnBadRequest()
        {
           
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("status", 2);

            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            DoiBongSupplierRepository _repo;
            var mock = GetFakeDoiBongSupplierList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Doibongsuppliers).Returns(mock.Object);
            _repo = new DoiBongSupplierRepository(_dbContextMock.Object);
            await _repo.Patch(5, "atm", update);

            A.CallTo(() => _doibongSupplierRepos.Patch(5, "atm", update)).Returns(false);
            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);

            //Act
            var result = await _controller.PatchDoiBongSupplier(5, "atm", update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DoibongSuppliersController_PatchDoiBongSupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("status", 2);
            A.CallTo(() => _doibongSupplierRepos.Patch(5, "atm", update)).Throws<Exception>();
            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);

            //Act
            var result = await _controller.PatchDoiBongSupplier(5,"atm", update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion
        #region CreateNewDoiBongSupplier(DoiBongSupplierModel doiBongSupplier)
        [TestMethod]
        public async Task DoibongSuppliersController_CreateNewDoiBongSupplier_ReturnOK()
        {
            //Arrange
            var doibongSupplierModel = new DoiBongSupplierModel() { IdDoiBong = "atm", IdSupplier = 1, Duration = 5, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(10), Status = 1 };
            var doibongSupplier = A.Fake<Doibongsupplier>();
            doibongSupplier.IdSupplier = 1;
            doibongSupplier.IdDoiBong = "atm";
            A.CallTo(() => _doibongSupplierRepos.Create(doibongSupplier)).Returns(await _repo.Create(doibongSupplier));
            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);
            //Act
            var result = await _controller.PostDoibongsupplier(doibongSupplierModel);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task DoibongSuppliersController_CreateNewDoiBongSupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            var doibongSupplier = A.Fake<Doibongsupplier>();
            doibongSupplier.IdSupplier = 1;
            doibongSupplier.IdDoiBong = "atm";
            A.CallTo(() => _doibongSupplierRepos.Create(doibongSupplier)).Throws<Exception>();
            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);
            //Act
            var result = await _controller.PostDoibongsupplier(null);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion


        #region DeleteDoibongsupplier(int idSupplier, string idDoiBong)
        [TestMethod]
        public async Task DoibongSuppliersController_DeleteDoibongsupplier_ReturnNoContent()
        {
           
            //Arrange
            int IdSupplier = 1;
             string IdDoiBong = "atm";
            await _repo.Delete(IdSupplier, IdDoiBong);
            A.CallTo(() => _doibongSupplierRepos.Delete(IdSupplier,IdDoiBong)).Returns(true);
            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);
            //Act
            var result = await _controller.DeleteDoibongsupplier(IdSupplier, IdDoiBong);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DoibongSuppliersController_DeleteDoibongsupplier_ReturnNotFound()
        {
            //Arrange
            int IdSupplier = 1;
            string IdDoiBong = "atm";
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            DoiBongSupplierRepository _repo;
            var mock = GetFakeDoiBongSupplierList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Doibongsuppliers).Returns(mock.Object);
            _repo = new DoiBongSupplierRepository(_dbContextMock.Object);
             await _repo.Delete(IdSupplier, IdDoiBong);
            A.CallTo(() => _doibongSupplierRepos.Delete(IdSupplier, IdDoiBong)).Returns(false);

            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);
            //Act
            var result = await _controller.DeleteDoibongsupplier(IdSupplier, IdDoiBong);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DoibongSuppliersController_DeleteDoibongsupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            int IdSupplier = 1;
            string IdDoiBong = "atm";
            A.CallTo(() => _doibongSupplierRepos.Delete(IdSupplier, IdDoiBong)).Throws<Exception>();
            var _controller = new DoibongSuppliersController(_doibongSupplierRepos);
            //Act
            var result = await _controller.DeleteDoibongsupplier(IdSupplier, IdDoiBong);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion
    }
}
