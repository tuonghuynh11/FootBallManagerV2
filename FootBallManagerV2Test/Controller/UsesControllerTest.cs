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
using MockQueryable.Moq;
using Moq;
using MyWebApp.Models;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class UsesControllerTest
    {
        public FootBallManagerV2Context _context;
        public FootBallManagerV2Context _context2;
        public IOptionsMonitor<AppSettings> _appSettings;

        private readonly Mock<FootBallManagerV2Context> _dbContextMock;

        public UsesControllerTest()
        {
          
            
            _appSettings = A.Fake<IOptionsMonitor<AppSettings>>();
            A.CallTo(() => _appSettings.CurrentValue).Returns(new AppSettings() { SecretKey = "dvkglqrstwvycwbvfitesgmnlxvcjlla" });


            var mockUsers = GetFakeUsersList(false).BuildMock().BuildMockDbSet();
            var mockUserRoles = GetFakeUserRoleList(false).BuildMock().BuildMockDbSet();
            var mockRefreshToken = GetFakeRefreshTokensList(false).BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Userroles).Returns(mockUserRoles.Object);
            this._dbContextMock.Setup(x => x.Users).Returns(mockUsers.Object);
            this._dbContextMock.Setup(x => x.Refreshtokens).Returns(mockRefreshToken.Object);
            this._context =this._dbContextMock.Object;

        }
        [TestInitialize]
        public void init()
        {
            _context2 = new FootBallManagerV2Context();
            try
            {
                _context2.Doibongs.ToList();
                _context2.Suppliers.ToList();
                _context2.Supplierservices.ToList();
                _context2.Tapluyens.ToList();
            }
            catch 
            {
                return;
            }

        }
        private static List<User> GetFakeUsersList(bool isNull)
        {
            if (isNull) return new List<User>();
            return new List<User>() {
                new User() {
                     Id = 1,
                    Iduserrole=2,
                    Username="ADMIN",
                    Password="03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",
                    Avatar=null,
                    Displayname="ADMIN",
                    Email="a@gmail.com",
                    Idavatar=null,
                    Idnhansu=86,
                    Idotp=null,
                    IdotpNavigation=null,
                    IduserroleNavigation=null,
                    Refreshtokens = null
                    },
                new User() {
                      Id = 2,
                    Iduserrole=4,
                    Username="coach",
                    Password="03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",
                    Avatar=null,
                    Displayname="coach1",
                    Email="a@gmail.com",
                    Idavatar=null,
                    Idnhansu=3,
                    Idotp=null,
                    IdotpNavigation=null,
                    IduserroleNavigation=null,
                    Refreshtokens = null

                },
                 new User() {
                      Id = 3,
                    Iduserrole=1,
                    Username="coach2",
                    Password="03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",
                    Avatar=null,
                    Displayname="coach2",
                    Email="a@gmail.com",
                    Idavatar=null,
                    Idnhansu=82,
                    Idotp=null,
                    IdotpNavigation=null,
                    IduserroleNavigation=null,
                    Refreshtokens = null

                }


            };
        }


        private static List<Userrole> GetFakeUserRoleList(bool isNull)
        {
            if (isNull) return new List<Userrole>();
            return new List<Userrole>() {
                new Userrole() {
                     Id = 1,
                    Role="Coach",
                    Users=null
                    },
                new Userrole() {
                      Id = 2,
                   Role="Admin",
                    Users=null


                },
                 new Userrole() {
                      Id = 3,
                    Role="Assistant",
                    Users=null

                },
                 new Userrole() {
                      Id = 4,
                    Role="President",
                    Users=null

                },
                  new Userrole() {
                      Id = 5,
                    Role="Supplier",
                    Users=null

                }


            };
        }

        private static List<Refreshtoken> GetFakeRefreshTokensList(bool isNull)
        {
            if (isNull) return new List<Refreshtoken>();
            return new List<Refreshtoken>() {
                new Refreshtoken() {
                    Id = Guid.Parse("9C40B666-025D-4464-9B40-7C72877C2C7C"),
                   UserId=1,
                   Token="2YeMUfbCU0j9Z8DAhGgtNNBSnvVNju62eO4hkyB/vyg=",
                   JwtId="ab9f6779-f6c3-49df-9a03-2d0a6035bd5e",
                   IsRevoked=false,
                   IssuedAt=DateTime.Now,
                   Expired=DateTime.Now,
                   IsUsed=false,
                   User=null,
                   
                    },
               new Refreshtoken() {
                     Id = Guid.Parse("5458BF7D-32D6-4257-A910-05D634A94344"),
                   UserId=1,
                   Token="w1a7U0TPsCjCBVnj7zkvARUpNZ5/GsuZuIs/u5Xxc8M=",
                   JwtId="dd7597a3-1954-4da2-b147-5773b2c67a9f",
                   IsRevoked=true,
                   IssuedAt=DateTime.Now,
                   Expired=DateTime.Now,
                   IsUsed=true,
                   User=null,

                    },
                 new Refreshtoken() {
                    Id = Guid.Parse("4F443016-D938-4D03-A4A4-0707357770AC"),
                   UserId=1,
                   Token="9iYKzFy1F4fOa1vicTfZ2eoaLZXF++sNfUID2vRT0MM=",
                   JwtId="3d33cad4-356b-4f11-882b-dbc1cbf3183c",
                   IsRevoked=false,
                   IssuedAt=DateTime.Now,
                   Expired=DateTime.Now,
                   IsUsed=false,
                   User=null,

                    },
                 new Refreshtoken() {
                      Id = Guid.Parse("706FC5ED-A0D9-42A0-BD30-1B910ACBB5C4"),
                   UserId=8,
                   Token="mclpZYv6srLi6yWSmxenDiTPPxMhJQRE9bC6hH9PJpU=",
                   JwtId="c15f3559-fb64-494c-90e7-4fbf92f9c05a",
                   IsRevoked=true,
                   IssuedAt=DateTime.Now,
                   Expired=DateTime.Now,
                   IsUsed=false,
                   User=null,

                    },
                  new Refreshtoken() {
                      Id = Guid.Parse("82CA90F2-3B2D-4A92-ADA4-77312CDED9DA"),
                   UserId=2,
                   Token="HZ7kQuthnghnAV0d/bNBgHMWqFh/GsHiMtkGL/VS1JY=",
                   JwtId="72a82f09-149b-4ff1-bf0a-bf3f5f02d223",
                   IsRevoked=true,
                   IssuedAt=DateTime.Now,
                   Expired=DateTime.Now,
                   IsUsed=true,
                   User=null,

                    },
                   new Refreshtoken() {
                      Id = Guid.Parse("170D0D10-1671-4D45-82AF-93D39EEA3C27"),
                   UserId=1,
                   Token="bn4WQxa9Iosl9e0chPf0dh4ob/m42PWoa5yt3TfaUo4=",
                   JwtId="3a76dcd9-5bd9-4af8-91ee-8e973542e070",
                   IsRevoked=false,
                   IssuedAt=DateTime.Now,
                   Expired=DateTime.Now,
                   IsUsed=false,
                   User=null,

                    },

            };
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
            var user = new User()
            {
                Avatar=null,
                Displayname="",
                Email="",
                Id=1,
                Idavatar=1,
                Idnhansu=1,
                Idotp=1,
                IdotpNavigation=null,
                Iduserrole=1,
                IduserroleNavigation=null,
                Password="",
                Refreshtokens=null,
                Username=""
            };
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
        public async Task UsersController_GetUserById_ReturnsNotFoundResult_WhenUserIsNotExist()
        {
            var dbContext = A.Fake<FootBallManagerV2Context>();

            var controller = new UsersController(_context, _appSettings);

            // Act
            int idUser = 10;

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
            var controller = new UsersController(_context, _appSettings);
            //Act
            int idUser = 1;
            User updateUser = A.Fake<User>();
            updateUser.Id = idUser;
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
            var controller = new UsersController(_context, _appSettings);
            //Act
            int idUser = 2;
            User updateUser = A.Fake<User>();
            var result = await controller.UpdateUser(idUser, updateUser);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task UsersController_UpdateUser_ReturnsNotFound()
        {
            // Arrange
            var dbContext = A.Fake<FootBallManagerV2Context>();

            A.CallTo(() => dbContext.Users).Returns(_context.Users);
            User updateUser = A.Fake<User>();
            updateUser.Id = 1;

            // Configure the DbContext to throw a DbUpdateConcurrencyException when SaveChanges is called
            A.CallTo(() => dbContext.Users).Throws<Exception>();

            var controller = new UsersController(dbContext, _appSettings);
            //Act
            int idUser = 1;
            var result = await controller.UpdateUser(idUser, updateUser);
            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
      

        [TestMethod]
        public async Task UsersController_UpdateUser_ReturnsProblemResultOnException()
        {
            // Arrange

            var dbContext = A.Fake<FootBallManagerV2Context>();
            User updateUser = new User();
            updateUser.Id = 1;

            // Configure the DbContext to throw a DbUpdateConcurrencyException when SaveChanges is called
            //A.CallTo(() => _context.Users.Update(updateUser)).Throws<Exception>();

            var controller = new UsersController(_context, _appSettings);
            //Act
            int idUser = 1;
            var result = await controller.UpdateUser(idUser, null);
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
            var controller = new UsersController(_context, _appSettings);
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
            var controller = new UsersController(_context, _appSettings);
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
            //var dbContext = A.Fake<FootBallManagerV2Context>();
            //A.CallTo(() => dbContext.Users).Returns(_context.Users);
            // A.CallTo(() => dbContext.Userroles).Returns(_context.Userroles);
            var controller = new UsersController(_context, _appSettings);

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
            var controller = new UsersController(_context, _appSettings);

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
