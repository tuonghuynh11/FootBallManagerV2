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
using Moq;
using MockQueryable.Moq;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class LeagueControllerTest
    {
        public readonly ILeagueRepository _leagueRepo;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly LeagueRepository _repo;
        public LeagueControllerTest()
        {
            this._leagueRepo = A.Fake<ILeagueRepository>();
            var mock = GetFakeLeagueList().BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Leagues).Returns(mock.Object);
            this._repo = new LeagueRepository(this._dbContextMock.Object);
        }
        private static List<League> GetFakeLeagueList()
        {
            return new List<League>() {
                new League() {
                     Id = 1,
                     Ngaybatdau = new DateTime(2024, 1, 1),
                     Ngayketthuc = new DateTime(2024, 1, 2),
                     Tengiaidau = "Champions League",
                     Idquocgia = 1,
                     Hinhanh = new byte[10],
                     IdquocgiaNavigation = new Quoctich(),
                     Leaguesuppliers = new List<Leaguesupplier>(),
                     Rounds = new List<Round>(),
                     Teamofleagues = new List<Teamofleague>(),
                     Thongtingiaidaus = new List<Thongtingiaidau>(),

        },
                new League() {
                     Id = 2,
                     Ngaybatdau = new DateTime(2024, 1, 1),
                     Ngayketthuc = new DateTime(2024, 1, 2),
                     Tengiaidau = "League 1",
                     Idquocgia = 1,
                     Hinhanh = new byte[10],
                     IdquocgiaNavigation = new Quoctich(),
                     Leaguesuppliers = new List<Leaguesupplier>(),
                     Rounds = new List<Round>(),
                     Teamofleagues = new List<Teamofleague>(),
                     Thongtingiaidaus = new List<Thongtingiaidau>(),

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
            LeagueRepository res = new LeagueRepository(A.Fake<FootBallManagerV2Context>());

            var Leagues = await _repo.GetAllLeagueListAsync();
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
            League.Ngaybatdau = new DateTime(2024, 1, 1);
            League.Ngayketthuc = new DateTime(2024, 1, 2);
            League.Tengiaidau = "Champions League";
            League.Idquocgia = 1;
            League.Hinhanh = new byte[10];
            League.IdquocgiaNavigation = new Quoctich();
            League.Leaguesuppliers = new List<Leaguesupplier>();
            League.Rounds = new List<Round>();
            League.Teamofleagues = new List<Teamofleague>();
            League.Thongtingiaidaus = new List<Thongtingiaidau>();
            await _repo.updateLeagueAsync(1, League) ;
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
            var newLeague = new League();
            newLeague.Id = 1;
            var League = await _repo.addLeagueAsync(newLeague);
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
            await _repo.deleteLeagueAsync(id);
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
