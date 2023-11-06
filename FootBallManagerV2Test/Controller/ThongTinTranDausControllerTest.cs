using FakeItEasy;
using FootBallManagerAPI.Controllers;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Models;
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
    public class ThongTinTranDausControllerTest
    {
        private readonly IThongTinTranDauRepository _thongTinTranDauRepos;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly ThongTinTranDauRepository _repo;

        public ThongTinTranDausControllerTest()
        {
            //ThongTinTranDauRepository thongTinTranDauRepository = new ThongTinTranDauRepository();

            this._thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>();
            var mock = GetFakeThongTinTranDauList(false).BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Thongtintrandaus).Returns(mock.Object);
            this._repo = new ThongTinTranDauRepository(this._dbContextMock.Object);
        }

        private static List<Thongtintrandau> GetFakeThongTinTranDauList(bool isNull)
        {
            if(isNull) return new List<Thongtintrandau>();
            return new List<Thongtintrandau>() {
                new Thongtintrandau() {
                   Id = 1,
                    Diem=3,
                    Iddoibong="atm",
                    IddoibongNavigation=null,
                    Idtrandau=1,
                    IdtrandauNavigation=null,
                    Items=null,
                    Ketqua=1,
                    Thedo=1,
                    Thevang=1

                },
                new Thongtintrandau() {
                   Id = 2,
                    Diem=3,
                    Iddoibong="mu",
                    IddoibongNavigation=null,
                    Idtrandau=1,
                    IdtrandauNavigation=null,
                    Items=null,
                    Ketqua=1,
                    Thedo=1,
                    Thevang=1

                }


            };
        }

        #region GetThongtintrandaus()
        [TestMethod]
        public async Task ThongTinTranDausController_GetThongtintrandaus_ReturnOK()
        {
            //Arrange
            var thongtintrandaus =await _repo.GetAll();
          

            A.CallTo(() => _thongTinTranDauRepos.GetAll()).Returns(thongtintrandaus);


            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.GetThongtintrandaus();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_GetThongtintrandaus_ReturnsProblemResultOnException()
        {
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            ThongTinTranDauRepository _repo;
            var mock = GetFakeThongTinTranDauList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Thongtintrandaus).Returns(mock.Object);
            _repo = new ThongTinTranDauRepository(_dbContextMock.Object);
            await _repo.GetAll();
            // Arrange
            var _thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>(options => options.Wrapping(new ThongTinTranDauRepository(A.Fake<FootBallManagerV2Context>())));

            A.CallTo(() => _thongTinTranDauRepos.GetAll()).Throws<Exception>(); // Simulate an exception
            var controller = new ThongtintrandausController(_thongTinTranDauRepos);

            // Act
            var result = await controller.GetThongtintrandaus();
            var objectResult = (ObjectResult)result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion



        #region GetThongtintrandau(int idTranDau)
        [TestMethod]
        public async Task ThongTinTranDausController_GetThongtintrandauById_ReturnOK()
        {
            //Arrange
            var _thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>(options => options.Wrapping(new ThongTinTranDauRepository(A.Fake<FootBallManagerV2Context>())));

            ThongTinTranDauRepository res = new ThongTinTranDauRepository(A.Fake<FootBallManagerV2Context>());
            FootBallTeamJoin fb= new FootBallTeamJoin() { 
                GIATRI=0,
                HINHANH=null,
                ID="atm",
                IDQUOCTICH=2,
                NGAYTHANHLAP=DateTime.Now,
                SANNHA="",
                SODOCHIENTHUAT="",
                SOLUONGTHANHVIEN=0,
                TEN="",
                THANHPHO = 1
            };
            List<Thongtintrandau> list = await _repo.GetById(1);

            A.CallTo(() => _thongTinTranDauRepos.GetById(1)).Returns(list);

            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.GetThongtintrandau(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task ThongTinTranDausController_GetThongtintrandauById_ReturnNotFound()
        {
            //Arrange
            var _thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>(options => options.Wrapping(new ThongTinTranDauRepository(A.Fake<FootBallManagerV2Context>())));

            A.CallTo(() => _thongTinTranDauRepos.GetById(1)).Returns<List<Thongtintrandau>>(null);

            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.GetThongtintrandau(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task ThongTinTranDausController_GetThongtintrandauById_ReturnsProblemResultOnException()
        {
            //Arrange
            var _thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>(options => options.Wrapping(new ThongTinTranDauRepository(A.Fake<FootBallManagerV2Context>())));

            A.CallTo(() => _thongTinTranDauRepos.GetById(1)).Throws<Exception>();

            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.GetThongtintrandau(1);

            //Assert
            var objectResult = (ObjectResult)result.Result;
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region UpdateThongtintrandau(int id,  Thongtintrandau thongtintrandau)
        [TestMethod]
        public async Task ThongTinTranDausController_UpdateThongtintrandau_ReturnNoContent()
        {
            //Arrange
            //List<Thongtintrandau> thongtintrandaus = await _thongTinTranDauRepos.GetById(0);
            Thongtintrandau thongtintrandau = A.Fake<Thongtintrandau>();
            thongtintrandau.Id = 2;
            thongtintrandau.Thedo = 2;
            await _repo.Update(thongtintrandau);
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.UpdateThongtintrandau(thongtintrandau.Id, thongtintrandau);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_UpdateThongtintrandau_ReturnBadRequest()
        {
            //Arrange
            List<Thongtintrandau> thongtintrandaus = await _thongTinTranDauRepos.GetById(0);
            Thongtintrandau thongtintrandau = A.Fake<Thongtintrandau>();
            thongtintrandau.Thedo = 2;
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.UpdateThongtintrandau(2, thongtintrandau);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task ThongTinTranDausController_UpdateThongtintrandau_ReturnNotFound()
        {
            //Arrange
            List<Thongtintrandau> thongtintrandaus = await _thongTinTranDauRepos.GetById(0);
            Thongtintrandau thongtintrandau = A.Fake<Thongtintrandau>();
            thongtintrandau.Id = 2;
            thongtintrandau.Thedo = 2;
            A.CallTo(() => _thongTinTranDauRepos.GetById(2)).Throws<Exception>();
            A.CallTo(() => _thongTinTranDauRepos.Update(thongtintrandau)).Throws<DbUpdateConcurrencyException>();
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);

            //Act
            var result = await _controller.UpdateThongtintrandau(2, thongtintrandau);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_UpdateThongtintrandau_ReturnsProblemResultOnException()
        {
            //Arrange
            List<Thongtintrandau> thongtintrandaus = await _thongTinTranDauRepos.GetById(0);
            Thongtintrandau thongtintrandau =A.Fake<Thongtintrandau>();
            thongtintrandau.Id = 2;
            thongtintrandau.Thedo = 2;

            A.CallTo(() => _thongTinTranDauRepos.GetById(2)).Returns(thongtintrandaus);
            A.CallTo(() => _thongTinTranDauRepos.Update(thongtintrandau)).Throws<DbUpdateConcurrencyException>();
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);

            //Act
            var result = await _controller.UpdateThongtintrandau(2, thongtintrandau);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task ThongTinTranDausController_UpdateThongtintrandau_ReturnsProblemResultOnNullObject()
        {
            //Arrange


            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);

            //Act
            var result = await _controller.UpdateThongtintrandau(2, null);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region PatchThongtintrandau(int id, JsonPatchDocument thongTinTranDauModel)
        [TestMethod]
        public async Task ThongTinTranDausController_PatchThongTinTranDau_ReturnNoContent()
        {
            //Arrange
            var _thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>(options => options.Wrapping(new ThongTinTranDauRepository(A.Fake<FootBallManagerV2Context>())));

            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            await _repo.Patch(5, update); 
            
            update.Replace("THEDO", 3);
            A.CallTo(() => _thongTinTranDauRepos.Patch(5, update)).Returns(true);
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.PatchThongtintrandau(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_PatchThongTinTranDau_ReturnBadRequest()
        {
            //Arrange
            var _thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>(options => options.Wrapping(new ThongTinTranDauRepository(A.Fake<FootBallManagerV2Context>())));

            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            ThongTinTranDauRepository _repo;
            var mock = GetFakeThongTinTranDauList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Thongtintrandaus).Returns(mock.Object);
            _repo = new ThongTinTranDauRepository(_dbContextMock.Object);
            await _repo.Patch(5, update);
            update.Replace("THEDO", 3);

            
            A.CallTo(() => _thongTinTranDauRepos.Patch(5, update)).Returns(false);
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.PatchThongtintrandau(5, update);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_PatchThongTinTranDau_ReturnsProblemResultOnException()
        {
            //Arrange
            var _thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>(options => options.Wrapping(new ThongTinTranDauRepository(A.Fake<FootBallManagerV2Context>())));

            Microsoft.AspNetCore.JsonPatch.JsonPatchDocument update = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument();
            update.Replace("THEDO", 3);
            A.CallTo(() => _thongTinTranDauRepos.Patch(5, update)).Throws<Exception>();
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.PatchThongtintrandau(5, update);
            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        #endregion


        #region CreateNewThongtintrandau(Thongtintrandau thongtintrandau)
        [TestMethod]
        public async Task ThongTinTranDausController_CreateNewThongTinTranDau_ReturnOK()
        {
            //Arrange
            var _thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>(options => options.Wrapping(new ThongTinTranDauRepository(A.Fake<FootBallManagerV2Context>())));

            Thongtintrandau thongtintrandau = new Thongtintrandau()
            {
                Id = 1,
                Diem = 3,
                Iddoibong = "atm",
                IddoibongNavigation = null,
                Idtrandau = 1,
                IdtrandauNavigation = null,
                Items = null,
                Ketqua = 1,
                Thedo = 1,
                Thevang = 1
            };
            A.CallTo(() => _thongTinTranDauRepos.Create(thongtintrandau)).Returns(await _repo.Create(thongtintrandau));
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.CreateNewThongtintrandau(thongtintrandau);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_CreateNewThongTinTranDau_ReturnsProblemResultOnException()
        {
            //Arrange
            var _thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>(options => options.Wrapping(new ThongTinTranDauRepository(A.Fake<FootBallManagerV2Context>())));

            Thongtintrandau thongtintrandau = A.Fake<Thongtintrandau>();
            A.CallTo(() => _thongTinTranDauRepos.Create(thongtintrandau)).Throws<Exception>();
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.CreateNewThongtintrandau(thongtintrandau);
            var objectResult = (ObjectResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion



        #region DeleteThongtintrandau(int id)
        [TestMethod]
        public async Task ThongTinTranDausController_DeleteThongtintrandau_ReturnNoContent()
        {
            //Arrange
            var _thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>(options => options.Wrapping(new ThongTinTranDauRepository(A.Fake<FootBallManagerV2Context>())));

            int idThongTinTranDau = 1;
            await _repo.Delete(idThongTinTranDau);
            A.CallTo(() => _thongTinTranDauRepos.Delete(idThongTinTranDau)).Returns(true);
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.DeleteThongtintrandau(idThongTinTranDau);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_DeleteThongtintrandau_ReturnNotFound()
        {
            //Arrange
            var _thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>(options => options.Wrapping(new ThongTinTranDauRepository(A.Fake<FootBallManagerV2Context>())));

            int idThongTinTranDau = 1;

            //context
            Mock<FootBallManagerV2Context> _dbContextMock;
            ThongTinTranDauRepository _repo;
            var mock = GetFakeThongTinTranDauList(true).BuildMock().BuildMockDbSet();
            _dbContextMock = new Mock<FootBallManagerV2Context>();
            _dbContextMock.Setup(x => x.Thongtintrandaus).Returns(mock.Object);
            _repo = new ThongTinTranDauRepository(_dbContextMock.Object);
            await _repo.Delete(idThongTinTranDau);
            A.CallTo(() => _thongTinTranDauRepos.Delete(idThongTinTranDau)).Returns(false);

            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.DeleteThongtintrandau(idThongTinTranDau);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ThongTinTranDausController_DeleteThongtintrandau_ReturnsProblemResultOnException()
        {
            //Arrange
            var _thongTinTranDauRepos = A.Fake<IThongTinTranDauRepository>(options => options.Wrapping(new ThongTinTranDauRepository(A.Fake<FootBallManagerV2Context>())));

            int idThongTinTranDau = 1;
            A.CallTo(() => _thongTinTranDauRepos.Delete(idThongTinTranDau)).Throws<Exception>();
            var _controller = new ThongtintrandausController(_thongTinTranDauRepos);
            //Act
            var result = await _controller.DeleteThongtintrandau(idThongTinTranDau);

            var objectResult = (ObjectResult)result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        }
        #endregion
    }
}
