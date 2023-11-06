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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class SupplierControllerTest
    {
        private readonly ISupplierRepository _supplierRepos;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly SupplierRepository _repo;

        public SupplierControllerTest()
        {
            this._supplierRepos = A.Fake<ISupplierRepository>();
            var mock = GetFakeSupplierList(false).BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Suppliers).Returns(mock.Object);
            this._repo = new SupplierRepository(this._dbContextMock.Object);

        }
        private static List<Supplier> GetFakeSupplierList(bool isNull)
        {
            if(isNull) return new List<Supplier>();
            return new List<Supplier>() {
                new Supplier() {
                    IdSupplier = 1,
                Addresss = "",
                Doibongsuppliers = null,
                EstablishDate = DateTime.Now,
                Images = null,
                Leaguesuppliers = null,
                PhoneNumber = "",
                RepresentativeName = "",
                SupplierName = "ABC",
                Supplierservices = null

                },
                new Supplier() {
                   IdSupplier = 2,
                Addresss = "",
                Doibongsuppliers = null,
                EstablishDate = DateTime.Now,
                Images = null,
                Leaguesuppliers = null,
                PhoneNumber = "",
                RepresentativeName = "",
                SupplierName = "CDF",
                Supplierservices = null

                }


            };
        }


        #region GetSuppliers()
        [TestMethod]
        public async Task SupplierController_GetSuppliers_ReturnOK()
        {

            //Arrange
            //SupplierRepository res = new SupplierRepository(A.Fake<FootBallManagerV2Context>());
            var suppliers =await _repo.GetAll();

            A.CallTo(() => _supplierRepos.GetAll()).Returns(suppliers);

            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.GetSuppliers();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task SupplierController_GetSuppliers_ReturnsProblemResultOnException()
        {

            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            SupplierRepository _repo;
            var mock = GetFakeSupplierList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Suppliers).Returns(mock.Object);
            _repo = new SupplierRepository(_dbContextMock.Object);
            await _repo.GetAll();
            // Arrange

            A.CallTo(() => _supplierRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new SuppliersController(_supplierRepos);

            // Act
            var result = await controller.GetSuppliers();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region GetSupplier(int id)
        [TestMethod]
        public async Task SupplierController_GetSupplierById_ReturnOK()
        {
            //Arrange
            var supplier = await _repo.GetById(1);
            A.CallTo(() => _supplierRepos.GetById(1)).Returns(supplier);

            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.GetSupplier(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task SupplierController_GetSupplierById_ReturnNotFound()
        {
            //Arrange

            A.CallTo(() => _supplierRepos.GetById(1)).Returns<Supplier>(null);

            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.GetSupplier(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task SupplierController_GetSupplierById_ReturnsProblemResultOnException()
        {
            //Arrange

            A.CallTo(() => _supplierRepos.GetById(1)).Throws<Exception>();

            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.GetSupplier(1);

            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region UpdateSupplier(int id, Supplier supplier)
        [TestMethod]
        public async Task SupplierController_UpdateSupplier_ReturnNoContent()
        {
            //Arrange
            var supplier = A.Fake<Supplier>();
            supplier.IdSupplier = 2;
            supplier.SupplierName = "Adidas";
            await _repo.Update(supplier);
            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.UpdateSuppliers(supplier.IdSupplier, supplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task SupplierController_UpdateSupplier_ReturnBadRequest()
        {
            //Arrange
            var supplier = A.Fake<Supplier>();
            supplier.IdSupplier = 2;
            supplier.SupplierName = "Adidas";
            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.UpdateSuppliers(1, supplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task SupplierController_UpdateSupplier_ReturnNotFound()
        {
            //Arrange
            var supplier = A.Fake<Supplier>();
            supplier.IdSupplier = 2;
            supplier.SupplierName = "Adidas";
            A.CallTo(() => _supplierRepos.GetById(2)).Throws<Exception>();
            A.CallTo(() => _supplierRepos.Update(supplier)).Throws<DbUpdateConcurrencyException>();
            var _controller = new SuppliersController(_supplierRepos);

            //Act
            var result = await _controller.UpdateSuppliers(2, supplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task SupplierController_UpdateSupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            var supplier = A.Fake<Supplier>();
            supplier.IdSupplier = 2;
            supplier.SupplierName = "Adidas";

            A.CallTo(() => _supplierRepos.GetById(2)).Returns(supplier);
            A.CallTo(() => _supplierRepos.Update(supplier)).Throws<DbUpdateConcurrencyException>();
            var _controller = new SuppliersController(_supplierRepos);

            //Act
            var result = await _controller.UpdateSuppliers(2, supplier);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task SupplierController_UpdateSupplier_ReturnsProblemResultOnNullObject()
        {
            //Arrange


            var _controller = new SuppliersController(_supplierRepos);

            //Act
            var result = await _controller.UpdateSuppliers(2, null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region PatchSupplier(int id, JsonPatchDocument supplierModel)
        [TestMethod]
        public async Task SupplierController_PatchSupplier_ReturnNoContent()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("supplierName", "Adidas");
            await _repo.Patch(5, update);
            A.CallTo(() => _supplierRepos.Patch(5, update)).Returns(true);
            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.PatchSupplier(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task SupplierController_PatchSupplier_ReturnBadRequest()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("supplierName", "Adidas");
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            SupplierRepository _repo;
            var mock = GetFakeSupplierList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Suppliers).Returns(mock.Object);
            _repo = new SupplierRepository(_dbContextMock.Object);
            await _repo.Patch(5,update);
            A.CallTo(() => _supplierRepos.Patch(5, update)).Returns(false);
            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.PatchSupplier(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task SupplierController_PatchSupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("supplierName", "Adidas");
            A.CallTo(() => _supplierRepos.Patch(5, update)).Throws<Exception>();
            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.PatchSupplier(5, update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region CreateNewSupplier(Supplier supplier)
        [TestMethod]
        public async Task SupplierController_CreateNewSupplier_ReturnOK()
        {
            //Arrange
            var supplier = new Supplier()
            {
                IdSupplier = 1,
                Addresss = "",
                Doibongsuppliers = null,
                EstablishDate = DateTime.Now,
                Images = null,
                Leaguesuppliers = null,
                PhoneNumber = "",
                RepresentativeName = "",
                SupplierName = "",
                Supplierservices = null
            };
            A.CallTo(() => _supplierRepos.Create(supplier)).Returns(await _repo.Create(supplier));
            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.PostSupplier(supplier);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task SupplierController_CreateNewSupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            var supplier = A.Fake<Supplier>();
            A.CallTo(() => _supplierRepos.Create(supplier)).Throws<Exception>();
            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.PostSupplier(supplier);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion

        #region  DeleteSupplier(int id)
        [TestMethod]
        public async Task SupplierController_DeleteSupplier_ReturnNoContent()
        {
            //Arrange
            int idSupplier = 1;
            await _repo.Delete(idSupplier);
            A.CallTo(() => _supplierRepos.Delete(idSupplier)).Returns(true);
            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.DeleteSupplier(idSupplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task SupplierController_DeleteSupplier_ReturnNotFound()
        {
            //Arrange
            int idSupplier = 1;

            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            SupplierRepository _repo;
            var mock = GetFakeSupplierList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Suppliers).Returns(mock.Object);
            _repo = new SupplierRepository(_dbContextMock.Object);
            await _repo.Delete(idSupplier);
            A.CallTo(() => _supplierRepos.Delete(idSupplier)).Returns(false);

            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.DeleteSupplier(idSupplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task SupplierController_DeleteSupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            int idSupplier = 1;
            A.CallTo(() => _supplierRepos.Delete(idSupplier)).Throws<Exception>();
            var _controller = new SuppliersController(_supplierRepos);
            //Act
            var result = await _controller.DeleteSupplier(idSupplier);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion
    }
}
