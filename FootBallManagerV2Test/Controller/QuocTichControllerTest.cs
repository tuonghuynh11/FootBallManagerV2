﻿using FakeItEasy;
using FootBallManagerAPI.Controllers;
using FootBallManagerAPI.Entities;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class QuocTichControllerTest
    {
        private readonly IQuocTichRepository _quocTichRepos;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly QuocTichRepository _repo;
        public QuocTichControllerTest()
        {
            this._quocTichRepos = A.Fake<IQuocTichRepository>();
            var mock = GetFakeQuocTichList(false).BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Quoctiches).Returns(mock.Object);
            this._repo = new QuocTichRepository(this._dbContextMock.Object);
        }
        private static List<Quoctich> GetFakeQuocTichList(bool isNull)
        {
            if (isNull) return new List<Quoctich>(); 
            return new List<Quoctich>() {
                new Quoctich() {
                Cauthus=null,
                Diadiems=null,
                Doibongs=null,
                Huanluyenviens= null,
                Id=1,
                Leagues=null,
                Tenquocgia = "Anh"
            },
            new Quoctich() {
                  Cauthus=null,
                Diadiems=null,
                Doibongs=null,
                Huanluyenviens= null,
                Id=1,
                Leagues=null,
                Tenquocgia = "Viet Nam"

                }


            };
        }

        #region GetQuoctiches()
        [TestMethod]
        public async Task QuocTichController_GetQuoctiches_ReturnOK()
        {
            //Arrange
            QuocTichRepository res = new QuocTichRepository(A.Fake<FootBallManagerV2Context>());

            var quoctichs = await _repo.GetAll();

            A.CallTo(() => _quocTichRepos.GetAll()).Returns(quoctichs);

            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.GetQuoctiches();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task QuocTichController_GetQuoctiches_ReturnsProblemResultOnException()
        {
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            QuocTichRepository _repo;
            var mock = GetFakeQuocTichList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Quoctiches).Returns(mock.Object);
            _repo = new QuocTichRepository(_dbContextMock.Object);
            await _repo.GetAll();
            // Arrange

            A.CallTo(() => _quocTichRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new QuoctichesController(_quocTichRepos);

            // Act
            var result = await controller.GetQuoctiches();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region GetQuoctich(int id)
        [TestMethod]
        public async Task QuocTichController_GetQuoctichById_ReturnOK()
        {
            //Arrange
            var quoctich = await _repo.GetById(1);

            A.CallTo(() => _quocTichRepos.GetById(1)).Returns(quoctich);

            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.GetQuoctich(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task QuocTichController_GetQuoctichById_ReturnNotFound()
        {
            //Arrange

            A.CallTo(() => _quocTichRepos.GetById(1)).Returns<Quoctich>(null);

            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.GetQuoctich(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task QuocTichController_GetQuoctichById_ReturnsProblemResultOnException()
        {
            //Arrange

            A.CallTo(() => _quocTichRepos.GetById(1)).Throws<Exception>();

            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.GetQuoctich(1);

            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region UpdateQuoctich(int id, Quoctich quoctich)
        [TestMethod]
        public async Task QuocTichController_UpdateQuoctich_ReturnNoContent()
        {
            //Arrange
            var quoctich = A.Fake<Quoctich>();
            quoctich.Id = 1;
            quoctich.Tenquocgia = "VietNam";
            await _repo.Update(quoctich);
            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.UpdateQuoctich(quoctich.Id, quoctich);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task QuocTichController_UpdateQuoctich_ReturnBadRequest()
        {
            //Arrange
            var quoctich = A.Fake<Quoctich>();
            quoctich.Id = 1;
            quoctich.Tenquocgia = "VietNam";
            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.UpdateQuoctich(2, quoctich);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task QuocTichController_UpdateQuoctich_ReturnNotFound()
        {
            //Arrange
            var quoctich = A.Fake<Quoctich>();
            quoctich.Id = 2;
            quoctich.Tenquocgia = "VietNam";
            A.CallTo(() => _quocTichRepos.GetById(2)).Throws<Exception>();
            A.CallTo(() => _quocTichRepos.Update(quoctich)).Throws<DbUpdateConcurrencyException>();
            var _controller = new QuoctichesController(_quocTichRepos);

            //Act
            var result = await _controller.UpdateQuoctich(2, quoctich);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task QuocTichController_UpdateQuoctich_ReturnsProblemResultOnException()
        {
            //Arrange
            var quoctich = A.Fake<Quoctich>();
            quoctich.Id = 2;
            quoctich.Tenquocgia = "VietNam";

            A.CallTo(() => _quocTichRepos.GetById(2)).Returns(quoctich);
            A.CallTo(() => _quocTichRepos.Update(quoctich)).Throws<DbUpdateConcurrencyException>();
            var _controller = new QuoctichesController(_quocTichRepos);

            //Act
            var result = await _controller.UpdateQuoctich(2, quoctich);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task QuocTichController_UpdateQuoctich_ReturnsProblemResultOnNullObject()
        {
            //Arrange


            var _controller = new QuoctichesController(_quocTichRepos);

            //Act
            var result = await _controller.UpdateQuoctich(2, null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region PatchQuocTich(int id, JsonPatchDocument quoctichModel)
        [TestMethod]
        public async Task QuocTichController_PatchQuocTich_ReturnNoContent()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("TENQUOCGIA", "Anh");
            await _repo.Patch(5,update);
            A.CallTo(() => _quocTichRepos.Patch(5, update)).Returns(true);
            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.PatchQuocTich(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task QuocTichController_PatchQuocTich_ReturnBadRequest()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("TENQUOCGIA", "Anh");

            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            QuocTichRepository _repo;
            var mock = GetFakeQuocTichList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Quoctiches).Returns(mock.Object);
            _repo = new QuocTichRepository(_dbContextMock.Object);

            await _repo.Patch(5, update);
            A.CallTo(() => _quocTichRepos.Patch(5, update)).Returns(false);
            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.PatchQuocTich(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task QuocTichController_PatchQuocTich_ReturnsProblemResultOnException()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("TENQUOCGIA", "Anh");
            A.CallTo(() => _quocTichRepos.Patch(5, update)).Throws<Exception>();
            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.PatchQuocTich(5, update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region CreateNewQuocTich(Quoctich quoctich)
        [TestMethod]
        public async Task QuocTichController_CreateNewQuocTich_ReturnOK()
        {
            //Arrange
            var quoctich = new Quoctich()
            {
                Cauthus = null,
                Diadiems = null,
                Doibongs = null,
                Huanluyenviens = null,
                Id = 1,
                Leagues = null,
                Tenquocgia = "Anh"
            };
            A.CallTo(() => _quocTichRepos.Create(quoctich)).Returns(await _repo.Create(quoctich));
            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.PostQuoctich(quoctich);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task QuocTichController_CreateNewQuocTich_ReturnsProblemResultOnException()
        {
            //Arrange
            var quoctich = A.Fake<Quoctich>();
            A.CallTo(() => _quocTichRepos.Create(quoctich)).Throws<Exception>();
            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.PostQuoctich(quoctich);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion

        #region DeleteQuoctich(int id)
        [TestMethod]
        public async Task QuocTichController_DeleteQuoctich_ReturnNoContent()
        {
            //Arrange
            int idQuocTich = 1;
            await _repo.Delete(idQuocTich);
            A.CallTo(() => _quocTichRepos.Delete(idQuocTich)).Returns(true);
            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.DeleteQuoctich(idQuocTich);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task QuocTichController_DeleteQuoctich_ReturnNotFound()
        {
            //Arrange
            int idQuocTich = 1;
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            QuocTichRepository _repo;
            var mock = GetFakeQuocTichList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Quoctiches).Returns(mock.Object);
            _repo = new QuocTichRepository(_dbContextMock.Object);
            await _repo.Delete(idQuocTich);
            A.CallTo(() => _quocTichRepos.Delete(idQuocTich)).Returns(false);

            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.DeleteQuoctich(idQuocTich);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task QuocTichController_DeleteQuoctich_ReturnsProblemResultOnException()
        {
            //Arrange
            int idQuocTich = 1;
            A.CallTo(() => _quocTichRepos.Delete(idQuocTich)).Throws<Exception>();
            var _controller = new QuoctichesController(_quocTichRepos);
            //Act
            var result = await _controller.DeleteQuoctich(idQuocTich);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion
    }
}
