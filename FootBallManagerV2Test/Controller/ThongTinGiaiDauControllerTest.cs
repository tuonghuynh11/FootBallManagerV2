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
    public class ThongTinGiaiDauControllerTest
    {
        private readonly IThongtinGiaiDauRepository _thongTinGiaiDauRepos;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly ThongTinGiaiDauRepository _repo;
        public ThongTinGiaiDauControllerTest()
        {
            this._thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>();
            var mock = GetFakeThongTinGiaiDauList(false).BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Thongtingiaidaus).Returns(mock.Object);
            this._repo = new ThongTinGiaiDauRepository(this._dbContextMock.Object);
        }
        private static List<Thongtingiaidau> GetFakeThongTinGiaiDauList(bool isNull)
        {
            if(isNull) return new List<Thongtingiaidau>();
            return new List<Thongtingiaidau>() {
                new Thongtingiaidau() {
                     Ga=1,
                   Gd=1,
                   Draw=1,
                   Iddoibong="atm",
                   IddoibongNavigation=null,
                   Idgiaidau=1,
                   IdgiaidauNavigation=null,
                   Lose=1,
                   Points=1,
                   Win=1

                },
                new Thongtingiaidau() {
                    Ga=1,
                   Gd=1,
                   Draw=1,
                   Iddoibong="mc",
                   IddoibongNavigation=null,
                   Idgiaidau=1,
                   IdgiaidauNavigation=null,
                   Lose=1,
                   Points=1,
                   Win=1

                }


            };
        }

        #region GetThongtingiaidaus()
        [TestMethod]
        public async Task ThongTinGiaiDausController_GetThongtingiaidaus_ReturnOK()
        {
            //Arrange
            var _thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>(options => options.Wrapping(new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>())));

            ThongTinGiaiDauRepository res = new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>());

            var thongtingiaidaus =await _repo.GetAll();

            A.CallTo(() => _thongTinGiaiDauRepos.GetAll()).Returns(thongtingiaidaus);

            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.GetThongtingiaidaus();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task ThongTinGiaiDausController_GetThongtingiaidaus_ReturnsProblemResultOnException()
        {
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            ThongTinGiaiDauRepository _repo;
            var mock = GetFakeThongTinGiaiDauList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Thongtingiaidaus).Returns(mock.Object);
            _repo = new ThongTinGiaiDauRepository(_dbContextMock.Object);
            await _repo.GetAll();
            // Arrange
            var _thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>(options => options.Wrapping(new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>())));

            A.CallTo(() => _thongTinGiaiDauRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);

            // Act
            var result = await controller.GetThongtingiaidaus();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region GetThongtingiaidau(int id)
        [TestMethod]
        public async Task ThongTinGiaiDausController_GetThongtingiaidauById_ReturnOK()
        {
            //Arrange
            var _thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>(options => options.Wrapping(new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>())));

            List<Thongtingiaidau> list = (List<Thongtingiaidau>)await _repo.GetById(1);


            A.CallTo(() => _thongTinGiaiDauRepos.GetById(1)).Returns(list);

            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.GetThongtingiaidau(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task ThongTinGiaiDausController_GetThongtingiaidauById_ReturnNotFound()
        {
            //Arrange
            var _thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>(options => options.Wrapping(new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>())));

            A.CallTo(() => _thongTinGiaiDauRepos.GetById(1)).Returns<IEnumerable<Thongtingiaidau>>(null);

            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.GetThongtingiaidau(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task ThongTinGiaiDausController_GetThongtingiaidauById_ReturnsProblemResultOnException()
        {
            //Arrange
            var _thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>(options => options.Wrapping(new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>())));

            A.CallTo(() => _thongTinGiaiDauRepos.GetById(1)).Throws<Exception>();

            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.GetThongtingiaidau(1);

            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region UpdateThongtingiaidau(int id, Thongtingiaidau thongtingiaidau)
        [TestMethod]
        public async Task ThongTinGiaiDausController_UpdateThongtingiaidau_ReturnNoContent()
        {
            //Arrange
            Thongtingiaidau thongtingiaidau = A.Fake<Thongtingiaidau>();
            thongtingiaidau.Iddoibong ="atm";
            thongtingiaidau.Idgiaidau = 2;
            await _repo.Update(thongtingiaidau);
            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.UpdateThongtingiaidau(thongtingiaidau.Idgiaidau,thongtingiaidau.Iddoibong, thongtingiaidau);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ThongTinGiaiDausController_UpdateThongtingiaidau_ReturnBadRequest()
        {
            //Arrange
            Thongtingiaidau thongtingiaidau = A.Fake<Thongtingiaidau>();
            thongtingiaidau.Iddoibong = "atm";

            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.UpdateThongtingiaidau(2,"mc", thongtingiaidau);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task ThongTinGiaiDausController_UpdateThongtingiaidau_ReturnNotFound()
        {
            //Arrange
            Thongtingiaidau thongtingiaidau = A.Fake<Thongtingiaidau>();
            thongtingiaidau.Iddoibong = "atm";
            thongtingiaidau.Idgiaidau = 0;
            A.CallTo(() => _thongTinGiaiDauRepos.GetById(0)).Throws<Exception>();
            A.CallTo(() => _thongTinGiaiDauRepos.Update(thongtingiaidau)).Throws<DbUpdateConcurrencyException>();
            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);

            //Act
            var result = await _controller.UpdateThongtingiaidau(thongtingiaidau.Idgiaidau, thongtingiaidau.Iddoibong, thongtingiaidau);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ThongTinGiaiDausController_UpdateThongtingiaidau_ReturnsProblemResultOnException()
        {
            //Arrange
            IEnumerable<Thongtingiaidau> thongtingiaidau = A.Fake<IEnumerable<Thongtingiaidau>>();
            Thongtingiaidau update = A.Fake<Thongtingiaidau>();
            update.Iddoibong = "atm";
            update.Idgiaidau = 0;

            List <Thongtingiaidau> temp = new List<Thongtingiaidau> { update };
            A.CallTo(() => _thongTinGiaiDauRepos.GetById(0)).Returns(temp);
            A.CallTo(() => _thongTinGiaiDauRepos.Update(update)).Throws<DbUpdateConcurrencyException>();
            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);

            //Act
            var result = await _controller.UpdateThongtingiaidau(0,"atm", update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task ThongTinGiaiDausController_UpdateThongtingiaidau_ReturnsProblemResultOnNullObject()
        {
            //Arrange
       

            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);

            //Act
            var result = await _controller.UpdateThongtingiaidau(2,"atm", null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region PatchThongtingiaidau(int id, JsonPatchDocument thongTinGiaiDauModel)
        [TestMethod]
        public async Task ThongTinGiaiDausController_PatchThongTinGiaiDau_ReturnNoContent()
        {
            //Arrange
            var _thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>(options => options.Wrapping(new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>())));

            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("Win", 13);
            A.CallTo(() => _thongTinGiaiDauRepos.Patch(5,"atm", update)).Returns(true);
            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.PatchThongtingiaidau(5, "atm", update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ThongTinGiaiDausController_PatchThongTinGiaiDau_ReturnBadRequest()
        {
            //Arrange
            var _thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>(options => options.Wrapping(new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>())));

            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            ThongTinGiaiDauRepository _repo;
            var mock = GetFakeThongTinGiaiDauList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Thongtingiaidaus).Returns(mock.Object);
            _repo = new ThongTinGiaiDauRepository(_dbContextMock.Object);
            await _repo.Patch(5, "atm", update);
            update.Replace("IDDOIBONG", "atm");
           
            A.CallTo(() => _thongTinGiaiDauRepos.Patch(5, "atm", update)).Returns(false);
            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.PatchThongtingiaidau(5, "atm", update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task ThongTinGiaiDausController_PatchThongTinGiaiDau_ReturnsProblemResultOnException()
        {
            //Arrange
            var _thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>(options => options.Wrapping(new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>())));

            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("IDDOIBONG", "atm");

            A.CallTo(() => _thongTinGiaiDauRepos.Patch(5, "atm", update)).Throws<Exception>();
            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.PatchThongtingiaidau(5, "atm", update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region CreateNewThongtingiaidau(Thongtingiaidau thongtingiaidau)
        [TestMethod]
        public async Task ThongTinGiaiDausController_CreateNewThongtingiaidau_ReturnOK()
        {
            //Arrange
            var _thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>(options => options.Wrapping(new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>())));

            Thongtingiaidau thongtingiaidau = new Thongtingiaidau()
            {
                Ga = 1,
                Gd = 1,
                Draw = 1,
                Iddoibong = "atm",
                IddoibongNavigation = null,
                Idgiaidau = 1,
                IdgiaidauNavigation = null,
                Lose = 1,
                Points = 1,
                Win = 1
            };
            A.CallTo(() => _thongTinGiaiDauRepos.Create(thongtingiaidau)).Returns(await _repo.Create(thongtingiaidau));
            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.PostThongtingiaidau(thongtingiaidau);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task ThongTinGiaiDausController_CreateNewThongtingiaidau_ReturnsProblemResultOnException()
        {
            //Arrange
            var _thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>(options => options.Wrapping(new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>())));

            Thongtingiaidau thongtingiaidau = A.Fake<Thongtingiaidau>();
            A.CallTo(() => _thongTinGiaiDauRepos.Create(thongtingiaidau)).Throws<Exception>();
            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.PostThongtingiaidau(thongtingiaidau);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion



        #region DeleteThongtingiaidau(int idGiaiDau,string idDoiBong)
        [TestMethod]
        public async Task ThongTinGiaiDausController_DeleteThongtingiaidau_ReturnNoContent()
        {
            //Arrange
            var _thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>(options => options.Wrapping(new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>())));

            int idGiaiDau = 1;
            string idDoiBong= "atm";
            await _repo.Delete(idGiaiDau, idDoiBong);
            A.CallTo(() => _thongTinGiaiDauRepos.Delete(idGiaiDau,idDoiBong)).Returns(true);
            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.DeleteThongtingiaidau(idGiaiDau, idDoiBong);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ThongTinGiaiDausController_DeleteThongtingiaidau_ReturnNotFound()
        {
            //Arrange
            var _thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>(options => options.Wrapping(new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>())));

            int idGiaiDau = 1;
            string idDoiBong = "atm";

            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            ThongTinGiaiDauRepository _repo;
            var mock = GetFakeThongTinGiaiDauList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Thongtingiaidaus).Returns(mock.Object);
            _repo = new ThongTinGiaiDauRepository(_dbContextMock.Object);
            await _repo.Delete(5, "atm");
            A.CallTo(() => _thongTinGiaiDauRepos.Delete(idGiaiDau, idDoiBong)).Returns(false);

            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.DeleteThongtingiaidau(idGiaiDau, idDoiBong);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_DeleteThongtintrandau_ReturnsProblemResultOnException()
        {
            //Arrange
            var _thongTinGiaiDauRepos = A.Fake<IThongtinGiaiDauRepository>(options => options.Wrapping(new ThongTinGiaiDauRepository(A.Fake<FootBallManagerV2Context>())));

            int idGiaiDau = 1;
            string idDoiBong = "atm";
            A.CallTo(() => _thongTinGiaiDauRepos.Delete(idGiaiDau, idDoiBong)).Throws<Exception>();

            var _controller = new ThongtingiaidausController(_thongTinGiaiDauRepos);
            //Act
            var result = await _controller.DeleteThongtingiaidau(idGiaiDau, idDoiBong);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion
    }
}
