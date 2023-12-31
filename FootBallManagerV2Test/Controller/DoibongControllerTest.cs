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
using Moq;
using MockQueryable.Moq;

namespace FootBallManagerV2Test.Controller
{
    [TestClass]
    public class DoibongControllerTest
    {
        public readonly IDoibongRepository _doibongRepo;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly DoibongRepository _repo;
        public DoibongControllerTest()
        {
            this._doibongRepo = A.Fake<IDoibongRepository>();
            var mock = GetFakeDoiBongList().BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Doibongs).Returns(mock.Object);
            this._repo = new DoibongRepository(this._dbContextMock.Object);
        }
        private static List<Doibong> GetFakeDoiBongList()
        {
            return new List<Doibong>() {
                new Doibong() {
                     Id = "abc",
                    Idquoctich = 2,
                    Thanhpho = 2,
                    Hinhanh = new byte[10],
                    Ten = "Man City",
                    Soluongthanhvien = 30,
                    Ngaythanhlap = new DateTime(1999, 1, 1),
                    Sannha = "Emirates",
                    Sodochienthuat = "4-3-3",
                    Giatri = 234300030003,
                    Cauthus = new List<Cauthu>(),
                    Chuyennhuongs = new List<Chuyennhuong>(),
                    Diems = new List<Diem>(),
                    Doibongsuppliers = new List<Doibongsupplier>(),
                    Doihinhchinhs = new List<Doihinhchinh>(),
                    Huanluyenviens = new List<Huanluyenvien>(),
                    IdquoctichNavigation = new Quoctich(),
                    Items = new List<Item>(),
                    Tapluyens = new List<Tapluyen>(),
                    Teamofleagues = new List<Teamofleague>(),
                    ThanhphoNavigation = new Diadiem(),
                    Thongtingiaidaus = new List<Thongtingiaidau>(),
                    Thongtintrandaus = new List<Thongtintrandau>()

        },
               new Doibong() {
                     Id = "rea",
                    Idquoctich = 2,
                    Thanhpho = 2,
                    Hinhanh = new byte[10],
                    Ten = "Real Marid",
                    Soluongthanhvien = 30,
                    Ngaythanhlap = new DateTime(1999, 1, 1),
                    Sannha = "Emirates",
                    Sodochienthuat = "4-3-3",
                    Giatri = 234300030003,
                    Cauthus = new List<Cauthu>(),
                    Chuyennhuongs = new List<Chuyennhuong>(),
                    Diems = new List<Diem>(),
                    Doibongsuppliers = new List<Doibongsupplier>(),
                    Doihinhchinhs = new List<Doihinhchinh>(),
                    Huanluyenviens = new List<Huanluyenvien>(),
                    IdquoctichNavigation = new Quoctich(),
                    Items = new List<Item>(),
                    Tapluyens = new List<Tapluyen>(),
                    Teamofleagues = new List<Teamofleague>(),
                    ThanhphoNavigation = new Diadiem(),
                    Thongtingiaidaus = new List<Thongtingiaidau>(),
                    Thongtintrandaus = new List<Thongtintrandau>()

        },


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
            DoibongRepository res = new DoibongRepository(A.Fake<FootBallManagerV2Context>());

            var Doibongs = await _repo.GetAllDoibongAsync();
            A.CallTo(() => _doibongRepo.GetAllDoibongAsync()).Returns(Doibongs);
            var controller = new DoibongsController(_doibongRepo);
            var result = await controller.GetDoibongs();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _doibongRepo.GetAllDoibongAsync()).Throws<Exception>();
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.GetDoibongs();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var Doibong = A.Fake<Doibong>();
            A.CallTo(() => _doibongRepo.GetDoibongAsync("abc")).Returns(Doibong);

            var controller = new DoibongsController(_doibongRepo);
            var result = await controller.GetDoibong("abc");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var Doibong = A.Fake<Doibong>();
            Doibong = null;
            A.CallTo(() => _doibongRepo.GetDoibongAsync("abc")).Returns(Doibong);

            var controller = new DoibongsController(_doibongRepo);
            var result = await controller.GetDoibong("abc");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _doibongRepo.GetDoibongAsync("abc")).Throws<Exception>();
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.GetDoibong("abc");
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var Doibong = new Doibong();
            Doibong.Id = "abc";
            Doibong.Idquoctich = 2;
            Doibong.Thanhpho = 2;
            Doibong.Hinhanh = new byte[10];
            Doibong.Ten = "Man City";
            Doibong.Soluongthanhvien = 30;
            Doibong.Ngaythanhlap = new DateTime(1999, 1, 1);
            Doibong.Sannha = "Emirates";
            Doibong.Sodochienthuat = "4-3-3";
            Doibong.Giatri = 234300030003;
            Doibong.Cauthus = new List<Cauthu>();
            Doibong.Chuyennhuongs = new List<Chuyennhuong>();
            Doibong.Diems = new List<Diem>();
            Doibong.Doibongsuppliers = new List<Doibongsupplier>();
            Doibong.Doihinhchinhs = new List<Doihinhchinh>();
            Doibong.Huanluyenviens = new List<Huanluyenvien>();
            Doibong.IdquoctichNavigation = new Quoctich();
            Doibong.Items = new List<Item>();
            Doibong.Tapluyens = new List<Tapluyen>();
            Doibong.Teamofleagues = new List<Teamofleague>();
            Doibong.ThanhphoNavigation = new Diadiem();
            Doibong.Thongtingiaidaus = new List<Thongtingiaidau>();
            Doibong.Thongtintrandaus = new List<Thongtintrandau>();
            await _repo.updateDoibongAsync("abc", Doibong);
            A.CallTo(() => _doibongRepo.updateDoibongAsync("abc", Doibong)).Invokes(() => {
                Assert.AreEqual(Doibong.Id, "abc");
            });
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.PutDoibong("abc", Doibong);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var Doibong = new Doibong();
            Doibong.Id = "abcd";
            A.CallTo(() => _doibongRepo.updateDoibongAsync("abc", Doibong)).Invokes(() => {
                Assert.AreEqual(Doibong.Id, 1);
            });
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.PutDoibong("abc", Doibong);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var Doibong = new Doibong();
            Doibong.Id = "abc";
            A.CallTo(() => _doibongRepo.updateDoibongAsync("abc", Doibong)).Throws<Exception>();
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.PutDoibong("abc", Doibong);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var newDoiBong = new Doibong();
            newDoiBong.Id = "abc";
            var Doibong = await _repo.addDoibongAsync(newDoiBong);
            A.CallTo(() => _doibongRepo.addDoibongAsync(Doibong)).Returns(Doibong);
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.PostDoibong(Doibong);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var Doibong = new Doibong();
            Doibong = null;
            A.CallTo(() => _doibongRepo.addDoibongAsync(Doibong)).Returns(Doibong);
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.PostDoibong(Doibong);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var Doibong = new Doibong();
            Doibong.Id = "abc";
            A.CallTo(() => _doibongRepo.addDoibongAsync(Doibong)).Throws<Exception>();
            var controller = new DoibongsController(_doibongRepo);

            var result = await controller.PostDoibong(Doibong);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            string id = "abc";
            await _repo.deleteDoibongAsync(id);
            A.CallTo(() => _doibongRepo.deleteDoibongAsync(id)).Invokes(() => {
                Assert.AreEqual(id, "abc");
            }); ;
            var controller = new DoibongsController(_doibongRepo);
            var result = await controller.DeleteDoibong(id);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            string id = "abc";
            A.CallTo(() => _doibongRepo.deleteDoibongAsync(id)).Throws<Exception>();
            var controller = new DoibongsController(_doibongRepo);
            var result = await controller.DeleteDoibong(id);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
