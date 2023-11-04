using FakeItEasy;
using FootBallManagerAPI.Controllers;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Models;
using FootBallManagerAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class LeagueSuppliersControllerTest
    {
        private readonly ILeagueSupplierRepository _leagueSupplierRepos;
        public LeagueSuppliersControllerTest()
        {
            this._leagueSupplierRepos = A.Fake<ILeagueSupplierRepository>();
        }

        #region GetLeaguesuppliers()
        [TestMethod]
        public async Task LeagueSuppliersController_GetLeaguesuppliers_ReturnOK()
        {
            //Arrange
            var leagueSupplier = A.Fake<IEnumerable<Leaguesupplier>>();

            A.CallTo(() => _leagueSupplierRepos.GetAll()).Returns(leagueSupplier);

            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.GetLeaguesuppliers();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_GetLeaguesuppliers_ReturnsProblemResultOnException()
        {
            // Arrange

            A.CallTo(() => _leagueSupplierRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new LeagueSuppliersController(_leagueSupplierRepos);

            // Act
            var result = await controller.GetLeaguesuppliers();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region GetLeaguesupplier(int idSupplier, int idLeague)
        [TestMethod]
        public async Task LeagueSuppliersController_GetLeaguesupplierById_ReturnOK()
        {
            //Arrange
            var leagueSupplier = new Leaguesupplier() { IdLeague = 1, IdSupplier = 1, Duration = 5, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(10), Status = 1, IdLeagueNavigation = null, IdSupplierNavigation = null };

            A.CallTo(() => _leagueSupplierRepos.GetById(1, 1)).Returns(leagueSupplier);

            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.GetLeaguesupplier(1, 1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task LeagueSuppliersController_GetLeaguesupplierById_ReturnNotFound()
        {
            //Arrange

            A.CallTo(() => _leagueSupplierRepos.GetById(1, 1)).Returns<Leaguesupplier>(null);

            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.GetLeaguesupplier(1, 1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task LeagueSuppliersController_GetLeaguesupplierById_ReturnsProblemResultOnException()
        {
            //Arrange

            A.CallTo(() => _leagueSupplierRepos.GetById(1, 1)).Throws<Exception>();

            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);

            //Act
            var result = await _controller.GetLeaguesupplier(1, 1);



            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region UpdateLeaguesupplier(int idSupplier, int idLeague, Leaguesupplier leaguesupplier)
        [TestMethod]
        public async Task LeagueSuppliersController_UpdateLeaguesupplier_ReturnNoContent()
        {
            //Arrange
            var leagueSupplier = A.Fake<Leaguesupplier>();
            leagueSupplier.IdSupplier = 1;
            leagueSupplier.IdLeague = 1;
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.UpdateLeaguesupplier(leagueSupplier.IdSupplier, leagueSupplier.IdLeague, leagueSupplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_UpdateLeaguesupplier_ReturnBadRequest()
        {
            //Arrange
            var leagueSupplier = A.Fake<Leaguesupplier>();
            leagueSupplier.IdSupplier = 1;
            leagueSupplier.IdLeague = 1;
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.UpdateLeaguesupplier(2, 1, leagueSupplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task LeagueSuppliersController_UpdateLeaguesupplier_ReturnNotFound()
        {
            //Arrange
            var leagueSupplier = A.Fake<Leaguesupplier>();
            leagueSupplier.IdSupplier = 2;
            leagueSupplier.IdLeague = 2;
            A.CallTo(() => _leagueSupplierRepos.GetById(2, 2)).Throws<Exception>();
            A.CallTo(() => _leagueSupplierRepos.Update(leagueSupplier)).Throws<DbUpdateConcurrencyException>();
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);

            //Act
            var result = await _controller.UpdateLeaguesupplier(2, 2, leagueSupplier);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_UpdateLeaguesupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            var leagueSupplier = A.Fake<Leaguesupplier>();
            leagueSupplier.IdSupplier = 2;
            leagueSupplier.IdLeague = 2;

            A.CallTo(() => _leagueSupplierRepos.GetById(2, 2)).Returns(leagueSupplier);
            A.CallTo(() => _leagueSupplierRepos.Update(leagueSupplier)).Throws<DbUpdateConcurrencyException>();
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);


            //Act
            var result = await _controller.UpdateLeaguesupplier(2,2, leagueSupplier);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task LeagueSuppliersController_UpdateLeaguesupplier_ReturnsProblemResultOnNullObject()
        {
            //Arrange


            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);


            //Act
            var result = await _controller.UpdateLeaguesupplier(2, 2, null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region  PatchLeagueSupplier(int idSupplier, int idLeague, JsonPatchDocument leagueSupplierModel)
        [TestMethod]
        public async Task LeagueSuppliersController_PatchLeagueSupplier_ReturnNoContent()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("status", 2);
            A.CallTo(() => _leagueSupplierRepos.Patch(5, 5, update)).Returns(true);
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.PatchLeagueSupplier(5, 5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_PatchLeagueSupplier_ReturnBadRequest()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("status", 2);
            A.CallTo(() => _leagueSupplierRepos.Patch(5, 5, update)).Returns(false);

            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);

            //Act
            var result = await _controller.PatchLeagueSupplier(5, 5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_PatchLeagueSupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("status", 2);
            A.CallTo(() => _leagueSupplierRepos.Patch(5, 5, update)).Throws<Exception>();
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);

            //Act
            var result = await _controller.PatchLeagueSupplier(5, 5, update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region CreateNewLeagueSupplier(Leaguesupplier leaguesupplier)
        [TestMethod]
        public async Task LeagueSuppliersController_CreateNewLeagueSupplier_ReturnOK()
        {
            //Arrange
            var leagueSupplier = A.Fake<Leaguesupplier>();
            leagueSupplier.IdSupplier = 1;
            leagueSupplier.IdLeague = 1;
            A.CallTo(() => _leagueSupplierRepos.Create(leagueSupplier)).Returns(leagueSupplier.IdSupplier);
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.PostLeaguesupplier(leagueSupplier);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_CreateNewLeagueSupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            var leagueSupplier = A.Fake<Leaguesupplier>();
            leagueSupplier.IdSupplier = 1;
            leagueSupplier.IdLeague = 1;
            A.CallTo(() => _leagueSupplierRepos.Create(leagueSupplier)).Throws<Exception>();
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.PostLeaguesupplier(null);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion


        #region DeleteLeaguesupplier(int idSupplier, int idLeague)
        [TestMethod]
        public async Task LeagueSuppliersController_DeleteLeaguesupplier_ReturnNoContent()
        {
            //Arrange
            int IdSupplier = 1;
            int IdLeague = 1;
            A.CallTo(() => _leagueSupplierRepos.Delete(IdSupplier, IdLeague)).Returns(true);
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.DeleteLeaguesupplier(IdSupplier, IdLeague);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_DeleteLeaguesupplier_ReturnNotFound()
        {
            //Arrange
            int IdSupplier = 1;
            int IdLeague = 1;
            A.CallTo(() => _leagueSupplierRepos.Delete(IdSupplier, IdLeague)).Returns(false);

            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);

            //Act
            var result = await _controller.DeleteLeaguesupplier(IdSupplier, IdLeague);


            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task LeagueSuppliersController_DeleteLeaguesupplier_ReturnsProblemResultOnException()
        {
            //Arrange
            int IdSupplier = 1;
            int IdLeague = 1;
            A.CallTo(() => _leagueSupplierRepos.Delete(IdSupplier, IdLeague)).Throws<Exception>();
            var _controller = new LeagueSuppliersController(_leagueSupplierRepos);
            //Act
            var result = await _controller.DeleteLeaguesupplier(IdSupplier, IdLeague);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion
    }
}
