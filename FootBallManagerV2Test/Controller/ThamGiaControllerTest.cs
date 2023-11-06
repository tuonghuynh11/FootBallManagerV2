using FakeItEasy;
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
    public class ThamGiaControllerTest
    {

        private readonly IThamGiaRepository _thamGiaRepos;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly ThamGiaRepository _repo;
        public ThamGiaControllerTest()
        {
            this._thamGiaRepos = A.Fake<IThamGiaRepository>();
            var mock = GetFakeThamGiaList(false).BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Thamgia).Returns(mock.Object);
            this._repo = new ThamGiaRepository(this._dbContextMock.Object);
        }
        private static List<Thamgium> GetFakeThamGiaList(bool isNull)
        {
            if(isNull) return new List<Thamgium>();
            return new List<Thamgium>() {
                new Thamgium() {
                      Idcauthu = 1,
                    IdcauthuNavigation=null,
                    Idtran=1,
                    IdtranNavigation=null,
                    Sobanthang=3

                },
                new Thamgium() {
                     Idcauthu = 1,
                    IdcauthuNavigation=null,
                    Idtran=2,
                    IdtranNavigation=null,
                    Sobanthang=3

                }


            };
        }



        #region GetThamgias()
        [TestMethod]
        public async Task ThamgiaController_GetThamgias_ReturnOK()
        {
            //Arrange
            ThamGiaRepository res = new ThamGiaRepository(A.Fake<FootBallManagerV2Context>());

            var thamgias =await _repo.GetAll();

            A.CallTo(() => _thamGiaRepos.GetAll()).Returns(thamgias);

            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.GetThamgias();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task ThamgiaController_GetThamgias_ReturnsProblemResultOnException()
        {
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            ThamGiaRepository _repo;
            var mock = GetFakeThamGiaList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Thamgia).Returns(mock.Object);
            _repo = new ThamGiaRepository(_dbContextMock.Object);
            await _repo.GetAll();
            // Arrange

            A.CallTo(() => _thamGiaRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new ThamgiaController(_thamGiaRepos);

            // Act
            var result = await controller.GetThamgias();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region GetThamgia(int idTran)
        [TestMethod]
        public async Task ThamgiaController_GetThamgiaById_ReturnOK()
        {
            //Arrange
            List<Thamgium> list = await _repo.GetById(1);
            
            A.CallTo(() => _thamGiaRepos.GetById(1)).Returns(list);

            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.GetThamgia(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task ThamgiaController_GetThamgiaById_ReturnNotFound()
        {
            //Arrange

            A.CallTo(() => _thamGiaRepos.GetById(1)).Returns<List<Thamgium>>(null);

            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.GetThamgia(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task ThamgiaController_GetThamgiaById_ReturnsProblemResultOnException()
        {
            //Arrange

            A.CallTo(() => _thamGiaRepos.GetById(1)).Throws<Exception>();

            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.GetThamgia(1);

            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region  UpdateThamgia( Thamgium thamgium)
        [TestMethod]
        public async Task ThamgiaController_UpdateThamgia_ReturnNoContent()
        {
            //Arrange
            Thamgium thamgia = A.Fake<Thamgium>();
            thamgia.Idtran = 2;
            thamgia.Idcauthu = 3;
            thamgia.Sobanthang = 2;
            await _repo.Update(thamgia);
            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.UpdateThamgia(thamgia);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }


        [TestMethod]
        public async Task ThamgiaController_UpdateThamgia_ReturnNotFound()
        {
            //Arrange
            Thamgium thamgia = A.Fake<Thamgium>();
            thamgia.Idtran = 2;
            thamgia.Idcauthu = 3;
            thamgia.Sobanthang = 2;
            A.CallTo(() => _thamGiaRepos.Update(thamgia)).Throws<DbUpdateConcurrencyException>();
            var _controller = new ThamgiaController(_thamGiaRepos);

            //Act
            var result = await _controller.UpdateThamgia(thamgia);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ThamgiaController_UpdateThamgia_ReturnsProblemResultOnException()
        {
            //Arrange
            Thamgium thamgia = A.Fake<Thamgium>();
            thamgia.Idtran = 2;
            thamgia.Idcauthu = 3;
            thamgia.Sobanthang = 2;

            A.CallTo(() => _thamGiaRepos.Update(thamgia)).Throws<Exception>();
            var _controller = new ThamgiaController(_thamGiaRepos);

            //Act
            var result = await _controller.UpdateThamgia(thamgia);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        #endregion


        #region  PatchThamGia(int idTran, int idCauThu, JsonPatchDocument thamGiaModel)
        [TestMethod]
        public async Task ThamgiaController_PatchThamGia_ReturnNoContent()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("SOBANTHANG", 3);
            int idTran = 1;
            int idCauThu = 1;

            A.CallTo(() => _thamGiaRepos.Patch(idTran,idCauThu, update)).Returns(true);
            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.PatchThamGia(idTran, idCauThu, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ThamgiaController_PatchThamGia_ReturnBadRequest()
        {
            //Arrange
            int idTran = 1;
            int idCauThu = 1;
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            ThamGiaRepository _repo;
            var mock = GetFakeThamGiaList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Thamgia).Returns(mock.Object);
            _repo = new ThamGiaRepository(_dbContextMock.Object);
            await _repo.Patch(idTran, idCauThu, update);

            update.Replace("SOBANTHANG", 3);
          


          

            A.CallTo(() => _thamGiaRepos.Patch(idTran, idCauThu, update)).Returns(false);
            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.PatchThamGia(idTran, idCauThu, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task ThamgiaController_PatchThamGia_ReturnsProblemResultOnException()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("SOBANTHANG", 3);
            int idTran = 1;
            int idCauThu = 1;
            A.CallTo(() => _thamGiaRepos.Patch(idTran, idCauThu, update)).Throws<Exception>();
            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.PatchThamGia(idTran, idCauThu, update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region CreateNewThamGia(Thamgium thamgium)
        [TestMethod]
        public async Task ThamgiaController_CreateNewThamGia_ReturnOK()
        {
            //Arrange
            Thamgium thamgia = new Thamgium()
            {
                Idcauthu = 1,
                IdcauthuNavigation = null,
                Idtran = 1,
                IdtranNavigation = null,
                Sobanthang = 3
            };
            A.CallTo(() => _thamGiaRepos.Create(thamgia)).Returns(await _repo.Create(thamgia));
            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.PostThamgia(thamgia);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task ThamgiaController_CreateNewThamGia_ReturnsProblemResultOnException()
        {
            //Arrange
            Thamgium thamgia = A.Fake<Thamgium>();
            A.CallTo(() => _thamGiaRepos.Create(thamgia)).Throws<Exception>();
            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.PostThamgia(thamgia);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion

        #region DeleteThamgia(int idTran, int idCauThu)
        [TestMethod]
        public async Task ThamgiaController_DeleteThamGia_ReturnNoContent()
        {
            //Arrange
            int idTran = 1;
            int idCauThu = 1;
            await _repo.Delete(idTran, idCauThu);
            A.CallTo(() => _thamGiaRepos.Delete(idTran,idCauThu)).Returns(true);
            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.DeleteThamgium(idTran, idCauThu);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ThamgiaController_DeleteThamGia_ReturnNotFound()
        {
            //Arrange
            int idTran = 1;
            int idCauThu = 1;

            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            ThamGiaRepository _repo;
            var mock = GetFakeThamGiaList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Thamgia).Returns(mock.Object);
            _repo = new ThamGiaRepository(_dbContextMock.Object);
            await _repo.Delete(idTran, idCauThu);
            A.CallTo(() => _thamGiaRepos.Delete(idTran, idCauThu)).Returns(false);

            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.DeleteThamgium(idTran, idCauThu);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ThamgiaController_DeleteThamGia_ReturnsProblemResultOnException()
        {
            //Arrange
            int idTran = 1;
            int idCauThu = 1;
            A.CallTo(() => _thamGiaRepos.Delete(idTran, idCauThu)).Throws<Exception>();
            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.DeleteThamgium(idTran, idCauThu);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion


        #region CheckPlayerJoin(int idTran,int idCauThu)
        [TestMethod]
        public async Task ThamgiaController_CheckPlayerJoin_ReturnOk()
        {
            //Arrange
            int idTran = 1;
            int idCauThu = 1;
            A.CallTo(() => _thamGiaRepos.IsPlayerJoin(idTran, idCauThu)).Returns(await _repo.IsPlayerJoin(idTran,idCauThu));

            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.CheckPlayerJoin(idTran, idCauThu);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
        [TestMethod]
        public async Task ThamgiaController_CheckPlayerJoin_ReturnBadRequest()
        {
            //Arrange
            int idTran = 1;
            int idCauThu = 1;
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            ThamGiaRepository _repo;
            var mock = GetFakeThamGiaList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Thamgia).Returns(mock.Object);
            _repo = new ThamGiaRepository(_dbContextMock.Object);
            await _repo.IsPlayerJoin(idTran, idCauThu);
            A.CallTo(() => _thamGiaRepos.IsPlayerJoin(idTran, idCauThu)).Throws<Exception>();

            var _controller = new ThamgiaController(_thamGiaRepos);
            //Act
            var result = await _controller.CheckPlayerJoin(idTran, idCauThu);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
        #endregion
    }
}
