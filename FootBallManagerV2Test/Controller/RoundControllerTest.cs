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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class RoundControllerTest
    {
        private readonly IRoundRepository _roundRepos;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly RoundRepository _repo;
        public RoundControllerTest()
        {
            this._roundRepos = A.Fake<IRoundRepository>();
            var mock = GetFakeRoundList(false).BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Rounds).Returns(mock.Object);
            this._repo = new RoundRepository(this._dbContextMock.Object);
        }

        private static List<Round> GetFakeRoundList(bool isNull)
        {
            if (isNull) return new List<Round>();
            return new List<Round>() {
                new Round() {
                    Id = 1,
                Footballmatches=null,
                Iddisplay="12",
                Idgiaidau=1,
                IdgiaidauNavigation=null,
                Ngaybatdau=DateTime.Now,
                Soluongdoi=32,
                Tenvongdau="Vong 1"

                },
                new Round() {
                     Id =21,
                Footballmatches=null,
                Iddisplay="13",
                Idgiaidau=1,
                IdgiaidauNavigation=null,
                Ngaybatdau=DateTime.Now,
                Soluongdoi=32,
                Tenvongdau="Vong 2"

                }


            };
        }


        #region GetRounds()
        [TestMethod]
        public async Task RoundController_GetRounds_ReturnOK()
        {
            //Arrange
            RoundRepository res = new RoundRepository(A.Fake<FootBallManagerV2Context>());

            var rounds = await _repo.GetAll();

            A.CallTo(() => _roundRepos.GetAll()).Returns(rounds);

            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.GetRounds();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task RoundController_GetRounds_ReturnsProblemResultOnException()
        {

            Mock<FootBallManagerV2Context> _dbContextMock;
            RoundRepository _repo;
            var mock = GetFakeRoundList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Rounds).Returns(mock.Object);
            _repo = new RoundRepository(_dbContextMock.Object);
            await _repo.GetAll();
            // Arrange

            A.CallTo(() => _roundRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new RoundsController(_roundRepos);

            // Act
            var result = await controller.GetRounds();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region GetRound(int id)
        [TestMethod]
        public async Task RoundController_GetRoundById_ReturnOK()
        {
            //Arrange
            var round = await _repo.GetById(1);

            A.CallTo(() => _roundRepos.GetById(1)).Returns(round);

            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.GetRound(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task RoundController_GetRoundById_ReturnNotFound()
        {
            //Arrange

            A.CallTo(() => _roundRepos.GetById(1)).Returns<Round>(null);

            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.GetRound(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task RoundController_GetRoundById_ReturnsProblemResultOnException()
        {
            //Arrange

            A.CallTo(() => _roundRepos.GetById(1)).Throws<Exception>();

            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.GetRound(1);

            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion
        #region UpdateRound(int id, Round round)
        [TestMethod]
        public async Task RoundController_UpdateRound_ReturnNoContent()
        {
            //Arrange
            var round = A.Fake<Round>();
            round.Id = 2;
            round.Idgiaidau = 2;
            await _repo.Update(round);
            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.UpdateRound(round.Id, round);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task RoundController_UpdateRound_ReturnBadRequest()
        {
            //Arrange
            var round = A.Fake<Round>();
            round.Id = 1;
            round.Idgiaidau = 2;
            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.UpdateRound(2, round);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task RoundController_UpdateRound_ReturnNotFound()
        {
            //Arrange
            var round = A.Fake<Round>();
            round.Id = 1;
            round.Idgiaidau = 2;
            A.CallTo(() => _roundRepos.GetById(1)).Throws<Exception>();
            A.CallTo(() => _roundRepos.Update(round)).Throws<DbUpdateConcurrencyException>();
            var _controller = new RoundsController(_roundRepos);

            //Act
            var result = await _controller.UpdateRound(1, round);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task RoundController_UpdateRound_ReturnsProblemResultOnException()
        {
            //Arrange
            var round = A.Fake<Round>();
            round.Id = 2;
            round.Idgiaidau = 2;

            A.CallTo(() => _roundRepos.GetById(2)).Returns(round);
            A.CallTo(() => _roundRepos.Update(round)).Throws<DbUpdateConcurrencyException>();
            var _controller = new RoundsController(_roundRepos);

            //Act
            var result = await _controller.UpdateRound(2, round);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task RoundController_UpdateRound_ReturnsProblemResultOnNullObject()
        {
            //Arrange


            var _controller = new RoundsController(_roundRepos);

            //Act
            var result = await _controller.UpdateRound(2, null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region PatchRound(int id, JsonPatchDocument roundModel)
        [TestMethod]
        public async Task RoundController_PatchRound_ReturnNoContent()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("TENVONGDAU", "Final");
            await _repo.Patch(5, update);
            A.CallTo(() => _roundRepos.Patch(5, update)).Returns(true);
            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.PatchRound(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task RoundController_PatchRound_ReturnBadRequest()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("TENVONGDAU", "Final");

            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            RoundRepository _repo;
            var mock = GetFakeRoundList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Rounds).Returns(mock.Object);
            _repo = new RoundRepository(_dbContextMock.Object);
            await _repo.Patch(5, update) ;

            A.CallTo(() => _roundRepos.Patch(5, update)).Returns(false);
            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.PatchRound(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task RoundController_PatchRound_ReturnsProblemResultOnException()
        {
            //Arrange
            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("TENVONGDAU", "Final");
            A.CallTo(() => _roundRepos.Patch(5, update)).Throws<Exception>();
            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.PatchRound(5, update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion

        #region CreateNewRound(Round round)
        [TestMethod]
        public async Task RoundController_CreateNewRound_ReturnOK()
        {
            //Arrange
            var round = new Round()
            {
                Id = 1,
                Footballmatches = null,
                Iddisplay = "",
                Idgiaidau = 1,
                IdgiaidauNavigation = null,
                Ngaybatdau = DateTime.Now,
                Soluongdoi = 32,
                Tenvongdau = ""
            };
            A.CallTo(() => _roundRepos.Create(round)).Returns(await _repo.Create(round));
            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.PostRound(round);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task RoundController_CreateNewRound_ReturnsProblemResultOnException()
        {
            //Arrange
            var round = A.Fake<Round>();
            A.CallTo(() => _roundRepos.Create(round)).Throws<Exception>();
            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.PostRound(round);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion


        #region DeleteRound(int id)
        [TestMethod]
        public async Task RoundController_DeleteRound_ReturnNoContent()
        {
            //Arrange
            int idRound = 1;
            await _repo.Delete(idRound);
            A.CallTo(() => _roundRepos.Delete(idRound)).Returns(true);
            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.DeleteRound(idRound);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task RoundController_DeleteRound_ReturnNotFound()
        {
            //Arrange
            int idRound = 1;
            A.CallTo(() => _roundRepos.Delete(idRound)).Returns(false);

            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.DeleteRound(idRound);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task RoundController_DeleteRound_ReturnsProblemResultOnException()
        {
            //Arrange
            int idRound = 1;
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            RoundRepository _repo;
            var mock = GetFakeRoundList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Rounds).Returns(mock.Object);
            _repo = new RoundRepository(_dbContextMock.Object);
            await _repo.Delete(idRound);
            A.CallTo(() => _roundRepos.Delete(idRound)).Throws<Exception>();
            var _controller = new RoundsController(_roundRepos);
            //Act
            var result = await _controller.DeleteRound(idRound);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion
    }
}
