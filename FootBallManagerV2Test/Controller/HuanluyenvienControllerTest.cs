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
using Moq;
using MockQueryable.Moq;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class HuanluyenvienControllerTest
    {
        public readonly IHuanluyenvienRepository _hlvRepo;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly HuanluyenvienRepository _repo;
        public HuanluyenvienControllerTest()
        {
            this._hlvRepo = A.Fake<IHuanluyenvienRepository>();
            var mock = GetFakeHLVList().BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Huanluyenviens).Returns(mock.Object);
            this._repo = new HuanluyenvienRepository(this._dbContextMock.Object);
        }
        private static List<Huanluyenvien> GetFakeHLVList()
        {
            return new List<Huanluyenvien>() {
                new Huanluyenvien() {
                    Id = 1,
                    Iddoibong = "abc",
                    Idquoctich = 1,
                    Hoten = "Pep",
                    Tuoi = 50,
                    Gmail = "a@gmail.com",
                    Ngaysinh = new DateTime(1980, 1, 1),
                    Chucvu = "HLV",
                    Hinhanh = new byte[10],
                    IddoibongNavigation = new Doibong(),
                    IdquoctichNavigation = new Quoctich(),
                    Notifications = new List<Notification>(),
                    Tapluyens = new List<Tapluyen>(),

                 },
              new Huanluyenvien() {
                    Id = 2,
                    Iddoibong = "abc",
                    Idquoctich = 1,
                    Hoten = "Zidane",
                    Tuoi = 50,
                    Gmail = "a@gmail.com",
                    Ngaysinh = new DateTime(1980, 1, 1),
                    Chucvu = "HLV",
                    Hinhanh = new byte[10],
                    IddoibongNavigation = new Doibong(),
                    IdquoctichNavigation = new Quoctich(),
                    Notifications = new List<Notification>(),
                    Tapluyens = new List<Tapluyen>(),

                 },


            };
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
            HuanluyenvienRepository res = new HuanluyenvienRepository(A.Fake<FootBallManagerV2Context>());

            var Huanluyenviens = await _repo.GetAllHlvAsync();
            A.CallTo(() => _hlvRepo.GetAllHlvAsync()).Returns(Huanluyenviens);
            var controller = new HuanluyenviensController(_hlvRepo);
            var result = await controller.GetHuanluyenviens();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _hlvRepo.GetAllHlvAsync()).Throws<Exception>();
            var controller = new HuanluyenviensController(_hlvRepo);

            var result = await controller.GetHuanluyenviens();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var Huanluyenvien = A.Fake<Huanluyenvien>();
            A.CallTo(() => _hlvRepo.GetHuanluyenvienAsync(1)).Returns(Huanluyenvien);

            var controller = new HuanluyenviensController(_hlvRepo);
            var result = await controller.GetHuanluyenvien(1);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var Huanluyenvien = A.Fake<Huanluyenvien>();
            Huanluyenvien = null;
            A.CallTo(() => _hlvRepo.GetHuanluyenvienAsync(1)).Returns(Huanluyenvien);

            var controller = new HuanluyenviensController(_hlvRepo);
            var result = await controller.GetHuanluyenvien(1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _hlvRepo.GetHuanluyenvienAsync(1)).Throws<Exception>();
            var controller = new HuanluyenviensController(_hlvRepo);

            var result = await controller.GetHuanluyenvien(1);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var Huanluyenvien = new Huanluyenvien();
            Huanluyenvien.Id = 1;
            Huanluyenvien.Iddoibong = "abc";
            Huanluyenvien.Idquoctich = 1;
            Huanluyenvien.Hoten = "Pep";
            Huanluyenvien.Tuoi = 50;
            Huanluyenvien.Gmail = "a@gmail.com";
            Huanluyenvien.Ngaysinh = new DateTime(1980, 1, 1);
            Huanluyenvien.Chucvu = "HLV";
            Huanluyenvien.Hinhanh = new byte[10];
            Huanluyenvien.IddoibongNavigation = new Doibong();
            Huanluyenvien.IdquoctichNavigation = new Quoctich();
            Huanluyenvien.Notifications = new List<Notification>();
            Huanluyenvien.Tapluyens = new List<Tapluyen>();
            await _repo.updateHlvAsync(1, Huanluyenvien);
            A.CallTo(() => _hlvRepo.updateHlvAsync(1, Huanluyenvien)).Invokes(() => {
                Assert.AreEqual(Huanluyenvien.Id, 1);
            });
            var controller = new HuanluyenviensController(_hlvRepo);

            var result = await controller.PutHuanluyenvien(1, Huanluyenvien);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var Huanluyenvien = new Huanluyenvien();
            Huanluyenvien.Id = 2;
            A.CallTo(() => _hlvRepo.updateHlvAsync(1, Huanluyenvien)).Invokes(() => {
                Assert.AreEqual(Huanluyenvien.Id, 1);
            });
            var controller = new HuanluyenviensController(_hlvRepo);

            var result = await controller.PutHuanluyenvien(1, Huanluyenvien);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var Huanluyenvien = new Huanluyenvien();
            Huanluyenvien.Id = 1;
            A.CallTo(() => _hlvRepo.updateHlvAsync(1, Huanluyenvien)).Throws<Exception>();
            var controller = new HuanluyenviensController(_hlvRepo);

            var result = await controller.PutHuanluyenvien(1, Huanluyenvien);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var newHLV = new Huanluyenvien();
            newHLV.Id = 1;
            var Huanluyenvien = await _repo.addHlvAsync(newHLV);
            A.CallTo(() => _hlvRepo.addHlvAsync(Huanluyenvien)).Returns(Huanluyenvien);
            var controller = new HuanluyenviensController(_hlvRepo);

            var result = await controller.PostHuanluyenvien(Huanluyenvien);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var Huanluyenvien = new Huanluyenvien();
            Huanluyenvien = null;
            A.CallTo(() => _hlvRepo.addHlvAsync(Huanluyenvien)).Returns(Huanluyenvien);
            var controller = new HuanluyenviensController(_hlvRepo);

            var result = await controller.PostHuanluyenvien(Huanluyenvien);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var Huanluyenvien = new Huanluyenvien();
            Huanluyenvien.Id = 1;
            A.CallTo(() => _hlvRepo.addHlvAsync(Huanluyenvien)).Throws<Exception>();
            var controller = new HuanluyenviensController(_hlvRepo);

            var result = await controller.PostHuanluyenvien(Huanluyenvien);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            int id = 1;
            await _repo.deleteHlvAsync(id);
            A.CallTo(() => _hlvRepo.deleteHlvAsync(id)).Invokes(() => {
                Assert.AreEqual(id, 1);
            });
            var controller = new HuanluyenviensController(_hlvRepo);
            var result = await controller.DeleteHuanluyenvien(id);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            int id = 1;
            A.CallTo(() => _hlvRepo.deleteHlvAsync(id)).Throws<Exception>();
            var controller = new HuanluyenviensController(_hlvRepo);
            var result = await controller.DeleteHuanluyenvien(id);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
