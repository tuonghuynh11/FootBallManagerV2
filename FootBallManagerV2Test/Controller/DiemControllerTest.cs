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
    public class DiemControllerTest
    {
        public readonly IDiemRepository _diemRepo;
        public DiemControllerTest()
        {
            this._diemRepo = A.Fake<IDiemRepository>();
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
            var diems = A.Fake<List<Diem>>();
            A.CallTo(() => _diemRepo.GetAllDiemAsync()).Returns(diems);
            var controller = new DiemsController(_diemRepo);
            var result = await controller.GetDiems();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _diemRepo.GetAllDiemAsync()).Throws<Exception>();
            var controller = new DiemsController(_diemRepo);

            var result = await controller.GetDiems();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var diem = A.Fake<Diem>();
            A.CallTo(() => _diemRepo.GetDiemAsync(1, "abc")).Returns(diem);

            var controller = new DiemsController(_diemRepo);
            var result = await controller.GetDiem(1, "abc");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var diem = A.Fake<Diem>();
            diem = null;
            A.CallTo(() => _diemRepo.GetDiemAsync(1, "abc")).Returns(diem);

            var controller = new DiemsController(_diemRepo);
            var result = await controller.GetDiem(1, "abc");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _diemRepo.GetDiemAsync(1, "abc")).Throws<Exception>();
            var controller = new DiemsController(_diemRepo);

            var result = await controller.GetDiem(1, "abc");
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var diem = new Diem();
            diem.Idgiaidau = 1;
            diem.Iddoibong = "abc";
            diem.Sodiem = 30;
            diem.IddoibongNavigation = new Doibong();
            A.CallTo(() => _diemRepo.updateDiemAsync(1, "abc", diem)).Invokes(() => {
                Assert.AreEqual(diem.Idgiaidau, 1);
                Assert.AreEqual(diem.Iddoibong, "abc");
            });
            var controller = new DiemsController(_diemRepo);

            var result = await controller.PutDiem(1, "abc",diem);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var diem = new Diem();
            diem.Idgiaidau = 2;
            diem.Iddoibong = "abc";
            A.CallTo(() => _diemRepo.updateDiemAsync(1, "abc",diem)).Invokes(() => {
                Assert.AreEqual(diem.Idgiaidau, 1);
                Assert.AreEqual(diem.Iddoibong, "abc");
            });
            var controller = new DiemsController(_diemRepo);

            var result = await controller.PutDiem(1, "abc", diem);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var diem = new Diem();
            diem.Idgiaidau = 1;
            diem.Iddoibong = "abc";
            A.CallTo(() => _diemRepo.updateDiemAsync(1, "abc", diem)).Throws<Exception>();
            var controller = new DiemsController(_diemRepo);

            var result = await controller.PutDiem(1, "abc", diem);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var diem = new Diem();
            diem.Idgiaidau = 2;
            diem.Iddoibong = "abc";
            A.CallTo(() => _diemRepo.addDiemAsync(diem)).Returns(diem);
            var controller = new DiemsController(_diemRepo);

            var result = await controller.PostDiem(diem);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var diem = new Diem();
            diem.Idgiaidau = 2;
            diem.Iddoibong = "abc";
            diem = null;
            A.CallTo(() => _diemRepo.addDiemAsync(diem)).Returns(diem);
            var controller = new DiemsController(_diemRepo);

            var result = await controller.PostDiem(diem);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var diem = new Diem();
            diem.Idgiaidau = 2;
            diem.Iddoibong = "abc";
            A.CallTo(() => _diemRepo.addDiemAsync(diem)).Throws<Exception>();
            var controller = new DiemsController(_diemRepo);

            var result = await controller.PostDiem(diem);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            int id = 1;
            string iddb = "abc";
            A.CallTo(() => _diemRepo.deleteDiemAsync(id, iddb)).Invokes(() => {
                Assert.AreEqual(id, 1);
            }); ;
            var controller = new DiemsController(_diemRepo);
            var result = await controller.DeleteDiem(id, iddb);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            int id = 1;
            string iddb = "abc";
            A.CallTo(() => _diemRepo.deleteDiemAsync(id, iddb)).Throws<Exception>();
            var controller = new DiemsController(_diemRepo);
            var result = await controller.DeleteDiem(id, iddb);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
