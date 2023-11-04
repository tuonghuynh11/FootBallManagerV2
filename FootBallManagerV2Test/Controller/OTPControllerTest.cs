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
    public class OTPControllerTest
    {
        private readonly IOtpRepository _otpRepos;
        public OTPControllerTest()
        {
            this._otpRepos = A.Fake<IOtpRepository>();
        }

        #region GetOtps()
        [TestMethod]
        public async Task OTPController_GetOtps_ReturnOK()
        {
            //Arrange
            var otps = A.Fake<IEnumerable<Otp>>();

            A.CallTo(() => _otpRepos.GetAll()).Returns(otps);

            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.GetOtps();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task OTPController_GetOtps_ReturnsProblemResultOnException()
        {
            // Arrange

            A.CallTo(() => _otpRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new OtpsController(_otpRepos);

            // Act
            var result = await controller.GetOtps();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region GetOtp(int id)
        [TestMethod]
        public async Task OTPController_GetOtpById_ReturnOK()
        {
            //Arrange
            var otp = A.Fake<Otp>();

            A.CallTo(() => _otpRepos.GetById(1)).Returns(otp);

            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.GetOtp(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task OTPController_GetOtpById_ReturnNotFound()
        {
            //Arrange

            A.CallTo(() => _otpRepos.GetById(1)).Returns<Otp>(null);

            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.GetOtp(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task OTPController_GetOtpById_ReturnsProblemResultOnException()
        {
            //Arrange

            A.CallTo(() => _otpRepos.GetById(1)).Throws<Exception>();

            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.GetOtp(1);

            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region UpdateOtp(int id, Otp otp)
        [TestMethod]
        public async Task OTPController_UpdateOtp_ReturnNoContent()
        {
            //Arrange
            var otp = A.Fake<Otp>();
            otp.Id = 1;
            otp.Code = "123344";
            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.UpdateOtp(otp.Id, otp);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task OTPController_UpdateOtp_ReturnBadRequest()
        {
            //Arrange
            var otp = A.Fake<Otp>();
            otp.Id = 1;
            otp.Code = "123344";
            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.UpdateOtp(2, otp);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task OTPController_UpdateOtp_ReturnNotFound()
        {
            //Arrange
            var otp = A.Fake<Otp>();
            otp.Id = 2;
            otp.Code = "123344";
            A.CallTo(() => _otpRepos.GetById(2)).Throws<Exception>();
            A.CallTo(() => _otpRepos.Update(otp)).Throws<DbUpdateConcurrencyException>();
            var _controller = new OtpsController(_otpRepos);

            //Act
            var result = await _controller.UpdateOtp(2, otp);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task OTPController_UpdateOtp_ReturnsProblemResultOnException()
        {
            //Arrange
            var otp = A.Fake<Otp>();
            otp.Id = 2;
            otp.Code = "123344";

            A.CallTo(() => _otpRepos.GetById(2)).Returns(otp);
            A.CallTo(() => _otpRepos.Update(otp)).Throws<DbUpdateConcurrencyException>();
            var _controller = new OtpsController(_otpRepos);

            //Act
            var result = await _controller.UpdateOtp(2, otp);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task OTPController_UpdateOtp_ReturnsProblemResultOnNullObject()
        {
            //Arrange


            var _controller = new OtpsController(_otpRepos);

            //Act
            var result = await _controller.UpdateOtp(2, null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region PatchOtp(int id, JsonPatchDocument otpModel)
        [TestMethod]
        public async Task OTPController_PatchOtp_ReturnNoContent()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("Code", "131223");
            A.CallTo(() => _otpRepos.Patch(5, update)).Returns(true);
            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.PatchOtp(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task OTPController_PatchOtp_ReturnBadRequest()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("Code", "131223");
            A.CallTo(() => _otpRepos.Patch(5, update)).Returns(false);
            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.PatchOtp(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task OTPController_PatchOtp_ReturnsProblemResultOnException()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("Code", "131223");
            A.CallTo(() => _otpRepos.Patch(5, update)).Throws<Exception>();
            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.PatchOtp(5, update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region CreateNewOtp(Otp otp)
        [TestMethod]
        public async Task OTPController_CreateNewOtp_ReturnOK()
        {
            //Arrange
            var otp = A.Fake<Otp>();
            A.CallTo(() => _otpRepos.Create(otp)).Returns(otp.Id);
            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.PostOtp(otp);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task OTPController_CreateNewOtp_ReturnsProblemResultOnException()
        {
            //Arrange
            var otp = A.Fake<Otp>();
            A.CallTo(() => _otpRepos.Create(otp)).Throws<Exception>();
            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.PostOtp(otp);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion



        #region  DeleteOtp(int id)
        [TestMethod]
        public async Task ThongTinTranDausController_DeleteThongtintrandau_ReturnNoContent()
        {
            //Arrange
            int idOtp = 1;
            A.CallTo(() => _otpRepos.Delete(idOtp)).Returns(true);
            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.DeleteOtp(idOtp);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_DeleteThongtintrandau_ReturnNotFound()
        {
            //Arrange
            int idOtp = 1;
            A.CallTo(() => _otpRepos.Delete(idOtp)).Returns(false);

            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.DeleteOtp(idOtp);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_DeleteThongtintrandau_ReturnsProblemResultOnException()
        {
            //Arrange
            int idOtp = 1;
            A.CallTo(() => _otpRepos.Delete(idOtp)).Throws<Exception>();
            var _controller = new OtpsController(_otpRepos);
            //Act
            var result = await _controller.DeleteOtp(idOtp);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion

    }
}
