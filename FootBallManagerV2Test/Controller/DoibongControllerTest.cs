using FakeItEasy;
using FootBallManagerAPI.Controllers;
using FootBallManagerAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootBallManagerAPI.Entities;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class DoibongControllerTest
    {
        public readonly IDoibongRepository _doibongRepo;
        public DoibongControllerTest()
        {
            this._doibongRepo = A.Fake<IDoibongRepository>();
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
            var Doibongs = A.Fake<List<Doibong>>();
            A.CallTo(() => _doibongRepo.GetAllDoibongAsync()).Returns(Doibongs);
            var controller = new DoibongsController(_doibongRepo);
            var result = await controller.GetDoibongs();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _doibongRepo.GetAllDoibongAsync()).Throws<Exception>();
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.GetDoibongs();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var Doibong = A.Fake<Doibong>();
            A.CallTo(() => _doibongRepo.GetDoibongAsync("abc")).Returns(Doibong);

            var controller = new DoibongsController(_doibongRepo);
            var result = await controller.GetDoibong("abc");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var Doibong = A.Fake<Doibong>();
            Doibong = null;
            A.CallTo(() => _doibongRepo.GetDoibongAsync("abc")).Returns(Doibong);

            var controller = new DoibongsController(_doibongRepo);
            var result = await controller.GetDoibong("abc");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _doibongRepo.GetDoibongAsync("abc")).Throws<Exception>();
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.GetDoibong("abc");
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var Doibong = new Doibong();
            Doibong.Id = "abc";
            Doibong.Idquoctich = 2;
            Doibong.Thanhpho = 2;
            Doibong.Hinhanh = new byte[10];
            Doibong.Ten = "Man City";
            Doibong.Soluongthanhvien = 30;
            Doibong.Ngaythanhlap = new DateTime(1999, 1, 1);
            Doibong.Sannha = "Emirates";
            Doibong.Sodochienthuat = "4-3-3";
            Doibong.Giatri = 234300030003;
            Doibong.Cauthus = new List<Cauthu>();
            Doibong.Chuyennhuongs = new List<Chuyennhuong>();
            Doibong.Diems = new List<Diem>();
            Doibong.Doibongsuppliers = new List<Doibongsupplier>();
            Doibong.Doihinhchinhs = new List<Doihinhchinh>();
            Doibong.Huanluyenviens = new List<Huanluyenvien>();
            Doibong.IdquoctichNavigation = new Quoctich();
            Doibong.Items = new List<Item>();
            Doibong.Tapluyens = new List<Tapluyen>();
            Doibong.Teamofleagues = new List<Teamofleague>();
            Doibong.ThanhphoNavigation = new Diadiem();
            Doibong.Thongtingiaidaus = new List<Thongtingiaidau>();
            Doibong.Thongtintrandaus = new List<Thongtintrandau>();
            A.CallTo(() => _doibongRepo.updateDoibongAsync("abc", Doibong)).Invokes(() => {
                Assert.AreEqual(Doibong.Id, "abc");
            });
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.PutDoibong("abc", Doibong);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var Doibong = new Doibong();
            Doibong.Id = "abcd";
            A.CallTo(() => _doibongRepo.updateDoibongAsync("abc", Doibong)).Invokes(() => {
                Assert.AreEqual(Doibong.Id, 1);
            });
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.PutDoibong("abc", Doibong);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var Doibong = new Doibong();
            Doibong.Id = "abc";
            A.CallTo(() => _doibongRepo.updateDoibongAsync("abc", Doibong)).Throws<Exception>();
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.PutDoibong("abc", Doibong);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var Doibong = new Doibong();
            Doibong.Id = "abc";
            A.CallTo(() => _doibongRepo.addDoibongAsync(Doibong)).Returns(Doibong);
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.PostDoibong(Doibong);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var Doibong = new Doibong();
            Doibong = null;
            A.CallTo(() => _doibongRepo.addDoibongAsync(Doibong)).Returns(Doibong);
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.PostDoibong(Doibong);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var Doibong = new Doibong();
            Doibong.Id = "abc";
            A.CallTo(() => _doibongRepo.addDoibongAsync(Doibong)).Throws<Exception>();
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.PostDoibong(Doibong);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            string id = "abc";
            A.CallTo(() => _doibongRepo.deleteDoibongAsync(id)).Invokes(() => {
                Assert.AreEqual(id, "abc");
            }); ;
            var controller = new DoibongsController(_doibongRepo);
            var result = await controller.DeleteDoibong(id);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            string id = "abc";
            A.CallTo(() => _doibongRepo.deleteDoibongAsync(id)).Throws<Exception>();
            var controller = new DoibongsController(_doibongRepo);
            var result = await controller.DeleteDoibong(id);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
