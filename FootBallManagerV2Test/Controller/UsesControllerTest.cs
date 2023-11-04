using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FootBallManagerAPI;
using FootBallManagerAPI.Controllers;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Models;
using FootBallManagerAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyWebApp.Models;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class UsesControllerTest
    {
        public FootBallManagerV2Context _context;
        public IOptionsMonitor<AppSettings> _appSettings;

        public UsesControllerTest()
        {
            _context = new FootBallManagerV2Context();
            _appSettings = A.Fake<IOptionsMonitor<AppSettings>>();
            A.CallTo(() => _appSettings.CurrentValue).Returns(new AppSettings() { SecretKey = "dvkglqrstwvycwbvfitesgmnlxvcjlla" });

        }
        #region GetUsers
        [TestMethod]
        public async Task UsersController_GetUsers_ReturnOK()
        {
            var _controller = new UsersController(_context, _appSettings);
            //Act
            var result = await _controller.GetUsers();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UsersController_GetUsers_ReturnsNotFoundResultWhenContextIsNullAsync()
        {
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(null);

            var controller = new UsersController(dbContext,_appSettings);

            // Act
            var result = await controller.GetUsers();

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UsersController_GetUsers_ReturnsProblemResultOnException()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Throws<Exception>(); // Simulate an exception
            var controller = new UsersController(dbContext, _appSettings);

            // Act
            var result = await controller.GetUsers();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region GetUser(int id)
        [TestMethod]
        public async Task UsersController_GetUserById_ReturnOK()
        {
            var _controller = new UsersController(_context, _appSettings);
            //Act
            int idUser = 1;
            var result = await _controller.GetUser(idUser);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<User>));
        }
        [TestMethod]
        public async Task UsersController_GetUserById_ReturnsNotFoundResultWhenContextIsNullAsync()
        {
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(null);

            var controller = new UsersController(dbContext, _appSettings);

            // Act
            int idUser = 1;

            var result = await controller.GetUser(idUser);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<User>));
            Assert.AreEqual(StatusCodes.Status404NotFound, ((NotFoundResult)result.Result).StatusCode);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UsersController_GetUserById_ReturnsProblemResultOnException()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Throws<Exception>(); // Simulate an exception
            var controller = new UsersController(dbContext, _appSettings);

            // Act
            int idUser = 1;
            var result = await controller.GetUser(idUser);
            var objectResult = (ObjectResult)result.Result;

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion

        #region UpdateUser(int id, User user)
        [TestMethod]
        public async Task UsersController_UpdateUser_ReturnNoContent()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            var controller = new UsersController(dbContext, _appSettings);
            //Act
            int idUser = 1;
            User updateUser = dbContext.Users.Find(1);
            var result = await controller.UpdateUser(idUser,updateUser);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task UsersController_UpdateUser_ReturnBadRequest()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            var controller = new UsersController(dbContext, _appSettings);
            //Act
            int idUser = 2;
            User updateUser = dbContext.Users.Find(1);
            var result = await controller.UpdateUser(idUser, updateUser);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task UsersController_UpdateUser_ReturnsProblemResultOnException()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();

            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            User updateUser = dbContext.Users.Find(1);

            // Configure the DbContext to throw a DbUpdateConcurrencyException when SaveChanges is called
            A.CallTo(() => dbContext.Users).Throws<Exception>();

            var controller = new UsersController(dbContext, _appSettings);
            //Act
            int idUser = 1;
            var result = await controller.UpdateUser(idUser, updateUser);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region CreateNewUser(User user)
        [TestMethod]
        public async Task UsersController_CreateNewUser_ReturnOK()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            var controller = new UsersController(dbContext, _appSettings);
            //Act
            User newUser = new User() { Iduserrole=3,Username="testcase",Password="123455667", Displayname="testcase", Email="a@gmail.com"};
           
            var result = await controller.CreateNewUser(newUser);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<User>));
        }

        [TestMethod]
        public async Task UsersController_CreateNewUser_ReturnsProblemResultOnException()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();

            // Configure the DbContext to throw a DbUpdateConcurrencyException when SaveChanges is called
            A.CallTo(() => dbContext.Users).Returns(null);

            var controller = new UsersController(dbContext, _appSettings);
            //Act
            User newUser = new User() { Iduserrole = 3, Username = "testcase", Password = "123455667", Displayname = "testcase", Email = "a@gmail.com" };
            var result = await controller.CreateNewUser(newUser);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<User>));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        # region PatchUser(int id, JsonPatchDocument updateUser)
        [TestMethod]
        public async Task UsersController_PatchUser_ReturnNoContent()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            var controller = new UsersController(dbContext, _appSettings);
            //Act
            int idUser = 1;
            JsonPatchDocument updateUser = new JsonPatchDocument();
            updateUser.Replace("displayName", "ADMIN");
            var result = await controller.PatchUser(idUser, updateUser);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task UsersController_PatchUser_ReturnNotFoundCase1()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(null);
            var controller = new UsersController(dbContext, _appSettings);
            //Act
            int idUser = 1;
            JsonPatchDocument updateUser = new JsonPatchDocument();
            updateUser.Replace("displayName", "ADMIN");
            var result = await controller.PatchUser(idUser, updateUser);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task UsersController_PatchUser_ReturnNotFoundCase2()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            var controller = new UsersController(dbContext, _appSettings);
            //Act
            int idUser = -2;
            JsonPatchDocument updateUser = new JsonPatchDocument();
            updateUser.Replace("displayName", "ADMIN");
            var result = await controller.PatchUser(idUser, updateUser);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public async Task UsersController_PatchUser_ReturnsProblemResultOnException()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Throws<Exception>();
            var controller = new UsersController(dbContext, _appSettings);
            //Act
            int idUser = 1;
            JsonPatchDocument updateUser = new JsonPatchDocument();
            updateUser.Replace("displayName", "ADMIN");
            var result = await controller.PatchUser(idUser, updateUser);
            var objectResult = result as ObjectResult;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion



        #region DeleteUser(int id)

        [TestMethod]
        public async Task UsersController_DeleteUser_ReturnNoContent()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            var controller = new UsersController(dbContext, _appSettings);
            //Act
            int idUser = 3;
            var result = await controller.DeleteUser(idUser);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task UsersController_DeleteUser_ReturnNotFoundCase1()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(null);
            var controller = new UsersController(dbContext, _appSettings);
            //Act
            int idUser = 3;
            var result = await controller.DeleteUser(idUser);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task UsersController_DeleteUser_ReturnNotFoundCase2()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            var controller = new UsersController(dbContext, _appSettings);
            //Act
            int idUser = -2;
       
            var result = await controller.DeleteUser(idUser);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public async Task UsersController_DeleteUser_ReturnsProblemResultOnException()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Throws<Exception>();
            var controller = new UsersController(dbContext, _appSettings);
            //Act
            int idUser = 1;
            var result = await controller.DeleteUser(idUser);
            var objectResult = result as ObjectResult;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }


        #endregion

        #region ResetPassword(ResetPassword resetObj )
        [TestMethod]
        public async Task UsersController_ResetPassword_ReturnOk()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            var controller = new UsersController(dbContext, _appSettings);

            //Act
            ResetPassword resetPassword = new ResetPassword() { IdUser = 1, NewPassword = "12345" };

            var result = await controller.ResetPassword(resetPassword);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UsersController_ResetPassword_ReturnBadRequest()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            var controller = new UsersController(dbContext, _appSettings);

            //Act
            ResetPassword resetPassword = new ResetPassword() { IdUser = 1 };

            var result = await controller.ResetPassword(resetPassword);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }


        [TestMethod]
        public async Task UsersController_ResetPassword_ReturnsProblemResultOnException_Case1()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Throws<Exception>();
            var controller = new UsersController(dbContext, _appSettings);

            //Act
            ResetPassword resetPassword = new ResetPassword() { IdUser = 1, NewPassword = "12345" };
            var result = await controller.ResetPassword(resetPassword);
            var objectResult = result as ObjectResult;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task UsersController_ResetPassword_ReturnsProblemResultOnException_Case2()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(null);
            var controller = new UsersController(dbContext, _appSettings);

            //Act
            ResetPassword resetPassword = new ResetPassword() { IdUser = 1, NewPassword = "12345" };
            var result = await controller.ResetPassword(resetPassword);
            var objectResult = result as ObjectResult;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task UsersController_ResetPassword_ReturnsNotFound()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            var controller = new UsersController(dbContext, _appSettings);

            //Act
            ResetPassword resetPassword = new ResetPassword() { IdUser = -1, NewPassword = "12345" };
            var result = await controller.ResetPassword(resetPassword);
            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        #endregion



        #region LogIn(LoginUser login)
        [TestMethod]
        public async Task UsersController_Login_ReturnOk()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
             A.CallTo(() => dbContext.Userroles).Returns(_context.Userroles);
            var controller = new UsersController(dbContext, _appSettings);

            //Act
            LoginUser login = new LoginUser() {UserName="coach",Password= "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" };
            var result = await controller.LogIn(login);
        
            
            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UsersController_Login_ReturnNotFound()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            A.CallTo(() => dbContext.Userroles).Returns(_context.Userroles);
            var controller = new UsersController(dbContext, _appSettings);

            //Act
            LoginUser login = new LoginUser() { UserName = "-2", Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" };
            var result = await controller.LogIn(login);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
        #endregion


        #region RenewToken(TokenModel renewToken)
        [TestMethod]
        public async Task UsersController_RenewToken_ReturnOk()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            A.CallTo(() => dbContext.Refreshtokens).Returns(_context.Refreshtokens);
            A.CallTo(() => dbContext.Userroles).Returns(_context.Userroles);
            var controller = new UsersController(dbContext, _appSettings);

            //Act
            LoginUser login = new LoginUser() { UserName = "coach", Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" };
            var userLogin = await controller.LogIn(login);
            OkObjectResult res = (OkObjectResult)userLogin;
            TokenModel tk = (res.Value as APIResponse).data as TokenModel;
            var result = await controller.RenewToken(tk);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task UsersController_RenewToken_Check_Access_Token_Not_Expired()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            A.CallTo(() => dbContext.Refreshtokens).Returns(_context.Refreshtokens);
            A.CallTo(() => dbContext.Userroles).Returns(_context.Userroles);
            var controller = new UsersController(dbContext, _appSettings);

            //Act

            TokenModel tk = new TokenModel() {
                accessToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImNvYWNoMSIsImVtYWlsIjoiaG10QGdtYWlsLmNvbSIsImp0aSI6Ijg5MTI4YTU4LTBkNTAtNGVkNi1iMTcxLTFhM2ZjMzRjMDc0MyIsIlVzZXJOYW1lIjoiY29hY2giLCJJZCI6IjIiLCJSb2xlIjoiUHJlc2lkZW50IiwidG9rZW5JZCI6ImEwYTdhNDRhLTZlOTYtNDQ0OS05NDQ1LTg4ODk5MzIwZjRiYiIsIm5iZiI6MTY5ODgzMDYwNCwiZXhwIjoxNzAxNDIyNjA0LCJpYXQiOjE2OTg4MzA2MDR9.y2gmn4pnzTWcltKfsJd0J0C-gkQWvYltPxhuefR7UHOO26TdtsawGyBMUHavEqVzvtlhO6WxDm8mV63CKE0Maw",
                refreshToken = "6JybzuR01udoffO6wUO182u0R2Dsqn5fkjgbsOB8SII="

            };
            var result = await controller.RenewToken(tk);
            APIResponse objResult = ((result as OkObjectResult).Value as APIResponse);
            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            //Assert.AreEqual("Access Token has not yet expired", objResult.message);
            
        }

        [TestMethod]
        public async Task UsersController_RenewToken_Check_Refresh_Token_Existed()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            A.CallTo(() => dbContext.Refreshtokens).Returns(_context.Refreshtokens);
            A.CallTo(() => dbContext.Userroles).Returns(_context.Userroles);
            var controller = new UsersController(dbContext, _appSettings);

            //Act

            TokenModel tk = new TokenModel()
            {
                accessToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImNvYWNoMSIsImVtYWlsIjoiaG10QGdtYWlsLmNvbSIsImp0aSI6Ijg5MTI4YTU4LTBkNTAtNGVkNi1iMTcxLTFhM2ZjMzRjMDc0MyIsIlVzZXJOYW1lIjoiY29hY2giLCJJZCI6IjIiLCJSb2xlIjoiUHJlc2lkZW50IiwidG9rZW5JZCI6ImEwYTdhNDRhLTZlOTYtNDQ0OS05NDQ1LTg4ODk5MzIwZjRiYiIsIm5iZiI6MTY5ODgzMDYwNCwiZXhwIjoxNzAxNDIyNjA0LCJpYXQiOjE2OTg4MzA2MDR9.y2gmn4pnzTWcltKfsJd0J0C-gkQWvYltPxhuefR7UHOO26TdtsawGyBMUHavEqVzvtlhO6WxDm8mV63CKE0Maw",
                refreshToken = "HZ7kQuthnghnAV0d/bNBgHMWqFh/GsHiMtkGL/VS2JY="

            };
            var result = await controller.RenewToken(tk);
            APIResponse objResult = ((result as OkObjectResult).Value as APIResponse);
            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual("Refresh Token does not exist", objResult.message);

        }

        [TestMethod]
        public async Task UsersController_RenewToken_Check_Refresh_Token_Is_Used()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            A.CallTo(() => dbContext.Refreshtokens).Returns(_context.Refreshtokens);
            A.CallTo(() => dbContext.Userroles).Returns(_context.Userroles);
            var controller = new UsersController(dbContext, _appSettings);

            //Act

            TokenModel tk = new TokenModel()
            {
                accessToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImNvYWNoMSIsImVtYWlsIjoiaG10QGdtYWlsLmNvbSIsImp0aSI6Ijg5MTI4YTU4LTBkNTAtNGVkNi1iMTcxLTFhM2ZjMzRjMDc0MyIsIlVzZXJOYW1lIjoiY29hY2giLCJJZCI6IjIiLCJSb2xlIjoiUHJlc2lkZW50IiwidG9rZW5JZCI6ImEwYTdhNDRhLTZlOTYtNDQ0OS05NDQ1LTg4ODk5MzIwZjRiYiIsIm5iZiI6MTY5ODgzMDYwNCwiZXhwIjoxNzAxNDIyNjA0LCJpYXQiOjE2OTg4MzA2MDR9.y2gmn4pnzTWcltKfsJd0J0C-gkQWvYltPxhuefR7UHOO26TdtsawGyBMUHavEqVzvtlhO6WxDm8mV63CKE0Maw",
                refreshToken = "HZ7kQuthnghnAV0d/bNBgHMWqFh/GsHiMtkGL/VS1JY="

            };
            var result = await controller.RenewToken(tk);
            APIResponse objResult = ((result as OkObjectResult).Value as APIResponse);
            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual("Refresh Token has been used", objResult.message);

        }

        [TestMethod]
        public async Task UsersController_RenewToken_Check_Token_Not_Macth()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            A.CallTo(() => dbContext.Refreshtokens).Returns(_context.Refreshtokens);
            A.CallTo(() => dbContext.Userroles).Returns(_context.Userroles);
            var controller = new UsersController(dbContext, _appSettings);

            //Act

            TokenModel tk = new TokenModel()
            {
                accessToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImNvYWNoMSIsImVtYWlsIjoiaG10QGdtYWlsLmNvbSIsImp0aSI6Ijg5MTI4YTU4LTBkNTAtNGVkNi1iMTcxLTFhM2ZjMzRjMDc0MyIsIlVzZXJOYW1lIjoiY29hY2giLCJJZCI6IjIiLCJSb2xlIjoiUHJlc2lkZW50IiwidG9rZW5JZCI6ImEwYTdhNDRhLTZlOTYtNDQ0OS05NDQ1LTg4ODk5MzIwZjRiYiIsIm5iZiI6MTY5ODgzMDYwNCwiZXhwIjoxNzAxNDIyNjA0LCJpYXQiOjE2OTg4MzA2MDR9.y2gmn4pnzTWcltKfsJd0J0C-gkQWvYltPxhuefR7UHOO26TdtsawGyBMUHavEqVzvtlhO6WxDm8mV63CKE0Maw",
                refreshToken = "bn4WQxa9Iosl9e0chPf0dh4ob/m42PWoa5yt3TfaUo4="

            };
            var result = await controller.RenewToken(tk);
            APIResponse objResult = ((result as OkObjectResult).Value as APIResponse);
            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual("Token doesn't match", objResult.message);

        }

        [TestMethod]
        public async Task UsersController_RenewToken_Check_Refresh_Token_Is_Revoked()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();
            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            A.CallTo(() => dbContext.Refreshtokens).Returns(_context.Refreshtokens);
            A.CallTo(() => dbContext.Userroles).Returns(_context.Userroles);
            var controller = new UsersController(dbContext, _appSettings);

            //Act

            TokenModel tk = new TokenModel()
            {
                accessToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImNvYWNoMSIsImVtYWlsIjoiaG10QGdtYWlsLmNvbSIsImp0aSI6Ijg5MTI4YTU4LTBkNTAtNGVkNi1iMTcxLTFhM2ZjMzRjMDc0MyIsIlVzZXJOYW1lIjoiY29hY2giLCJJZCI6IjIiLCJSb2xlIjoiUHJlc2lkZW50IiwidG9rZW5JZCI6ImEwYTdhNDRhLTZlOTYtNDQ0OS05NDQ1LTg4ODk5MzIwZjRiYiIsIm5iZiI6MTY5ODgzMDYwNCwiZXhwIjoxNzAxNDIyNjA0LCJpYXQiOjE2OTg4MzA2MDR9.y2gmn4pnzTWcltKfsJd0J0C-gkQWvYltPxhuefR7UHOO26TdtsawGyBMUHavEqVzvtlhO6WxDm8mV63CKE0Maw",
                refreshToken = "mclpZYv6srLi6yWSmxenDiTPPxMhJQRE9bC6hH9PJpU="

            };
            var result = await controller.RenewToken(tk);
            APIResponse objResult = ((result as OkObjectResult).Value as APIResponse);
            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual("Refresh Token has been revoked", objResult.message);

        }
        #endregion
    }
}
