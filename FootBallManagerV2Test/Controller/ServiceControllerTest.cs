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

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class ServiceControllerTest
    {
        private readonly IServiceRepository _serviceRepos;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly ServiceRepository _repo;
        public ServiceControllerTest()
        {
            this._serviceRepos = A.Fake<IServiceRepository>();
            var mock = GetFakeCauThuList().BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Services).Returns(mock.Object);
            this._repo = new ServiceRepository(this._dbContextMock.Object);
        }
        private static List<Service> GetFakeCauThuList()
        {
            return new List<Service>() {
                new Service() {
                      IdService=1,
                     Detail="",
                Fieldservices=null,
                Images=null,
                Supplierservices = null,
                ServiceName="Shirt"

                },
                new Service() {
                    IdService=2,
                     Detail="",
                Fieldservices=null,
                Images=null,
                Supplierservices = null,
                ServiceName="Food"

                }


            };
        }

        #region GetServices()
        [TestMethod]
        public async Task ServiceController_GetServices_ReturnOK()
        {
            //Arrange
            ServiceRepository res = new ServiceRepository(A.Fake<FootBallManagerV2Context>());

            var services =await _repo.GetAll();

            A.CallTo(() => _serviceRepos.GetAll()).Returns(services);

            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.GetServices();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task ServiceController_GetServices_ReturnsProblemResultOnException()
        {
            // Arrange

            A.CallTo(() => _serviceRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new ServicesController(_serviceRepos);

            // Act
            var result = await controller.GetServices();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region GetService(int id)
        [TestMethod]
        public async Task ServiceController_GetService_ReturnOK()
        {
            //Arrange
            var service = await _repo.GetById(1);

            A.CallTo(() => _serviceRepos.GetById(1)).Returns(service);

            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.GetService(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task ServiceController_GetService_ReturnNotFound()
        {
            //Arrange

            A.CallTo(() => _serviceRepos.GetById(1)).Returns<Service>(null);

            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.GetService(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task ServiceController_GetService_ReturnsProblemResultOnException()
        {
            //Arrange

            A.CallTo(() => _serviceRepos.GetById(1)).Throws<Exception>();

            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.GetService(1);

            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region UpdateService(int id, Service service)
        [TestMethod]
        public async Task ServiceController_UpdateService_ReturnNoContent()
        {
            //Arrange
            var service = A.Fake<Service>();
            service.IdService = 1;
            service.ServiceName = "Shirt";
            await _repo.Update(service);
            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.UpdateService(service.IdService, service);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ServiceController_UpdateService_ReturnBadRequest()
        {
            //Arrange
            var service = A.Fake<Service>();
            service.IdService = 1;
            service.ServiceName = "Shirt";
            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.UpdateService(2, service);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task ServiceController_UpdateService_ReturnNotFound()
        {
            //Arrange
            var service = A.Fake<Service>();
            service.IdService = 2;
            service.ServiceName = "Shirt";
            A.CallTo(() => _serviceRepos.GetById(2)).Throws<Exception>();
            A.CallTo(() => _serviceRepos.Update(service)).Throws<DbUpdateConcurrencyException>();
            var _controller = new ServicesController(_serviceRepos);

            //Act
            var result = await _controller.UpdateService(2, service);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ServiceController_UpdateService_ReturnsProblemResultOnException()
        {
            //Arrange
            var service = A.Fake<Service>();
            service.IdService = 2;
            service.ServiceName = "Shirt";

            A.CallTo(() => _serviceRepos.GetById(2)).Returns(service);
            A.CallTo(() => _serviceRepos.Update(service)).Throws<DbUpdateConcurrencyException>();
            var _controller = new ServicesController(_serviceRepos);

            //Act
            var result = await _controller.UpdateService(2, service);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task ServiceController_UpdateService_ReturnsProblemResultOnNullObject()
        {
            //Arrange


            var _controller = new ServicesController(_serviceRepos);

            //Act
            var result = await _controller.UpdateService(2, null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region PatchService(int id, JsonPatchDocument serviceModel)
        [TestMethod]
        public async Task ServiceController_PatchService_ReturnNoContent()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("serviceName", "Shoes");
            await _repo.Patch(5, update);
            A.CallTo(() => _serviceRepos.Patch(5, update)).Returns(true);
            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.PatchService(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ServiceController_PatchService_ReturnBadRequest()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("serviceName", "Shoes");
            A.CallTo(() => _serviceRepos.Patch(5, update)).Returns(false);
            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.PatchService(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task ServiceController_PatchService_ReturnsProblemResultOnException()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("serviceName", "Shoes");
            A.CallTo(() => _serviceRepos.Patch(5, update)).Throws<Exception>();
            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.PatchService(5, update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region CreateNewService(Service service)
        [TestMethod]
        public async Task ServiceController_CreateNewService_ReturnOK()
        {
            //Arrange
            var service = new Service()
            {
                IdService = 1,
                Detail = "",
                Fieldservices = null,
                Images = null,
                Supplierservices = null,
                ServiceName = ""
            };
            A.CallTo(() => _serviceRepos.Create(service)).Returns(await _repo.Create(service));
            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.PostService(service);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task ServiceController_CreateNewService_ReturnsProblemResultOnException()
        {
            //Arrange
            var service = A.Fake<Service>();
            A.CallTo(() => _serviceRepos.Create(service)).Throws<Exception>();
            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.PostService(service);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion

        #region  DeleteService(int id)
        [TestMethod]
        public async Task ServiceController_DeleteService_ReturnNoContent()
        {
            //Arrange
            int idService = 1;
            await _repo.Delete(idService);
            A.CallTo(() => _serviceRepos.Delete(idService)).Returns(true);
            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.DeleteService(idService);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ServiceController_DeleteService_ReturnNotFound()
        {
            //Arrange
            int idService = 1;
            A.CallTo(() => _serviceRepos.Delete(idService)).Returns(false);

            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.DeleteService(idService);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ServiceController_DeleteService_ReturnsProblemResultOnException()
        {
            //Arrange
            int idService = 1;
            A.CallTo(() => _serviceRepos.Delete(idService)).Throws<Exception>();
            var _controller = new ServicesController(_serviceRepos);
            //Act
            var result = await _controller.DeleteService(idService);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion


    }
}
