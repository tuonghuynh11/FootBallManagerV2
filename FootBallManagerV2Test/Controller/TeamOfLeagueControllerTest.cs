using FakeItEasy;
using FootBallManagerAPI.Controllers;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Repositories;
using FootBallManagerAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class TeamOfLeagueControllerTest
    {
        private readonly ITeamOfLeagueRepository _teamOfLeagueRepos;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly TeamOfLeagueRepository _repo;
        public TeamOfLeagueControllerTest()
        {
            this._teamOfLeagueRepos = A.Fake<ITeamOfLeagueRepository>();
            var mock = GetFakeTeamOfLeagueList(false).BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Teamofleagues).Returns(mock.Object);
            this._repo = new TeamOfLeagueRepository(this._dbContextMock.Object);
        }

        private static List<Teamofleague> GetFakeTeamOfLeagueList(bool isNull)
        {
            if (isNull) return new List<Teamofleague>();
            return new List<Teamofleague>() {
                new Teamofleague() {
                     Id = 1,
                    Iddoibong="atm",
                    IddoibongNavigation=null,
                    Idgiaidau=1,
                    IdgiaidauNavigation=null

                },
                new Teamofleague() {
                    Id = 2,
                    Iddoibong="mc",
                    IddoibongNavigation=null,
                    Idgiaidau=1,
                    IdgiaidauNavigation=null

                }


            };
        }

        #region GetTeamofleagues()
        [TestMethod]
        public async Task TeamOfLeagueController_GetTeamofleagues_ReturnOK()
        {
            //Arrange
            TeamOfLeagueRepository res = new TeamOfLeagueRepository(A.Fake<FootBallManagerV2Context>());

            var teamofleagues = await _repo.GetAll();

            A.CallTo(() => _teamOfLeagueRepos.GetAll()).Returns(teamofleagues);

            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.GetTeamofleagues();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TeamOfLeagueController_GetTeamofleagues_ReturnsProblemResultOnException()
        {
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            TeamOfLeagueRepository _repo;
            var mock = GetFakeTeamOfLeagueList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Teamofleagues).Returns(mock.Object);
            _repo = new TeamOfLeagueRepository(_dbContextMock.Object);
            await _repo.GetAll();
            // Arrange

            A.CallTo(() => _teamOfLeagueRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new TeamOfLeaguesController(_teamOfLeagueRepos);

            // Act
            var result = await controller.GetTeamofleagues();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region GetTeamofleague(int id)
        [TestMethod]
        public async Task TeamOfLeagueController_GetTeamOfLeagueById_ReturnOK()
        {
            //Arrange
            List<Teamofleague> list = await _repo.GetById(1);

            A.CallTo(() => _teamOfLeagueRepos.GetById(1)).Returns(list);

            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.GetTeamofleague(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task TeamOfLeagueController_GetTeamOfLeagueById_ReturnNotFound()
        {
            //Arrange

            A.CallTo(() => _teamOfLeagueRepos.GetById(1)).Returns<List<Teamofleague>>(null);

            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.GetTeamofleague(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task TeamOfLeagueController_GetTeamOfLeagueById_ReturnsProblemResultOnException()
        {
            //Arrange

            A.CallTo(() => _teamOfLeagueRepos.GetById(1)).Throws<Exception>();

            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.GetTeamofleague(1);

            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region UpdateTeamofleague(int id, Teamofleague teamofleague)
        [TestMethod]
        public async Task TeamOfLeagueController_UpdateTeamofleague_ReturnNoContent()
        {
            //context
          
            _repo.TeamofleagueExists(1);
            //Arrange
            Teamofleague teamofleague = A.Fake<Teamofleague>();
            teamofleague.Iddoibong = "mc";
            teamofleague.Id = 1;
            await _repo.Update(teamofleague);
            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.UpdateTeamofleague(teamofleague.Id, teamofleague);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TeamOfLeagueController_UpdateTeamofleague_ReturnBadRequest()
        {
            //Arrange
            Teamofleague teamofleague = A.Fake<Teamofleague>();
            teamofleague.Iddoibong = "mc";
            teamofleague.Id = 1;
            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.UpdateTeamofleague(2, teamofleague);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task TeamOfLeagueController_UpdateTeamofleague_ReturnNotFound()
        {
            //Arrange
            Teamofleague teamofleague = A.Fake<Teamofleague>();
            teamofleague.Iddoibong = "mc";
            teamofleague.Id = 2;

            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            TeamOfLeagueRepository _repo;
            var mock = GetFakeTeamOfLeagueList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Teamofleagues).Returns(mock.Object);
            _repo = new TeamOfLeagueRepository(_dbContextMock.Object);
             _repo.TeamofleagueExists(2);


            A.CallTo(() => _teamOfLeagueRepos.TeamofleagueExists(2)).Returns(false);
            A.CallTo(() => _teamOfLeagueRepos.Update(teamofleague)).Throws<DbUpdateConcurrencyException>();
            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);

            //Act
            var result = await _controller.UpdateTeamofleague(2, teamofleague);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task TeamOfLeagueController_UpdateTeamofleague_ReturnsProblemResultOnException()
        {
            //Arrange
            Teamofleague teamofleague = A.Fake<Teamofleague>();
            teamofleague.Iddoibong = "mc";
            teamofleague.Id = 2;

            A.CallTo(() => _teamOfLeagueRepos.TeamofleagueExists(2)).Returns(true);
            A.CallTo(() => _teamOfLeagueRepos.Update(teamofleague)).Throws<DbUpdateConcurrencyException>();
            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);

            //Act
            var result = await _controller.UpdateTeamofleague(2, teamofleague);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task TeamOfLeagueController_UpdateTeamofleague_ReturnsProblemResultOnNullObject()
        {
            //Arrange


            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);

            //Act
            var result = await _controller.UpdateTeamofleague(2, null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region PatchTeamofleague(int id, JsonPatchDocument teamOfLeagueModel)
        [TestMethod]
        public async Task TeamOfLeagueController_PatchTeamofleague_ReturnNoContent()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("IDDOIBONG", "atm");
            await _repo.Patch(5, update);
            A.CallTo(() => _teamOfLeagueRepos.Patch(5, update)).Returns(true);
            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.PatchTeamofleague(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TeamOfLeagueController_PatchTeamofleague_ReturnBadRequest()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("IDDOIBONG", "atm");

            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            TeamOfLeagueRepository _repo;
            var mock = GetFakeTeamOfLeagueList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Teamofleagues).Returns(mock.Object);
            _repo = new TeamOfLeagueRepository(_dbContextMock.Object);
            await _repo.Patch(5,update);

            A.CallTo(() => _teamOfLeagueRepos.Patch(5, update)).Returns(false);
            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.PatchTeamofleague(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task TeamOfLeagueController_PatchTeamofleague_ReturnsProblemResultOnException()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("IDDOIBONG", "atm");
            A.CallTo(() => _teamOfLeagueRepos.Patch(5, update)).Throws<Exception>();
            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.PatchTeamofleague(5, update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion



        #region  CreateNewTeamofleague(Teamofleague teamofleague)
        [TestMethod]
        public async Task TeamOfLeagueController_CreateNewTeamofleague_ReturnOK()
        {
            //Arrange
            Teamofleague teamofleague = new Teamofleague()
            {
                Id = 1,
                Iddoibong = "atm",
                IddoibongNavigation = null,
                Idgiaidau = 1,
                IdgiaidauNavigation = null
            };
            A.CallTo(() => _teamOfLeagueRepos.Create(teamofleague)).Returns(await _repo.Create(teamofleague));
            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.PostTeamofleague(teamofleague);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task TeamOfLeagueController_CreateNewTeamofleague_ReturnsProblemResultOnException()
        {
            //Arrange
            Teamofleague teamofleague = A.Fake<Teamofleague>();
            A.CallTo(() => _teamOfLeagueRepos.Create(teamofleague)).Throws<Exception>();
            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.PostTeamofleague(teamofleague);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion

        #region DeleteTeamofleague(int idGiaiDau,string idTeam)
        [TestMethod]
        public async Task TeamOfLeagueController_DeleteTeamofleague_ReturnNoContent()
        {
            //Arrange
            int idGiaiDau = 1;
            string idTeam = "atm";
            await _repo.Delete(idGiaiDau, idTeam);
            A.CallTo(() => _teamOfLeagueRepos.Delete(idGiaiDau,idTeam)).Returns(true);
            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.DeleteTeamofleague(idGiaiDau, idTeam);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task TeamOfLeagueController_DeleteTeamofleague_ReturnNotFound()
        {
            //Arrange
            int idGiaiDau = 1;
            string idTeam = "atm";
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            TeamOfLeagueRepository _repo;
            var mock = GetFakeTeamOfLeagueList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Teamofleagues).Returns(mock.Object);
            _repo = new TeamOfLeagueRepository(_dbContextMock.Object);
            await _repo.Delete(idGiaiDau,idTeam);
            A.CallTo(() => _teamOfLeagueRepos.Delete(idGiaiDau, idTeam)).Returns(false);

            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.DeleteTeamofleague(idGiaiDau, idTeam);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task TeamOfLeagueController_DeleteTeamofleague_ReturnsProblemResultOnException()
        {
            //Arrange
            int idGiaiDau = 1;
            string idTeam = "atm";
            A.CallTo(() => _teamOfLeagueRepos.Delete(idGiaiDau, idTeam)).Throws<Exception>();
            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.DeleteTeamofleague(idGiaiDau, idTeam);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion

        #region IsTeamJoinLeague(int idLeague, string idTeam)
        [TestMethod]
        public async Task TeamOfLeagueController_IsTeamJoinLeague_ReturnOK()
        {
            //Arrange
            int idGiaiDau = 1;
            string idTeam = "atm";
            await _repo.IsTeamJoinLeague(idGiaiDau, idTeam);
            A.CallTo(() => _teamOfLeagueRepos.IsTeamJoinLeague(idGiaiDau, idTeam)).Returns(await _repo.IsTeamJoinLeague(idGiaiDau,idTeam));

            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.IsTeamJoinLeague(idGiaiDau, idTeam);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TeamOfLeagueController_IsTeamJoinLeague_ReturnsBadRequest()
        {
            //Arrange
            int idGiaiDau = 1;
            string idTeam = "atm";
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            TeamOfLeagueRepository _repo;
            var mock = GetFakeTeamOfLeagueList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Teamofleagues).Returns(mock.Object);
            _repo = new TeamOfLeagueRepository(_dbContextMock.Object);
            await _repo.IsTeamJoinLeague(idGiaiDau,idTeam);
            A.CallTo(() => _teamOfLeagueRepos.IsTeamJoinLeague(idGiaiDau, idTeam)).Throws<Exception>();
            var _controller = new TeamOfLeaguesController(_teamOfLeagueRepos);
            //Act
            var result = await _controller.IsTeamJoinLeague(idGiaiDau, idTeam);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));

        }
        #endregion
    }
}
