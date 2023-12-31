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
    public class FieldControllerTest
    {
        public readonly IFieldRepository _fieldRepo;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly FieldRepository _repo;
        public FieldControllerTest()
        {
            this._fieldRepo = A.Fake<IFieldRepository>();
            var mock = GetFakeFieldList().BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Fields).Returns(mock.Object);
            this._repo = new FieldRepository(this._dbContextMock.Object);
        }

        private static List<Field> GetFakeFieldList()
        {
            return new List<Field>() {
                new Field() {
                   IdField = 1,
                   IdDiaDiem = 1,
                   Images = new byte[10],
                   FieldName = "Stanford Bridge",
                   TechnicalInformation = "abc",
                   NumOfSeats = 20000,
                   Status = 1,
                   Fieldservices = new List<Fieldservice>(),
                   Footballmatches = new List<Footballmatch>(),
                   IdDiaDiemNavigation = new Diadiem(),

        },
                new Field() {
                   IdField = 2,
                   IdDiaDiem = 1,
                   Images = new byte[10],
                   FieldName = "Etihad",
                   TechnicalInformation = "abc",
                   NumOfSeats = 20000,
                   Status = 1,
                   Fieldservices = new List<Fieldservice>(),
                   Footballmatches = new List<Footballmatch>(),
                   IdDiaDiemNavigation = new Diadiem(),

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
            FieldRepository res = new FieldRepository(A.Fake<FootBallManagerV2Context>());

            var Fields = await _repo.GetAllFieldsAsync();
            A.CallTo(() => _fieldRepo.GetAllFieldsAsync()).Returns(Fields);
            var controller = new FieldsController(_fieldRepo);
            var result = await controller.GetFields();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _fieldRepo.GetAllFieldsAsync()).Throws<Exception>();
            var controller = new FieldsController(_fieldRepo);

            var result = await controller.GetFields();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var Field = A.Fake<Field>();
            A.CallTo(() => _fieldRepo.GetFieldAsync(1)).Returns(Field);

            var controller = new FieldsController(_fieldRepo);
            var result = await controller.GetField(1);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var Field = A.Fake<Field>();
            Field = null;
            A.CallTo(() => _fieldRepo.GetFieldAsync(1)).Returns(Field);

            var controller = new FieldsController(_fieldRepo);
            var result = await controller.GetField(1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _fieldRepo.GetFieldAsync(1)).Throws<Exception>();
            var controller = new FieldsController(_fieldRepo);

            var result = await controller.GetField(1);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var Field = new Field();
            Field.IdField = 1;
            Field.IdDiaDiem = 1;
            Field.Images = new byte[10];
            Field.FieldName = "Stanford Bridge";
            Field.TechnicalInformation = "abc";
            Field.NumOfSeats = 20000;
            Field.Status = 1;
            Field.Fieldservices = new List<Fieldservice>();
            Field.Footballmatches = new List<Footballmatch>();
            Field.IdDiaDiemNavigation = new Diadiem();
            await _repo.updateFieldAsync(1, Field);
            A.CallTo(() => _fieldRepo.updateFieldAsync(1, Field)).Invokes(() => {
                Assert.AreEqual(Field.IdField, 1);
            });
            var controller = new FieldsController(_fieldRepo);

            var result = await controller.PutField(1, Field);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var Field = new Field();
            Field.IdField = 2;
            A.CallTo(() => _fieldRepo.updateFieldAsync(1, Field)).Invokes(() => {
                Assert.AreEqual(Field.IdField, 1);
            });
            var controller = new FieldsController(_fieldRepo);

            var result = await controller.PutField(1, Field);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var Field = new Field();
            Field.IdField = 1;
            A.CallTo(() => _fieldRepo.updateFieldAsync(1, Field)).Throws<Exception>();
            var controller = new FieldsController(_fieldRepo);

            var result = await controller.PutField(1, Field);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var newField = new Field();
            newField.IdField = 1;
            var Field = await _repo.addFieldAsync(newField);
            A.CallTo(() => _fieldRepo.addFieldAsync(Field)).Returns(Field);
            var controller = new FieldsController(_fieldRepo);

            var result = await controller.PostField(Field);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var Field = new Field();
            Field = null;
            A.CallTo(() => _fieldRepo.addFieldAsync(Field)).Returns(Field);
            var controller = new FieldsController(_fieldRepo);

            var result = await controller.PostField(Field);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var Field = new Field();
            Field.IdField = 1;
            A.CallTo(() => _fieldRepo.addFieldAsync(Field)).Throws<Exception>();
            var controller = new FieldsController(_fieldRepo);

            var result = await controller.PostField(Field);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            int id = 1;
            await _repo.deleteFieldAsync(id);
            A.CallTo(() => _fieldRepo.deleteFieldAsync(id)).Invokes(() => {
                Assert.AreEqual(id, 1);
            }); ;
            var controller = new FieldsController(_fieldRepo);
            var result = await controller.DeleteField(id);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            int id = 1;
            A.CallTo(() => _fieldRepo.deleteFieldAsync(id)).Throws<Exception>();
            var controller = new FieldsController(_fieldRepo);
            var result = await controller.DeleteField(id);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
