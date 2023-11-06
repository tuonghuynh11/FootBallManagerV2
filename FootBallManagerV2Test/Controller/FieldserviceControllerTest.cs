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
    public class FieldserviceControllerTest
    {
        public readonly IFieldServiceRepository _fieldserviceRepo;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly FieldserviceRepository _repo;
        public FieldserviceControllerTest()
        {
            this._fieldserviceRepo = A.Fake<IFieldServiceRepository>();
            var mock = GetFakeFieldSerivceList().BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Fieldservices).Returns(mock.Object);
            this._repo = new FieldserviceRepository(this._dbContextMock.Object);
        }
        private static List<Fieldservice> GetFakeFieldSerivceList()
        {
            return new List<Fieldservice>() {
                new Fieldservice() {
                     IdField = 1,
                     IdService = 1,
                     Status = 1,
                     IdFieldNavigation = new Field(),
                     IdServiceNavigation = new Service(),

                 },
                new Fieldservice() {
                     IdField = 2,
                     IdService = 2,
                     Status = 1,
                     IdFieldNavigation = new Field(),
                     IdServiceNavigation = new Service(),

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
            FieldserviceRepository res = new FieldserviceRepository(A.Fake<FootBallManagerV2Context>());

            var Fieldservices = await _repo.GetAllFieldservicesAsync();
            A.CallTo(() => _fieldserviceRepo.GetAllFieldservicesAsync()).Returns(Fieldservices);
            var controller = new FieldservicesController(_fieldserviceRepo);
            var result = await controller.GetFieldservices();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _fieldserviceRepo.GetAllFieldservicesAsync()).Throws<Exception>();
            var controller = new FieldservicesController(_fieldserviceRepo);

            var result = await controller.GetFieldservices();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var Fieldservice = A.Fake<Fieldservice>();
            A.CallTo(() => _fieldserviceRepo.GetFieldserviceAsync(1, 1)).Returns(Fieldservice);

            var controller = new FieldservicesController(_fieldserviceRepo);
            var result = await controller.GetFieldservice(1, 1);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var Fieldservice = A.Fake<Fieldservice>();
            Fieldservice = null;
            A.CallTo(() => _fieldserviceRepo.GetFieldserviceAsync(1, 1)).Returns(Fieldservice);

            var controller = new FieldservicesController(_fieldserviceRepo);
            var result = await controller.GetFieldservice(1, 1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _fieldserviceRepo.GetFieldserviceAsync(1, 1)).Throws<Exception>();
            var controller = new FieldservicesController(_fieldserviceRepo);

            var result = await controller.GetFieldservice(1, 1);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var Fieldservice = new Fieldservice();
            Fieldservice.IdField = 1;
            Fieldservice.IdService = 1;
            Fieldservice.Status = 1;
            Fieldservice.IdFieldNavigation = new Field();
            Fieldservice.IdServiceNavigation = new Service();
            await _repo.updateFieldServiceAsync(1, 1, Fieldservice);
            A.CallTo(() => _fieldserviceRepo.updateFieldServiceAsync(1, 1, Fieldservice)).Invokes(() => {
                Assert.AreEqual(Fieldservice.IdField, 1);
                Assert.AreEqual(Fieldservice.IdService, 1);
            });
            var controller = new FieldservicesController(_fieldserviceRepo);

            var result = await controller.PutFieldservice(1,1, Fieldservice);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var Fieldservice = new Fieldservice();
            Fieldservice.IdField = 2;
            Fieldservice.IdService = 1;
            A.CallTo(() => _fieldserviceRepo.updateFieldServiceAsync(1,1, Fieldservice)).Invokes(() => {
                Assert.AreEqual(Fieldservice.IdField, 1);
                Assert.AreEqual(Fieldservice.IdService, 1);
            });
            var controller = new FieldservicesController(_fieldserviceRepo);

            var result = await controller.PutFieldservice(1,1, Fieldservice);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var Fieldservice = new Fieldservice();
            Fieldservice.IdField = 1;
            Fieldservice.IdService = 1;
            A.CallTo(() => _fieldserviceRepo.updateFieldServiceAsync(1,1, Fieldservice)).Throws<Exception>();
            var controller = new FieldservicesController(_fieldserviceRepo);

            var result = await controller.PutFieldservice(1,1, Fieldservice);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var newFieldSerivce = new Fieldservice();
            newFieldSerivce.IdField = 1;
            newFieldSerivce.IdService = 1;
            var Fieldservice = await _repo.addFieldserviceAsync(newFieldSerivce);
            A.CallTo(() => _fieldserviceRepo.addFieldserviceAsync(Fieldservice)).Returns(Fieldservice);
            var controller = new FieldservicesController(_fieldserviceRepo);

            var result = await controller.PostFieldservice(Fieldservice);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var Fieldservice = new Fieldservice();
            Fieldservice = null;
            A.CallTo(() => _fieldserviceRepo.addFieldserviceAsync(Fieldservice)).Returns(Fieldservice);
            var controller = new FieldservicesController(_fieldserviceRepo);

            var result = await controller.PostFieldservice(Fieldservice);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var Fieldservice = new Fieldservice();
            Fieldservice.IdField = 1;
            Fieldservice.IdService = 1;
            A.CallTo(() => _fieldserviceRepo.addFieldserviceAsync(Fieldservice)).Throws<Exception>();
            var controller = new FieldservicesController(_fieldserviceRepo);

            var result = await controller.PostFieldservice(Fieldservice);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            int idField = 1;
            int idService = 1;
            await _repo.deleteFieldserviceAsync(idField, idService);
            A.CallTo(() => _fieldserviceRepo.deleteFieldserviceAsync(idField, idService)).Invokes(() => {
                Assert.AreEqual(idField, 1);
                Assert.AreEqual(idService, 1);
            }); 
            var controller = new FieldservicesController(_fieldserviceRepo);
            var result = await controller.DeleteFieldservice(idField, idService);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            int idField = 1;
            int idService = 1;
            A.CallTo(() => _fieldserviceRepo.deleteFieldserviceAsync(idField, idService)).Throws<Exception>();
            var controller = new FieldservicesController(_fieldserviceRepo);
            var result = await controller.DeleteFieldservice(idField, idService);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
