using FakeItEasy;
using FootBallManagerAPI.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootBallManagerAPI.Controllers;
using Microsoft.AspNetCore.Http;
using FootBallManagerAPI.Entities;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class ChuyennhuongControllerTest
    {
        private readonly IChuyennhuongRepository _chuyennhuongRepo;
        public ChuyennhuongControllerTest()
        {
            this._chuyennhuongRepo = A.Fake<IChuyennhuongRepository>();
        }
        private static int? GetStatusCode<T>(ActionResult<T?> actionResult)
        {
            IConvertToActionResult convertToActionResult = actionResult; // ActionResult implicit implements IConvertToActionResult
            var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;
            return actionResultWithStatusCode?.StatusCode;
        }

        [TestMethod]
        public async Task GetAllMethodOk()
        {
            var chuyennhuongs = A.Fake<List<Chuyennhuong>>();
            A.CallTo(() => _chuyennhuongRepo.GetAllChuyennhuongAsync()).Returns(chuyennhuongs);
            var controller = new ChuyennhuongsController(_chuyennhuongRepo);
            var result = await controller.GetChuyennhuongs();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _chuyennhuongRepo.GetAllChuyennhuongAsync()).Throws<Exception>();
            var controller = new ChuyennhuongsController(_chuyennhuongRepo);

            var result = await controller.GetChuyennhuongs();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var chuyennhuong = A.Fake<Chuyennhuong>();
            A.CallTo(() => _chuyennhuongRepo.GetChuyennhuongAsync(1)).Returns(chuyennhuong);

            var controller = new ChuyennhuongsController(_chuyennhuongRepo);
            var result = await controller.GetChuyennhuong(1);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var chuyennhuong = A.Fake<Chuyennhuong>();
            chuyennhuong = null;
            A.CallTo(() => _chuyennhuongRepo.GetChuyennhuongAsync(1)).Returns(chuyennhuong);

            var controller = new ChuyennhuongsController(_chuyennhuongRepo);
            var result = await controller.GetChuyennhuong(1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _chuyennhuongRepo.GetChuyennhuongAsync(1)).Throws<Exception>();
            var controller = new ChuyennhuongsController(_chuyennhuongRepo);

            var result = await controller.GetChuyennhuong(1);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var chuyennhuong = new Chuyennhuong();
            chuyennhuong.Id = 1;
            chuyennhuong.Idcauthu = 1;
            chuyennhuong.Iddoimua = "qwe";
            chuyennhuong.IdcauthuNavigation = new Cauthu();
            chuyennhuong.IddoimuaNavigation = new Doibong();
            A.CallTo(() => _chuyennhuongRepo.updateChuyennhuongAsync(1, chuyennhuong)).Invokes(() => {
                Assert.AreEqual(chuyennhuong.Id, 1);
            });
            var controller = new ChuyennhuongsController(_chuyennhuongRepo);

            var result = await controller.PutChuyennhuong(1, chuyennhuong);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var chuyennhuong = new Chuyennhuong();
            chuyennhuong.Id = 2;
            A.CallTo(() => _chuyennhuongRepo.updateChuyennhuongAsync(1, chuyennhuong)).Invokes(() => {
                Assert.AreEqual(chuyennhuong.Id, 1);
            });
            var controller = new ChuyennhuongsController(_chuyennhuongRepo);

            var result = await controller.PutChuyennhuong(1, chuyennhuong);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var chuyennhuong = new Chuyennhuong();
            chuyennhuong.Id = 1;
            A.CallTo(() => _chuyennhuongRepo.updateChuyennhuongAsync(1, chuyennhuong)).Throws<Exception>();
            var controller = new ChuyennhuongsController(_chuyennhuongRepo);

            var result = await controller.PutChuyennhuong(1, chuyennhuong);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var chuyennhuong = new Chuyennhuong();
            chuyennhuong.Id = 1;
            A.CallTo(() => _chuyennhuongRepo.addChuyennhuongAsync(chuyennhuong)).Returns(chuyennhuong);
            var controller = new ChuyennhuongsController(_chuyennhuongRepo);

            var result = await controller.PostChuyennhuong(chuyennhuong);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var chuyennhuong = new Chuyennhuong();
            chuyennhuong = null;
            A.CallTo(() => _chuyennhuongRepo.addChuyennhuongAsync(chuyennhuong)).Returns(chuyennhuong);
            var controller = new ChuyennhuongsController(_chuyennhuongRepo);

            var result = await controller.PostChuyennhuong(chuyennhuong);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var chuyennhuong = new Chuyennhuong();
            chuyennhuong.Id = 1;
            A.CallTo(() => _chuyennhuongRepo.addChuyennhuongAsync(chuyennhuong)).Throws<Exception>();
            var controller = new ChuyennhuongsController(_chuyennhuongRepo);

            var result = await controller.PostChuyennhuong(chuyennhuong);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            int id = 1;
            A.CallTo(() => _chuyennhuongRepo.deleteChuyennhuongAsync(1)).Invokes(() => {
                Assert.AreEqual(id, 1);
            }); ;
            var controller = new ChuyennhuongsController(_chuyennhuongRepo);
            var result = await controller.DeleteChuyennhuong(id);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            int id = 1;
            A.CallTo(() => _chuyennhuongRepo.deleteChuyennhuongAsync(1)).Throws<Exception>();
            var controller = new ChuyennhuongsController(_chuyennhuongRepo);
            var result = await controller.DeleteChuyennhuong(id);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
