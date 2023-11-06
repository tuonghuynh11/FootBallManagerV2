using FakeItEasy;
using FootBallManagerAPI.Controllers;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Repositories;
using FootBallManagerAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockQueryable.Moq;
using Moq.EntityFrameworkCore;
namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class CauthuControllerTest
    {
        private readonly ICauthuRepository _cauthuRepo;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly CauthuRepository _repo;
        public CauthuControllerTest()
        {
            //this._cauthuRepo = A.Fake<ICauthuRepository>();
            this._cauthuRepo = A.Fake<ICauthuRepository>(options => options.Wrapping(new CauthuRepository(A.Fake<FootBallManagerV2Context>())));
            var mock = GetFakeCauThuList().BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Cauthus).Returns(mock.Object);
            this._repo = new CauthuRepository(this._dbContextMock.Object);
        }
        private static List<Cauthu> GetFakeCauThuList()
        {
            return new List<Cauthu>() {
                new Cauthu() {
                    Id=1,
                    Cannang="72",
                    Chanthuan="Trai",
                    Chieucao="180",
                    Chuyennhuongs=null,
                    Doihinhchinhs=null,
                    Giatricauthu=200000000,
                    Hinhanh=null,
                    Hoten="Nguyen Van A",
                    Iddoibong="atm",
                    IddoibongNavigation=null,
                    Idquoctich=1,
                    IdquoctichNavigation=null,
                    Items=null,
                    Soao=12,
                    Sobanthang=12,
                    Sogiai=2,
                    Thamgia=null,
                     Thetrang="Tot",
                     Tuoi=25,
                     Vitri="ST"
                    
                },
                new Cauthu() {
                    Id=2,
                    Cannang="72",
                    Chanthuan="Trai",
                    Chieucao="180",
                    Chuyennhuongs=null,
                    Doihinhchinhs=null,
                    Giatricauthu=200000000,
                    Hinhanh=null,
                    Hoten="Nguyen Van B",
                    Iddoibong="atm",
                    IddoibongNavigation=null,
                    Idquoctich=1,
                    IdquoctichNavigation=null,
                    Items=null,
                    Soao=12,
                    Sobanthang=12,
                    Sogiai=2,
                    Thamgia=null,
                     Thetrang="Tot",
                     Tuoi=25,
                     Vitri="ST"

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
            CauthuRepository res = new CauthuRepository(A.Fake<FootBallManagerV2Context>());

            var cauthus = await _repo.GetAllCauthuAsync();
            A.CallTo(() => _cauthuRepo.GetAllCauthuAsync()).Returns(cauthus);
            var controller = new CauthusController(_cauthuRepo);
            var result = await controller.GetCauthus();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _cauthuRepo.GetAllCauthuAsync()).Throws<Exception>();
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.GetCauthus();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof (BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var cauthu = A.Fake<Cauthu>();
            A.CallTo(() => _cauthuRepo.getCauthuByIdAsync(1)).Returns(cauthu);

            var controller = new CauthusController(_cauthuRepo);
            var result = await controller.GetCauthu(1);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var cauthu = A.Fake<Cauthu>();
            cauthu = null;
            A.CallTo(() => _cauthuRepo.getCauthuByIdAsync(1)).Returns(cauthu);

            var controller = new CauthusController(_cauthuRepo);
            var result = await controller.GetCauthu(1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _cauthuRepo.getCauthuByIdAsync(1)).Throws<Exception>();
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.GetCauthu(1);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var cauthu = new Cauthu();
            cauthu.Id = 1;
            cauthu.Iddoibong = "abc";
            cauthu.Idquoctich = 1;
            cauthu.Hoten = "Kevin";
            cauthu.Tuoi = 30;
            cauthu.Sogiai = 1;
            cauthu.Sobanthang = 20;
            cauthu.Hinhanh = new byte[10];
            cauthu.Chanthuan = "left";
            cauthu.Thetrang = "strong";
            cauthu.Vitri = "CM";
            cauthu.Soao = 11;
            cauthu.Chieucao = "183";
            cauthu.Cannang = "70";
            cauthu.Giatricauthu = 2300000000;
            cauthu.Chuyennhuongs = new List<Chuyennhuong>();
            cauthu.Doihinhchinhs = new List<Doihinhchinh>();
            cauthu.IddoibongNavigation = new Doibong();
            cauthu.IdquoctichNavigation = new Quoctich();
            cauthu.Items = new List<Item>();
            cauthu.Thamgia = new List<Thamgium>();
            await _repo.updateCauthuAsync(1, cauthu);
            A.CallTo(() => _cauthuRepo.updateCauthuAsync(1, cauthu)).Invokes(() => {
                Assert.AreEqual(cauthu.Id, 1);
            });
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.PutCauthu(1, cauthu);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var cauthu = new Cauthu();
            cauthu.Id = 2;
            A.CallTo(() => _cauthuRepo.updateCauthuAsync(1, cauthu)).Invokes(() => {
                Assert.AreEqual(cauthu.Id, 1);
            });
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.PutCauthu(1, cauthu);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var cauthu = new Cauthu(); 
            cauthu.Id = 1;
            A.CallTo(() => _cauthuRepo.updateCauthuAsync(1, cauthu)).Throws<Exception>();
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.PutCauthu(1, cauthu);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var newPlayer = new Cauthu();
            newPlayer.Id = 1;
            var cauthu = await _repo.addCauthuAsync(newPlayer);
           
            A.CallTo(() => _cauthuRepo.addCauthuAsync(cauthu)).Returns(cauthu);
            var controller = new CauthusController (_cauthuRepo);

            var result = await controller.PostCauthu(cauthu);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var cauthu = new Cauthu();
            cauthu = null;
            A.CallTo(() => _cauthuRepo.addCauthuAsync(cauthu)).Returns(cauthu);
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.PostCauthu(cauthu);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var cauthu = new Cauthu();
            cauthu.Id = 1;
            A.CallTo(() => _cauthuRepo.addCauthuAsync(cauthu)).Throws<Exception>();
            var controller = new CauthusController(_cauthuRepo);

            var result = await controller.PostCauthu(cauthu);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            int id = 1;
            await _repo.deleteCauthuAsync(id);
            A.CallTo(() => _cauthuRepo.deleteCauthuAsync(id)).Invokes(() => {
                Assert.AreEqual(id, 1);
            }); 
            var controller = new CauthusController(_cauthuRepo);
            var result = await controller.DeleteCauthu(id);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            int id = 1;
            A.CallTo(() => _cauthuRepo.deleteCauthuAsync(id)).Throws<Exception>();
            var controller = new CauthusController(_cauthuRepo);
            var result = await controller.DeleteCauthu(id);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
