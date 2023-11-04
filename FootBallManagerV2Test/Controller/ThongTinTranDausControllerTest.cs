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
    public class ThongTinTranDausControllerTest
    {
        private readonly IThongTinTranDauRepository _thongTinTranDauRepos;
        public ThongTinTranDausControllerTest()
        {
            this._thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>();
        }


        #region GetThongtintrandaus()
        [TestMethod]
        public async Task ThongTinTranDausController_GetThongtintrandaus_ReturnOK()
        {
            //Arrange
            var thongtintrandaus = A.Fake<IEnumerable<Thongtintrandau>>();

            A.CallTo(() => _thongTinTranDauRepos.GetAll()).Returns(thongtintrandaus);

            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.GetThongtintrandaus();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_GetThongtintrandaus_ReturnsProblemResultOnException()
        {
            // Arrange

            A.CallTo(() => _thongTinTranDauRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new ThongtintrandausController(_thongTinTranDauRepos);

            // Act
            var result = await controller.GetThongtintrandaus();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion



        #region GetThongtintrandau(int idTranDau)
        [TestMethod]
        public async Task ThongTinTranDausController_GetThongtintrandauById_ReturnOK()
        {
            //Arrange
            List<Thongtintrandau> list = new List<Thongtintrandau>()
            {
                new Thongtintrandau()
                {
                    Id = 1,
                    Diem=3,
                    Iddoibong="atm",
                    IddoibongNavigation=null,
                    Idtrandau=1,
                    IdtrandauNavigation=null,
                    Items=null,
                    Ketqua=1,
                    Thedo=1,
                    Thevang=1
                }
            };

            A.CallTo(() => _thongTinTranDauRepos.GetById(1)).Returns(list);

            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.GetThongtintrandau(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task ThongTinTranDausController_GetThongtintrandauById_ReturnNotFound()
        {
            //Arrange

            A.CallTo(() => _thongTinTranDauRepos.GetById(1)).Returns<List<Thongtintrandau>>(null);

            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.GetThongtintrandau(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task ThongTinTranDausController_GetThongtintrandauById_ReturnsProblemResultOnException()
        {
            //Arrange

            A.CallTo(() => _thongTinTranDauRepos.GetById(1)).Throws<Exception>();

            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.GetThongtintrandau(1);

            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region UpdateThongtintrandau(int id,  Thongtintrandau thongtintrandau)
        [TestMethod]
        public async Task ThongTinTranDausController_UpdateThongtintrandau_ReturnNoContent()
        {
            //Arrange
            List<Thongtintrandau> thongtintrandaus = await _thongTinTranDauRepos.GetById(0);
            Thongtintrandau thongtintrandau = A.Fake<Thongtintrandau>();
            thongtintrandau.Id = 2;
            thongtintrandau.Thedo = 2;
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.UpdateThongtintrandau(thongtintrandau.Id, thongtintrandau);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_UpdateThongtintrandau_ReturnBadRequest()
        {
            //Arrange
            List<Thongtintrandau> thongtintrandaus = await _thongTinTranDauRepos.GetById(0);
            Thongtintrandau thongtintrandau = A.Fake<Thongtintrandau>();
            thongtintrandau.Thedo = 2;
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.UpdateThongtintrandau(2, thongtintrandau);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task ThongTinTranDausController_UpdateThongtintrandau_ReturnNotFound()
        {
            //Arrange
            List<Thongtintrandau> thongtintrandaus = await _thongTinTranDauRepos.GetById(0);
            Thongtintrandau thongtintrandau = A.Fake<Thongtintrandau>();
            thongtintrandau.Id = 2;
            thongtintrandau.Thedo = 2;
            A.CallTo(() => _thongTinTranDauRepos.GetById(2)).Throws<Exception>();
            A.CallTo(() => _thongTinTranDauRepos.Update(thongtintrandau)).Throws<DbUpdateConcurrencyException>();
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);

            //Act
            var result = await _controller.UpdateThongtintrandau(2, thongtintrandau);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_UpdateThongtintrandau_ReturnsProblemResultOnException()
        {
            //Arrange
            List<Thongtintrandau> thongtintrandaus = await _thongTinTranDauRepos.GetById(0);
            Thongtintrandau thongtintrandau =A.Fake<Thongtintrandau>();
            thongtintrandau.Id = 2;
            thongtintrandau.Thedo = 2;

            A.CallTo(() => _thongTinTranDauRepos.GetById(2)).Returns(thongtintrandaus);
            A.CallTo(() => _thongTinTranDauRepos.Update(thongtintrandau)).Throws<DbUpdateConcurrencyException>();
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);

            //Act
            var result = await _controller.UpdateThongtintrandau(2, thongtintrandau);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task ThongTinTranDausController_UpdateThongtintrandau_ReturnsProblemResultOnNullObject()
        {
            //Arrange


            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);

            //Act
            var result = await _controller.UpdateThongtintrandau(2, null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region PatchThongtintrandau(int id, JsonPatchDocument thongTinTranDauModel)
        [TestMethod]
        public async Task ThongTinTranDausController_PatchThongTinTranDau_ReturnNoContent()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("THEDO", 3);
            A.CallTo(() => _thongTinTranDauRepos.Patch(5, update)).Returns(true);
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.PatchThongtintrandau(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_PatchThongTinTranDau_ReturnBadRequest()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("THEDO", 3);
            A.CallTo(() => _thongTinTranDauRepos.Patch(5, update)).Returns(false);
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.PatchThongtintrandau(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_PatchThongTinTranDau_ReturnsProblemResultOnException()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("THEDO", 3);
            A.CallTo(() => _thongTinTranDauRepos.Patch(5, update)).Throws<Exception>();
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.PatchThongtintrandau(5, update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region CreateNewThongtintrandau(Thongtintrandau thongtintrandau)
        [TestMethod]
        public async Task ThongTinTranDausController_CreateNewThongTinTranDau_ReturnOK()
        {
            //Arrange
            Thongtintrandau thongtintrandau = A.Fake<Thongtintrandau>();
            A.CallTo(() => _thongTinTranDauRepos.Create(thongtintrandau)).Returns(thongtintrandau.Id);
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.CreateNewThongtintrandau(thongtintrandau);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_CreateNewThongTinTranDau_ReturnsProblemResultOnException()
        {
            //Arrange
            Thongtintrandau thongtintrandau = A.Fake<Thongtintrandau>();
            A.CallTo(() => _thongTinTranDauRepos.Create(thongtintrandau)).Throws<Exception>();
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.CreateNewThongtintrandau(thongtintrandau);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion



        #region DeleteThongtintrandau(int id)
        [TestMethod]
        public async Task ThongTinTranDausController_DeleteThongtintrandau_ReturnNoContent()
        {
            //Arrange
            int idThongTinTranDau = 1;
            A.CallTo(() => _thongTinTranDauRepos.Delete(idThongTinTranDau)).Returns(true);
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.DeleteThongtintrandau(idThongTinTranDau);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_DeleteThongtintrandau_ReturnNotFound()
        {
            //Arrange
            int idThongTinTranDau = 1;
            A.CallTo(() => _thongTinTranDauRepos.Delete(idThongTinTranDau)).Returns(false);

            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.DeleteThongtintrandau(idThongTinTranDau);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_DeleteThongtintrandau_ReturnsProblemResultOnException()
        {
            //Arrange
            int idThongTinTranDau = 1;
            A.CallTo(() => _thongTinTranDauRepos.Delete(idThongTinTranDau)).Throws<Exception>();
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.DeleteThongtintrandau(idThongTinTranDau);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion
    }
}
