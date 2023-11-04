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

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class LeagueControllerTest
    {
        public readonly ILeagueRepository _leagueRepo;
        public LeagueControllerTest()
        {
            this._leagueRepo = A.Fake<ILeagueRepository>();
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
            var Leagues = A.Fake<List<League>>();
            A.CallTo(() => _leagueRepo.GetAllLeagueListAsync()).Returns(Leagues);
            var controller = new LeaguesController(_leagueRepo);
            var result = await controller.GetLeagues();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _leagueRepo.GetAllLeagueListAsync()).Throws<Exception>();
            var controller = new LeaguesController(_leagueRepo);

            var result = await controller.GetLeagues();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var League = A.Fake<League>();
            A.CallTo(() => _leagueRepo.GetLeagueAsync(1)).Returns(League);

            var controller = new LeaguesController(_leagueRepo);
            var result = await controller.GetLeague(1);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var League = A.Fake<League>();
            League = null;
            A.CallTo(() => _leagueRepo.GetLeagueAsync(1)).Returns(League);

            var controller = new LeaguesController(_leagueRepo);
            var result = await controller.GetLeague(1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _leagueRepo.GetLeagueAsync(1)).Throws<Exception>();
            var controller = new LeaguesController(_leagueRepo);

            var result = await controller.GetLeague(1);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var League = new League();
            League.Id = 1;
            A.CallTo(() => _leagueRepo.updateLeagueAsync(1, League)).Invokes(() => {
                Assert.AreEqual(League.Id, 1);
            });
            var controller = new LeaguesController(_leagueRepo);

            var result = await controller.PutLeague(1, League);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var League = new League();
            League.Id = 2;
            A.CallTo(() => _leagueRepo.updateLeagueAsync(1, League)).Invokes(() => {
                Assert.AreEqual(League.Id, 1);
            });
            var controller = new LeaguesController(_leagueRepo);

            var result = await controller.PutLeague(1, League);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var League = new League();
            League.Id = 1;
            A.CallTo(() => _leagueRepo.updateLeagueAsync(1, League)).Throws<Exception>();
            var controller = new LeaguesController(_leagueRepo);

            var result = await controller.PutLeague(1, League);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var League = new League();
            League.Id = 1;
            A.CallTo(() => _leagueRepo.addLeagueAsync(League)).Returns(League);
            var controller = new LeaguesController(_leagueRepo);

            var result = await controller.PostLeague(League);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var League = new League();
            League = null;
            A.CallTo(() => _leagueRepo.addLeagueAsync(League)).Returns(League);
            var controller = new LeaguesController(_leagueRepo);

            var result = await controller.PostLeague(League);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var League = new League();
            League.Id = 1;
            A.CallTo(() => _leagueRepo.addLeagueAsync(League)).Throws<Exception>();
            var controller = new LeaguesController(_leagueRepo);

            var result = await controller.PostLeague(League);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            int id = 1;
            A.CallTo(() => _leagueRepo.deleteLeagueAsync(id)).Invokes(() => {
                Assert.AreEqual(id, 1);
            });
            var controller = new LeaguesController(_leagueRepo);
            var result = await controller.DeleteLeague(id);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            int id = 1;
            A.CallTo(() => _leagueRepo.deleteLeagueAsync(id)).Throws<Exception>();
            var controller = new LeaguesController(_leagueRepo);
            var result = await controller.DeleteLeague(id);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
