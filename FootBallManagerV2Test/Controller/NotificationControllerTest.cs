﻿using FakeItEasy;
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

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class NotificationControllerTest
    {
        public readonly INotificationRepository _notiRepo;
        public NotificationControllerTest()
        {
            this._notiRepo = A.Fake<INotificationRepository>();
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
            var Notifications = A.Fake<List<Notification>>();
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
            var Notification = new Notification();
            Notification.Id = 1;
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
