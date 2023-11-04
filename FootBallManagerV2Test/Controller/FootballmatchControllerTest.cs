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
    public class FootballmatchControllerTest
    {
        public readonly IFootballmatchRepository _fmRepo;
        public FootballmatchControllerTest()
        {
            this._fmRepo = A.Fake<IFootballmatchRepository>();
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
            var Footballmatchs = A.Fake<List<Footballmatch>>();
            A.CallTo(() => _fmRepo.GetAllFootballmatch()).Returns(Footballmatchs);
            var controller = new FootballmatchesController(_fmRepo);
            var result = await controller.GetFootballmatches();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _fmRepo.GetAllFootballmatch()).Throws<Exception>();
            var controller = new FootballmatchesController(_fmRepo);

            var result = await controller.GetFootballmatches();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var Footballmatch = A.Fake<Footballmatch>();
            A.CallTo(() => _fmRepo.GetFootballmatch(1)).Returns(Footballmatch);

            var controller = new FootballmatchesController(_fmRepo);
            var result = await controller.GetFootballmatch(1);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var Footballmatch = A.Fake<Footballmatch>();
            Footballmatch = null;
            A.CallTo(() => _fmRepo.GetFootballmatch(1)).Returns(Footballmatch);

            var controller = new FootballmatchesController(_fmRepo);
            var result = await controller.GetFootballmatch(1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _fmRepo.GetFootballmatch(1)).Throws<Exception>();
            var controller = new FootballmatchesController(_fmRepo);

            var result = await controller.GetFootballmatch(1);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var Footballmatch = new Footballmatch();
            Footballmatch.Id = 1;
            A.CallTo(() => _fmRepo.updateFootballmatchAsync(1, Footballmatch)).Invokes(() => {
                Assert.AreEqual(Footballmatch.Id, 1);
            });
            var controller = new FootballmatchesController(_fmRepo);

            var result = await controller.PutFootballmatch(1, Footballmatch);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var Footballmatch = new Footballmatch();
            Footballmatch.Id = 2;
            A.CallTo(() => _fmRepo.updateFootballmatchAsync(1, Footballmatch)).Invokes(() => {
                Assert.AreEqual(Footballmatch.Id, 1);
            });
            var controller = new FootballmatchesController(_fmRepo);

            var result = await controller.PutFootballmatch(1, Footballmatch);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var Footballmatch = new Footballmatch();
            Footballmatch.Id = 1;
            A.CallTo(() => _fmRepo.updateFootballmatchAsync(1, Footballmatch)).Throws<Exception>();
            var controller = new FootballmatchesController(_fmRepo);

            var result = await controller.PutFootballmatch(1, Footballmatch);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var Footballmatch = new Footballmatch();
            Footballmatch.Id = 1;
            A.CallTo(() => _fmRepo.addFootballmatchAsync(Footballmatch)).Returns(Footballmatch);
            var controller = new FootballmatchesController(_fmRepo);

            var result = await controller.PostFootballmatch(Footballmatch);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var Footballmatch = new Footballmatch();
            Footballmatch = null;
            A.CallTo(() => _fmRepo.addFootballmatchAsync(Footballmatch)).Returns(Footballmatch);
            var controller = new FootballmatchesController(_fmRepo);

            var result = await controller.PostFootballmatch(Footballmatch);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var Footballmatch = new Footballmatch();
            Footballmatch.Id = 1;
            A.CallTo(() => _fmRepo.addFootballmatchAsync(Footballmatch)).Throws<Exception>();
            var controller = new FootballmatchesController(_fmRepo);

            var result = await controller.PostFootballmatch(Footballmatch);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            int id = 1;
            A.CallTo(() => _fmRepo.deleteFootballmatchAsync(id)).Invokes(() => {
                Assert.AreEqual(id, 1);
            });
            var controller = new FootballmatchesController(_fmRepo);
            var result = await controller.DeleteFootballmatch(id);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            int id = 1;
            A.CallTo(() => _fmRepo.deleteFootballmatchAsync(id)).Throws<Exception>();
            var controller = new FootballmatchesController(_fmRepo);
            var result = await controller.DeleteFootballmatch(id);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
