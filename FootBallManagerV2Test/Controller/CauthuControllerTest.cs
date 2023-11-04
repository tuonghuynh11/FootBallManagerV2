using FakeItEasy;
using FootBallManagerAPI.Controllers;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class CauthuControllerTest
    {
        private readonly ICauthuRepository _cauthuRepo;
        public CauthuControllerTest()
        {
            this._cauthuRepo = A.Fake<ICauthuRepository>();
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
            var cauthus = A.Fake<List<Cauthu>>();
            A.CallTo(() => _cauthuRepo.GetAllCauthuAsync()).Returns(cauthus);
            var controller = new CauthusController(_cauthuRepo);
            var result = await controller.GetCauthus();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _cauthuRepo.GetAllCauthuAsync()).Throws<Exception>();
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.GetCauthus();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof (BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var cauthu = A.Fake<Cauthu>();
            A.CallTo(() => _cauthuRepo.getCauthuByIdAsync(1)).Returns(cauthu);

            var controller = new CauthusController(_cauthuRepo);
            var result = await controller.GetCauthu(1);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var cauthu = A.Fake<Cauthu>();
            cauthu = null;
            A.CallTo(() => _cauthuRepo.getCauthuByIdAsync(1)).Returns(cauthu);

            var controller = new CauthusController(_cauthuRepo);
            var result = await controller.GetCauthu(1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _cauthuRepo.getCauthuByIdAsync(1)).Throws<Exception>();
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.GetCauthu(1);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var cauthu = new Cauthu();
            cauthu.Id = 1;
            cauthu.Iddoibong = "abc";
            cauthu.Idquoctich = 1;
            cauthu.Hoten = "Kevin";
            cauthu.Tuoi = 30;
            cauthu.Sogiai = 1;
            cauthu.Sobanthang = 20;
            cauthu.Hinhanh = new byte[10];
            cauthu.Chanthuan = "left";
            cauthu.Thetrang = "strong";
            cauthu.Vitri = "CM";
            cauthu.Soao = 11;
            cauthu.Chieucao = "183";
            cauthu.Cannang = "70";
            cauthu.Giatricauthu = 2300000000;
            cauthu.Chuyennhuongs = new List<Chuyennhuong>();
            cauthu.Doihinhchinhs = new List<Doihinhchinh>();
            cauthu.IddoibongNavigation = new Doibong();
            cauthu.IdquoctichNavigation = new Quoctich();
            cauthu.Items = new List<Item>();
            cauthu.Thamgia = new List<Thamgium>();
            A.CallTo(() => _cauthuRepo.updateCauthuAsync(1, cauthu)).Invokes(() => {
                Assert.AreEqual(cauthu.Id, 1);
            });
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.PutCauthu(1, cauthu);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var cauthu = new Cauthu();
            cauthu.Id = 2;
            A.CallTo(() => _cauthuRepo.updateCauthuAsync(1, cauthu)).Invokes(() => {
                Assert.AreEqual(cauthu.Id, 1);
            });
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.PutCauthu(1, cauthu);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var cauthu = new Cauthu(); 
            cauthu.Id = 1;
            A.CallTo(() => _cauthuRepo.updateCauthuAsync(1, cauthu)).Throws<Exception>();
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.PutCauthu(1, cauthu);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var cauthu = new Cauthu();
            cauthu.Id = 1;
            A.CallTo(() => _cauthuRepo.addCauthuAsync(cauthu)).Returns(cauthu);
            var controller = new CauthusController (_cauthuRepo);

            var result = await controller.PostCauthu(cauthu);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var cauthu = new Cauthu();
            cauthu = null;
            A.CallTo(() => _cauthuRepo.addCauthuAsync(cauthu)).Returns(cauthu);
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.PostCauthu(cauthu);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var cauthu = new Cauthu();
            cauthu.Id = 1;
            A.CallTo(() => _cauthuRepo.addCauthuAsync(cauthu)).Throws<Exception>();
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.PostCauthu(cauthu);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            int id = 1;
            A.CallTo(() => _cauthuRepo.deleteCauthuAsync(id)).Invokes(() => {
                Assert.AreEqual(id, 1);
            }); 
            var controller = new CauthusController(_cauthuRepo);
            var result = await controller.DeleteCauthu(id);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            int id = 1;
            A.CallTo(() => _cauthuRepo.deleteCauthuAsync(id)).Throws<Exception>();
            var controller = new CauthusController(_cauthuRepo);
            var result = await controller.DeleteCauthu(id);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
