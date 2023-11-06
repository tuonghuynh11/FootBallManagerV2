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
using FootBallManagerAPI.Repository;
using Moq;
using MockQueryable.Moq;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class NotificationControllerTest
    {
        public readonly INotificationRepository _notiRepo;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly NotificationRepository _repo;
        public NotificationControllerTest()
        {
            this._notiRepo = A.Fake<INotificationRepository>();
            var mock = GetFakeNotificationList().BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Notifications).Returns(mock.Object);
            this._repo = new NotificationRepository(this._dbContextMock.Object);
        }
        private static List<Notification> GetFakeNotificationList()
        {
            return new List<Notification>() {
                new Notification() {
                      Id = 1,
                      Idhlv = 1,
                      Notify = "New",
                      Checked = "true",
                      IdhlvNavigation = new Huanluyenvien(),
    
            },
                new Notification() {
                     Id = 2,
                      Idhlv = 1,
                      Notify = "New",
                      Checked = "true",
                      IdhlvNavigation = new Huanluyenvien(),

                }


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
            NotificationRepository res = new NotificationRepository(A.Fake<FootBallManagerV2Context>());

            var Notifications = await _repo.GetAllNotificationsAsync();
            A.CallTo(() => _notiRepo.GetAllNotificationsAsync()).Returns(Notifications);
            var controller = new NotificationsController(_notiRepo);
            var result = await controller.GetNotifications();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _notiRepo.GetAllNotificationsAsync()).Throws<Exception>();
            var controller = new NotificationsController(_notiRepo);

            var result = await controller.GetNotifications();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var Notification = A.Fake<Notification>();
            A.CallTo(() => _notiRepo.GetNotificationAsync(1)).Returns(Notification);

            var controller = new NotificationsController(_notiRepo);
            var result = await controller.GetNotification(1);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var Notification = A.Fake<Notification>();
            Notification = null;
            A.CallTo(() => _notiRepo.GetNotificationAsync(1)).Returns(Notification);

            var controller = new NotificationsController(_notiRepo);
            var result = await controller.GetNotification(1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _notiRepo.GetNotificationAsync(1)).Throws<Exception>();
            var controller = new NotificationsController(_notiRepo);

            var result = await controller.GetNotification(1);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var Notification = new Notification();
            Notification.Id = 1;
            Notification.Idhlv = 1;
            Notification.Notify = "New";
            Notification.Checked = "true";
            Notification.IdhlvNavigation = new Huanluyenvien();
            await _repo.updateNotificationAsync(1, Notification);
            A.CallTo(() => _notiRepo.updateNotificationAsync(1, Notification)).Invokes(() => {
                Assert.AreEqual(Notification.Id, 1);
            });
            var controller = new NotificationsController(_notiRepo);

            var result = await controller.PutNotification(1, Notification);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var Notification = new Notification();
            Notification.Id = 2;
            A.CallTo(() => _notiRepo.updateNotificationAsync(1, Notification)).Invokes(() => {
                Assert.AreEqual(Notification.Id, 1);
            });
            var controller = new NotificationsController(_notiRepo);

            var result = await controller.PutNotification(1, Notification);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var Notification = new Notification();
            Notification.Id = 1;
            A.CallTo(() => _notiRepo.updateNotificationAsync(1, Notification)).Throws<Exception>();
            var controller = new NotificationsController(_notiRepo);

            var result = await controller.PutNotification(1, Notification);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var newNotification = new Notification();
            newNotification.Id = 1;
            var Notification = await _repo.addNotificationAsync(newNotification);
           A.CallTo(() => _notiRepo.addNotificationAsync(Notification)).Returns(Notification);
            var controller = new NotificationsController(_notiRepo);

            var result = await controller.PostNotification(Notification);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var Notification = new Notification();
            Notification = null;
            A.CallTo(() => _notiRepo.addNotificationAsync(Notification)).Returns(Notification);
            var controller = new NotificationsController(_notiRepo);

            var result = await controller.PostNotification(Notification);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var Notification = new Notification();
            Notification.Id = 1;
            A.CallTo(() => _notiRepo.addNotificationAsync(Notification)).Throws<Exception>();
            var controller = new NotificationsController(_notiRepo);

            var result = await controller.PostNotification(Notification);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            int id = 1;
            await _repo.deleteNotificationAsync(id);
            A.CallTo(() => _notiRepo.deleteNotificationAsync(id)).Invokes(() => {
                Assert.AreEqual(id, 1);
            });
            var controller = new NotificationsController(_notiRepo);
            var result = await controller.DeleteNotification(id);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            int id = 1;
            A.CallTo(() => _notiRepo.deleteNotificationAsync(id)).Throws<Exception>();
            var controller = new NotificationsController(_notiRepo);
            var result = await controller.DeleteNotification(id);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
