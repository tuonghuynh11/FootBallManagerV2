using FakeItEasy;
using FootBallManagerAPI.Controllers;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Models;
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
    public class SupplierServiceControllerTest
    {
        private readonly ISupplierServiceRepository _supplierServiceRepos;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly SupplierServiceRepository _repo;
        public SupplierServiceControllerTest()
        {
            this._supplierServiceRepos = A.Fake<ISupplierServiceRepository>();
            var mock = GetFakeSupplierServiceList().BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Supplierservices).Returns(mock.Object);
            this._repo = new SupplierServiceRepository(this._dbContextMock.Object);
        }
        private static List<Supplierservice> GetFakeSupplierServiceList()
        {
            return new List<Supplierservice>() {
                new Supplierservice() {
                      IdService = 1,
                IdSupplier= 1,
                IdServiceNavigation=null,
                IdSupplierNavigation=null

                },
                new Supplierservice() {
                 IdService = 12,
                IdSupplier= 12,
                IdServiceNavigation=null,
                IdSupplierNavigation=null

                }


            };
        }

        #region GetSupplierservices()
        [TestMethod]
        public async Task SupplierServicesController_GetSupplierservices_ReturnOK()
        {
            //Arrange
            SupplierServiceRepository res = new SupplierServiceRepository(A.Fake<FootBallManagerV2Context>());

            var supplierServices = await _repo.GetAll();

            A.CallTo(() => _supplierServiceRepos.GetAll()).Returns(supplierServices);

            var _controller = new SupplierServicesController(_supplierServiceRepos);
            //Act
            var result = await _controller.GetSupplierservices();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task SupplierServicesController_GetSupplierservices_ReturnsProblemResultOnException()
        {
            // Arrange

            A.CallTo(() => _supplierServiceRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new SupplierServicesController(_supplierServiceRepos);

            // Act
            var result = await controller.GetSupplierservices();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region GetSupplierservice(int idService, int idSupplier)
        [TestMethod]
        public async Task SupplierServicesController_GetSupplierserviceById_ReturnOK()
        {
            //Arrange
            int idService = 1;
            int idSupplier = 1;
            var supplierService = await _repo.GetById(idService, idSupplier);
            
            A.CallTo(() => _supplierServiceRepos.GetById(idService,idSupplier)).Returns(supplierService);

            var _controller = new SupplierServicesController(_supplierServiceRepos);
            //Act
            var result = await _controller.GetSupplierservice(idService, idSupplier);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task SupplierServicesController_GetSupplierserviceById_ReturnNotFound()
        {
            //Arrange
            int idService = 1;
            int idSupplier = 1;
            A.CallTo(() => _supplierServiceRepos.GetById(idService, idSupplier)).Returns<Supplierservice>(null);

            var _controller = new SupplierServicesController(_supplierServiceRepos);
            //Act
            var result = await _controller.GetSupplierservice(idService, idSupplier);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task SupplierServicesController_GetSupplierserviceById_ReturnsProblemResultOnException()
        {
            //Arrange
            int idService = 1;
            int idSupplier = 1;
            A.CallTo(() => _supplierServiceRepos.GetById(idService, idSupplier)).Throws<Exception>();

            var _controller = new SupplierServicesController(_supplierServiceRepos);
            //Act
            var result = await _controller.GetSupplierservice(idService, idSupplier);

            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region UpdateSupplierservice(int idService,int idSupplier, Supplierservice supplierservice)
        [TestMethod]
        public async Task SupplierServicesController_UpdateSupplierservice_ReturnNoContent()
        {
            //Arrange
            var supplierService = A.Fake<Supplierservice>();
            supplierService.IdService = 1;
            supplierService.IdSupplier = 1;
            await _repo.Update(supplierService);
            var _controller = new SupplierServicesController(_supplierServiceRepos);
            //Act
            var result = await _controller.UpdateSupplierservice(supplierService.IdService,supplierService.IdSupplier, supplierService);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task SupplierServicesController_UpdateSupplierservice_ReturnBadRequest()
        {
            //Arrange
            var supplierService = A.Fake<Supplierservice>();
            supplierService.IdService = 1;
            supplierService.IdSupplier = 1;
            var _controller = new SupplierServicesController(_supplierServiceRepos);
            //Act
            var result = await _controller.UpdateSupplierservice(2,2, supplierService);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task SupplierServicesController_UpdateSupplierservice_ReturnNotFound()
        {
            //Arrange
            var supplierService = A.Fake<Supplierservice>();
            supplierService.IdService = 1;
            supplierService.IdSupplier = 1;
            A.CallTo(() => _supplierServiceRepos.GetById(1,1)).Throws<Exception>();
            A.CallTo(() => _supplierServiceRepos.Update(supplierService)).Throws<DbUpdateConcurrencyException>();
            var _controller = new SupplierServicesController(_supplierServiceRepos);

            //Act
            var result = await _controller.UpdateSupplierservice(1,1, supplierService);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task SupplierServicesController_UpdateSupplierservice_ReturnsProblemResultOnException()
        {
            //Arrange
            var supplierService = A.Fake<Supplierservice>();
            supplierService.IdService = 1;
            supplierService.IdSupplier = 1;

            A.CallTo(() => _supplierServiceRepos.GetById(1,1)).Returns(supplierService);
            A.CallTo(() => _supplierServiceRepos.Update(supplierService)).Throws<DbUpdateConcurrencyException>();
            var _controller = new SupplierServicesController(_supplierServiceRepos);

            //Act
            var result = await _controller.UpdateSupplierservice(1,1, supplierService);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task SupplierServicesController_UpdateSupplierservice_ReturnsProblemResultOnNullObject()
        {
            //Arrange


            var _controller = new SupplierServicesController(_supplierServiceRepos);

            //Act
            var result = await _controller.UpdateSupplierservice(1,1, null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region CreateNewSupplierService(SupplierServiceModel supplierservice)
        [TestMethod]
        public async Task SupplierServicesController_CreateNewSupplierService_ReturnOK()
        {
            //Arrange
            var supplierServiceModel = A.Fake<SupplierServiceModel>();
            supplierServiceModel.IdService = 1;
            supplierServiceModel.IdSupplier = 1;
            var supplierService = new Supplierservice()
            {
                IdService = 1,
                IdSupplier = 1,
                IdServiceNavigation = null,
                IdSupplierNavigation = null
            };
            A.CallTo(() => _supplierServiceRepos.Create(supplierService)).Returns(await _repo.Create(supplierService));
            var _controller = new SupplierServicesController(_supplierServiceRepos);
            //Act
            var result = await _controller.PostSupplierservice(supplierServiceModel);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task SupplierServicesController_CreateNewSupplierService_ReturnsProblemResultOnException_ByNullObject()
        {
            //Arrange
            var _controller = new SupplierServicesController(_supplierServiceRepos);
            //Act
            var result = await _controller.PostSupplierservice(null);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion

        #region DeleteSupplierservice(int idService,int idSupplier)
        [TestMethod]
        public async Task SupplierServicesController_DeleteSupplierservice_ReturnNoContent()
        {
            //Arrange
           int IdService = 1;
           int IdSupplier = 1;
            await _repo.Delete(IdService, IdSupplier);
            A.CallTo(() => _supplierServiceRepos.Delete(IdService, IdSupplier)).Returns(true);
            var _controller = new SupplierServicesController(_supplierServiceRepos);
            //Act
            var result = await _controller.DeleteSupplierservice(IdService, IdSupplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task SupplierServicesController_DeleteSupplierservice_ReturnNotFound()
        {
            //Arrange
            int IdService = 1;
            int IdSupplier = 1;
            A.CallTo(() => _supplierServiceRepos.Delete(IdService, IdSupplier)).Returns(false);

            var _controller = new SupplierServicesController(_supplierServiceRepos);
            //Act
            var result = await _controller.DeleteSupplierservice(IdService, IdSupplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task SupplierServicesController_DeleteSupplierservice_ReturnsProblemResultOnException()
        {
            //Arrange
            int idThongTinTranDau = 1;
            int IdService = 1;
            int IdSupplier = 1;
            A.CallTo(() => _supplierServiceRepos.Delete(IdService, IdSupplier)).Throws<Exception>();
            var _controller = new SupplierServicesController(_supplierServiceRepos);
            //Act
            var result = await _controller.DeleteSupplierservice(IdService, IdSupplier);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion
    }
}
