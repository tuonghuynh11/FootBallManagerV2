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
    public class DiadiemControllerTest
    {
        private readonly IDiadiemRepository _diadiemRepo;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly DiadiemRepository _repo;
        public DiadiemControllerTest()
        {
            this._diadiemRepo = A.Fake<IDiadiemRepository>();
            var mock = GetFakeDiaDiemList().BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Diadiems).Returns(mock.Object);
            this._repo = new DiadiemRepository(this._dbContextMock.Object);
        }

        private static List<Diadiem> GetFakeDiaDiemList()
        {
            return new List<Diadiem>() {
                new Diadiem() {
                   Doibongs=null,
                   Fields=null,
                   Id=1,
                   Idquocgia=1,
                   IdquocgiaNavigation=null,
                   Tendiadiem="San A"
                   
                },
                new Diadiem() {
                   Doibongs=null,
                   Fields=null,
                   Id=2,
                   Idquocgia=2,
                   IdquocgiaNavigation=null,
                   Tendiadiem="San B"

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
            DiadiemRepository res = new DiadiemRepository(A.Fake<FootBallManagerV2Context>());

            var diadiems = await _repo.GetAllDiadiemAsync();
            A.CallTo(() => _diadiemRepo.GetAllDiadiemAsync()).Returns(diadiems);
            var controller = new DiadiemsController(_diadiemRepo);
            var result = await controller.GetDiadiems();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _diadiemRepo.GetAllDiadiemAsync()).Throws<Exception>();
            var controller = new DiadiemsController(_diadiemRepo);

            var result = await controller.GetDiadiems();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var diadiem = A.Fake<Diadiem>();
            A.CallTo(() => _diadiemRepo.GetDiadiemAsync(1)).Returns(diadiem);

            var controller = new DiadiemsController(_diadiemRepo);
            var result = await controller.GetDiadiem(1);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var diadiem = A.Fake<Diadiem>();
            diadiem = null;
            A.CallTo(() => _diadiemRepo.GetDiadiemAsync(1)).Returns(diadiem);

            var controller = new DiadiemsController(_diadiemRepo);
            var result = await controller.GetDiadiem(1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _diadiemRepo.GetDiadiemAsync(1)).Throws<Exception>();
            var controller = new DiadiemsController(_diadiemRepo);

            var result = await controller.GetDiadiem(1);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var diadiem = new Diadiem();
            diadiem.Id = 1;
            diadiem.Tendiadiem = "London";
            diadiem.Idquocgia = 2;
            diadiem.Doibongs = new List<Doibong>();
            diadiem.Fields = new List<Field>();
            diadiem.IdquocgiaNavigation = new Quoctich();
            await _repo.updateDiadiemAsync(1,diadiem);
            A.CallTo(() => _diadiemRepo.updateDiadiemAsync(1, diadiem)).Invokes(() => {
                Assert.AreEqual(diadiem.Id, 1);
            });
            var controller = new DiadiemsController(_diadiemRepo);

            var result = await controller.PutDiadiem(1, diadiem);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var diadiem = new Diadiem();
            diadiem.Id = 2;
            A.CallTo(() => _diadiemRepo.updateDiadiemAsync(1, diadiem)).Invokes(() => {
                Assert.AreEqual(diadiem.Id, 1);
            });
            var controller = new DiadiemsController(_diadiemRepo);

            var result = await controller.PutDiadiem(1, diadiem);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var diadiem = new Diadiem();
            diadiem.Id = 1;
            A.CallTo(() => _diadiemRepo.updateDiadiemAsync(1, diadiem)).Throws<Exception>();
            var controller = new DiadiemsController(_diadiemRepo);

            var result = await controller.PutDiadiem(1, diadiem);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var newDiaDiem = new Diadiem();
            newDiaDiem.Id = 1;
            var diadiem = await _repo.addDiadiemAsync(newDiaDiem);
            A.CallTo(() => _diadiemRepo.addDiadiemAsync(diadiem)).Returns(diadiem);
            var controller = new DiadiemsController(_diadiemRepo);

            var result = await controller.PostDiadiem(diadiem);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var diadiem = new Diadiem();
            diadiem = null;
            A.CallTo(() => _diadiemRepo.addDiadiemAsync(diadiem)).Returns(diadiem);
            var controller = new DiadiemsController(_diadiemRepo);

            var result = await controller.PostDiadiem(diadiem);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var diadiem = new Diadiem();
            diadiem.Id = 1;
            A.CallTo(() => _diadiemRepo.addDiadiemAsync(diadiem)).Throws<Exception>();
            var controller = new DiadiemsController(_diadiemRepo);

            var result = await controller.PostDiadiem(diadiem);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            int id = 1;
            await _repo.DeleteDiadiemAsync(id);
            A.CallTo(() => _diadiemRepo.DeleteDiadiemAsync(id)).Invokes(() => {
                Assert.AreEqual(id, 1);
            }); ;
            var controller = new DiadiemsController(_diadiemRepo);
            var result = await controller.DeleteDiadiem(id);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            int id = 1;
            A.CallTo(() => _diadiemRepo.DeleteDiadiemAsync(id)).Throws<Exception>();
            var controller = new DiadiemsController(_diadiemRepo);
            var result = await controller.DeleteDiadiem(id);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
