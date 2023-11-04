using FakeItEasy;
using FootBallManagerAPI.Controllers;
using FootBallManagerAPI.Entities;
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
    public class TapLuyenControllerTest
    {
        private readonly ITapLuyenRepository _tapLuyenRepos;
        public TapLuyenControllerTest()
        {
            this._tapLuyenRepos = A.Fake<ITapLuyenRepository>();
        }


        #region GetTapluyens()
        [TestMethod]
        public async Task TapLuyenController_GetTapluyens_ReturnOK()
        {
            //Arrange
            var tapluyens = A.Fake<IEnumerable<Tapluyen>>();

            A.CallTo(() => _tapLuyenRepos.GetAll()).Returns(tapluyens);

            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.GetTapluyens();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TapLuyenController_GetTapluyens_ReturnsProblemResultOnException()
        {
            // Arrange

            A.CallTo(() => _tapLuyenRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new TapluyensController(_tapLuyenRepos);

            // Act
            var result = await controller.GetTapluyens();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region GetTapluyen(int id)
        [TestMethod]
        public async Task TapLuyenController_GetTapluyenById_ReturnOK()
        {
            //Arrange
            var tapluyen = new Tapluyen() { 
                Id=1,
                Iddoibong="atm",
                IddoibongNavigation=null,
                Ghichu="",
                Hoatdong="",
                Idnguoiquanly=1,
                IdnguoiquanlyNavigation=null,
                Thoigianbatdau= DateTime.Now,
                Thoigianketthuc=DateTime.Now.AddHours(1),
                Trangthai= "finish"
            };

            A.CallTo(() => _tapLuyenRepos.GetById(1)).Returns(tapluyen);

            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.GetTapluyen(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task TapLuyenController_GetTapluyenById_ReturnNotFound()
        {
            //Arrange

            A.CallTo(() => _tapLuyenRepos.GetById(1)).Returns<Tapluyen>(null);

            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.GetTapluyen(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task TapLuyenController_GetTapluyenById_ReturnsProblemResultOnException()
        {
            //Arrange

            A.CallTo(() => _tapLuyenRepos.GetById(1)).Throws<Exception>();

            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.GetTapluyen(1);

            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region UpdateTapluyen(int id, Tapluyen tapluyen)
        [TestMethod]
        public async Task TapLuyenController_UpdateTapluyen_ReturnNoContent()
        {
            //Arrange
            var tapluyen = A.Fake<Tapluyen>();
            tapluyen.Id = 2;
            tapluyen.Ghichu = "Run 10 rounds";
            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.UpdateTapluyen(tapluyen.Id, tapluyen);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TapLuyenController_UpdateTapluyen_ReturnBadRequest()
        {
            //Arrange
            var tapluyen = A.Fake<Tapluyen>();
            tapluyen.Id = 2;
            tapluyen.Ghichu = "Run 10 rounds";
            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.UpdateTapluyen(1, tapluyen);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task TapLuyenController_UpdateTapluyen_ReturnNotFound()
        {
            //Arrange
            var tapluyen = A.Fake<Tapluyen>();
            tapluyen.Id = 2;
            tapluyen.Ghichu = "Run 10 rounds";
            A.CallTo(() => _tapLuyenRepos.GetById(2)).Throws<Exception>();
            A.CallTo(() => _tapLuyenRepos.Update(tapluyen)).Throws<DbUpdateConcurrencyException>();
            var _controller = new TapluyensController(_tapLuyenRepos);

            //Act
            var result = await _controller.UpdateTapluyen(2, tapluyen);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task TapLuyenController_UpdateTapluyen_ReturnsProblemResultOnException()
        {
            //Arrange
            var tapluyen = A.Fake<Tapluyen>();
            tapluyen.Id = 2;
            tapluyen.Ghichu = "Run 10 rounds";

            A.CallTo(() => _tapLuyenRepos.GetById(2)).Returns(tapluyen);
            A.CallTo(() => _tapLuyenRepos.Update(tapluyen)).Throws<DbUpdateConcurrencyException>();
            var _controller = new TapluyensController(_tapLuyenRepos);

            //Act
            var result = await _controller.UpdateTapluyen(2, tapluyen);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task TapLuyenController_UpdateTapluyen_ReturnsProblemResultOnNullObject()
        {
            //Arrange


            var _controller = new TapluyensController(_tapLuyenRepos);

            //Act
            var result = await _controller.UpdateTapluyen(2, null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region PatchTapLuyen(int id, JsonPatchDocument tapLuyenModel)
        [TestMethod]
        public async Task TapLuyenController_PatchTapLuyen_ReturnNoContent()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("HOATDONG", "Run 100 rounds");
            A.CallTo(() => _tapLuyenRepos.Patch(5, update)).Returns(true);
            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.PatchTapLuyen(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TapLuyenController_PatchTapLuyen_ReturnBadRequest()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("HOATDONG", "Run 100 rounds");
            A.CallTo(() => _tapLuyenRepos.Patch(5, update)).Returns(false);
            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.PatchTapLuyen(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task TapLuyenController_PatchTapLuyen_ReturnsProblemResultOnException()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("HOATDONG", "Run 100 rounds");
            A.CallTo(() => _tapLuyenRepos.Patch(5, update)).Throws<Exception>();
            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.PatchTapLuyen(5, update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region CreateNewTapLuyen(Tapluyen tapluyen)
        [TestMethod]
        public async Task TapLuyenController_CreateNewTapLuyen_ReturnOK()
        {
            //Arrange
            Tapluyen tapluyen = A.Fake<Tapluyen>();
            A.CallTo(() => _tapLuyenRepos.Create(tapluyen)).Returns(tapluyen.Id);
            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.PostTapluyen(tapluyen);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task TapLuyenController_CreateNewTapLuyen_ReturnsProblemResultOnException()
        {
            //Arrange
            Tapluyen tapluyen = A.Fake<Tapluyen>();
            A.CallTo(() => _tapLuyenRepos.Create(tapluyen)).Throws<Exception>();
            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.PostTapluyen(tapluyen);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion

        #region DeleteTapluyen(int id)
        [TestMethod]
        public async Task TapLuyenController_DeleteTapluyen_ReturnNoContent()
        {
            //Arrange
            int idTapLuyen = 1;
            A.CallTo(() => _tapLuyenRepos.Delete(idTapLuyen)).Returns(true);
            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.DeleteTapluyen(idTapLuyen);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TapLuyenController_DeleteTapluyen_ReturnNotFound()
        {
            //Arrange
            int idTapLuyen = 1;

            A.CallTo(() => _tapLuyenRepos.Delete(idTapLuyen)).Returns(false);

            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.DeleteTapluyen(idTapLuyen);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task TapLuyenController_DeleteTapluyen_ReturnsProblemResultOnException()
        {
            //Arrange
            int idTapLuyen = 1;
            A.CallTo(() => _tapLuyenRepos.Delete(idTapLuyen)).Throws<Exception>();
            var _controller = new TapluyensController(_tapLuyenRepos);
            //Act
            var result = await _controller.DeleteTapluyen(idTapLuyen);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion
    }
}
