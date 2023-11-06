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
using FootBallManagerAPI.Repository;
using Moq;
using MockQueryable.Moq;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class DoihinhchinhControllerTest
    {
        public readonly IDoihinhchinhRepository _doihinhRepo;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly DoihinhchinhRepository _repo;
        public DoihinhchinhControllerTest()
        {
            this._doihinhRepo = A.Fake<IDoihinhchinhRepository>();
            var mock = GetFakeDoiHinhChinhList().BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Doihinhchinhs).Returns(mock.Object);
            this._repo = new DoihinhchinhRepository(this._dbContextMock.Object);
        }

        private static List<Doihinhchinh> GetFakeDoiHinhChinhList()
        {
            return new List<Doihinhchinh>() {
                new Doihinhchinh() {
                   Idcauthu=1,
                   IdcauthuNavigation=null,
                   Iddoibong="atm",
                   IddoibongNavigation=null,
                   Vaitro="Doi Truong",
                   Vitri="ST"

                },
                 new Doihinhchinh() {
                   Idcauthu=2,
                   IdcauthuNavigation=null,
                   Iddoibong="mc",
                   IddoibongNavigation=null,
                   Vaitro="Doi Truong",
                   Vitri="ST"

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
            DoihinhchinhRepository res = new DoihinhchinhRepository(A.Fake<FootBallManagerV2Context>());

            var Doihinhchinhs = await _repo.GetAllDoihinhAsync();
            A.CallTo(() => _doihinhRepo.GetAllDoihinhAsync()).Returns(Doihinhchinhs);
            var controller = new DoihinhchinhsController(_doihinhRepo);
            var result = await controller.GetDoihinhchinhs();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _doihinhRepo.GetAllDoihinhAsync()).Throws<Exception>();
            var controller = new DoihinhchinhsController(_doihinhRepo);

            var result = await controller.GetDoihinhchinhs();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var Doihinhchinh = A.Fake<Doihinhchinh>();
            A.CallTo(() => _doihinhRepo.GetDoihinhAsync("abc", 1)).Returns(Doihinhchinh);

            var controller = new DoihinhchinhsController(_doihinhRepo);
            var result = await controller.GetDoihinhchinh("abc", 1);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var Doihinhchinh = A.Fake<Doihinhchinh>();
            Doihinhchinh = null;
            A.CallTo(() => _doihinhRepo.GetDoihinhAsync("abc",1)).Returns(Doihinhchinh);

            var controller = new DoihinhchinhsController(_doihinhRepo);
            var result = await controller.GetDoihinhchinh("abc", 1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _doihinhRepo.GetDoihinhAsync("abc",1)).Throws<Exception>();
            var controller = new DoihinhchinhsController(_doihinhRepo);

            var result = await controller.GetDoihinhchinh("abc",1);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var Doihinhchinh = new Doihinhchinh();
            Doihinhchinh.Iddoibong = "abc";
            Doihinhchinh.Idcauthu = 1;
            Doihinhchinh.Vitri = "CM";
            Doihinhchinh.Vaitro = "Midfielder";
            Doihinhchinh.IdcauthuNavigation = new Cauthu();
            Doihinhchinh.IddoibongNavigation = new Doibong();
            await _repo.updateDoihinhAsync("abc", 1, Doihinhchinh);
            A.CallTo(() => _doihinhRepo.updateDoihinhAsync("abc",1, Doihinhchinh)).Invokes(() => {
                Assert.AreEqual(Doihinhchinh.Iddoibong, "abc");
                Assert.AreEqual(Doihinhchinh.Idcauthu, 1);
            });
            var controller = new DoihinhchinhsController(_doihinhRepo);

            var result = await controller.PutDoihinhchinh("abc",1, Doihinhchinh);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var Doihinhchinh = new Doihinhchinh();
            Doihinhchinh.Iddoibong = "adbc";
            Doihinhchinh.Idcauthu = 2;
            A.CallTo(() => _doihinhRepo.updateDoihinhAsync("abc", 1, Doihinhchinh)).Invokes(() => {
                Assert.AreEqual(Doihinhchinh.Iddoibong, "abc");
                Assert.AreEqual(Doihinhchinh.Idcauthu, 1);
            });
            var controller = new DoihinhchinhsController(_doihinhRepo);

            var result = await controller.PutDoihinhchinh("abc",1, Doihinhchinh);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var Doihinhchinh = new Doihinhchinh();
            Doihinhchinh.Iddoibong = "abc";
            Doihinhchinh.Idcauthu = 1;
            A.CallTo(() => _doihinhRepo.updateDoihinhAsync("abc",1, Doihinhchinh)).Throws<Exception>();
            var controller = new DoihinhchinhsController(_doihinhRepo);

            var result = await controller.PutDoihinhchinh("abc",1, Doihinhchinh);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var newDoiHinhChinh = new Doihinhchinh();
            newDoiHinhChinh.Iddoibong = "abc";
            newDoiHinhChinh.Idcauthu = 1;
            var Doihinhchinh = await _repo.addDoihinhAsync(newDoiHinhChinh);
            A.CallTo(() => _doihinhRepo.addDoihinhAsync(Doihinhchinh)).Returns(Doihinhchinh);
            var controller = new DoihinhchinhsController(_doihinhRepo);

            var result = await controller.PostDoihinhchinh(Doihinhchinh);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var Doihinhchinh = new Doihinhchinh();
            Doihinhchinh = null;
            A.CallTo(() => _doihinhRepo.addDoihinhAsync(Doihinhchinh)).Returns(Doihinhchinh);
            var controller = new DoihinhchinhsController(_doihinhRepo);

            var result = await controller.PostDoihinhchinh(Doihinhchinh);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var Doihinhchinh = new Doihinhchinh();
            Doihinhchinh.Iddoibong = "abc";
            Doihinhchinh.Idcauthu = 1;
            A.CallTo(() => _doihinhRepo.addDoihinhAsync(Doihinhchinh)).Throws<Exception>();
            var controller = new DoihinhchinhsController(_doihinhRepo);

            var result = await controller.PostDoihinhchinh(Doihinhchinh);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            string Iddoibong = "abc";
            int Idcauthu = 1;
            await _repo.deleteDoihinhAsync(Iddoibong, Idcauthu);
            A.CallTo(() => _doihinhRepo.deleteDoihinhAsync(Iddoibong, Idcauthu)).Invokes(() => {
                Assert.AreEqual(Idcauthu, 1);
                Assert.AreEqual(Iddoibong, "abc");
            }); ;
            var controller = new DoihinhchinhsController(_doihinhRepo);
            var result = await controller.DeleteDoihinhchinh(Iddoibong, Idcauthu);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            string Iddoibong = "abc";
            int Idcauthu = 1;
            A.CallTo(() => _doihinhRepo.deleteDoihinhAsync(Iddoibong, Idcauthu)).Throws<Exception>();
            var controller = new DoihinhchinhsController(_doihinhRepo);
            var result = await controller.DeleteDoihinhchinh(Iddoibong, Idcauthu);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
