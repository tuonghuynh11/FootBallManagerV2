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
    public class ItemtypeControllerTest
    {
        public readonly IItemtypeRepository _itemtypeRepo;
        private readonly Mock<FootBallManagerV2Context> _dbContextMock;
        private readonly ItemtypeRepository _repo;
        public ItemtypeControllerTest()
        {
            this._itemtypeRepo = A.Fake<IItemtypeRepository>();
            var mock = GetFakeItemTypeList().BuildMock().BuildMockDbSet();
            this._dbContextMock = new Mock<FootBallManagerV2Context>();
            this._dbContextMock.Setup(x => x.Itemtypes).Returns(mock.Object);
            this._repo = new ItemtypeRepository(this._dbContextMock.Object);
        }
        private static List<Itemtype> GetFakeItemTypeList()
        {
            return new List<Itemtype>() {
                new Itemtype() {
                     Id = 1,
                     Tenitem = "shoes",
                     Items = new List<Item>(),

                },
                new Itemtype() {
                     Id = 2,
                     Tenitem = "beverage",
                     Items = new List<Item>(),

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
            ItemtypeRepository res = new ItemtypeRepository(A.Fake<FootBallManagerV2Context>());

            var Itemtypes = await _repo.GetAllItemtypesAsync();
            A.CallTo(() => _itemtypeRepo.GetAllItemtypesAsync()).Returns(Itemtypes);
            var controller = new ItemtypesController(_itemtypeRepo);
            var result = await controller.GetItemtypes();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _itemtypeRepo.GetAllItemtypesAsync()).Throws<Exception>();
            var controller = new ItemtypesController(_itemtypeRepo);

            var result = await controller.GetItemtypes();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var Itemtype = A.Fake<Itemtype>();
            A.CallTo(() => _itemtypeRepo.GetItemtypeAsync(1)).Returns(Itemtype);

            var controller = new ItemtypesController(_itemtypeRepo);
            var result = await controller.GetItemtype(1);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var Itemtype = A.Fake<Itemtype>();
            Itemtype = null;
            A.CallTo(() => _itemtypeRepo.GetItemtypeAsync(1)).Returns(Itemtype);

            var controller = new ItemtypesController(_itemtypeRepo);
            var result = await controller.GetItemtype(1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _itemtypeRepo.GetItemtypeAsync(1)).Throws<Exception>();
            var controller = new ItemtypesController(_itemtypeRepo);

            var result = await controller.GetItemtype(1);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var Itemtype = new Itemtype();
            Itemtype.Id = 1;
            Itemtype.Tenitem = "shoes";
            Itemtype.Items = new List<Item>();
            await _repo.updateItemtypeAsync(1, Itemtype);
            A.CallTo(() => _itemtypeRepo.updateItemtypeAsync(1, Itemtype)).Invokes(() => {
                Assert.AreEqual(Itemtype.Id, 1);
            });
            var controller = new ItemtypesController(_itemtypeRepo);

            var result = await controller.PutItemtype(1, Itemtype);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var Itemtype = new Itemtype();
            Itemtype.Id = 2;
            A.CallTo(() => _itemtypeRepo.updateItemtypeAsync(1, Itemtype)).Invokes(() => {
                Assert.AreEqual(Itemtype.Id, 1);
            });
            var controller = new ItemtypesController(_itemtypeRepo);

            var result = await controller.PutItemtype(1, Itemtype);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var Itemtype = new Itemtype();
            Itemtype.Id = 1;
            A.CallTo(() => _itemtypeRepo.updateItemtypeAsync(1, Itemtype)).Throws<Exception>();
            var controller = new ItemtypesController(_itemtypeRepo);

            var result = await controller.PutItemtype(1, Itemtype);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var newItemType = new Itemtype();
            newItemType.Id = 1;
            var Itemtype = await _repo.addItemtypeAsync(newItemType);

            A.CallTo(() => _itemtypeRepo.addItemtypeAsync(Itemtype)).Returns(Itemtype);
            var controller = new ItemtypesController(_itemtypeRepo);

            var result = await controller.PostItemtype(Itemtype);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var Itemtype = new Itemtype();
            Itemtype = null;
            A.CallTo(() => _itemtypeRepo.addItemtypeAsync(Itemtype)).Returns(Itemtype);
            var controller = new ItemtypesController(_itemtypeRepo);

            var result = await controller.PostItemtype(Itemtype);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var Itemtype = new Itemtype();
            Itemtype.Id = 1;
            A.CallTo(() => _itemtypeRepo.addItemtypeAsync(Itemtype)).Throws<Exception>();
            var controller = new ItemtypesController(_itemtypeRepo);

            var result = await controller.PostItemtype(Itemtype);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            int id = 1;
            await _repo.deleteItemtypeAsync(id);
            A.CallTo(() => _itemtypeRepo.deleteItemtypeAsync(id)).Invokes(() => {
                Assert.AreEqual(id, 1);
            });
            var controller = new ItemtypesController(_itemtypeRepo);
            var result = await controller.DeleteItemtype(id);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            int id = 1;
            A.CallTo(() => _itemtypeRepo.deleteItemtypeAsync(id)).Throws<Exception>();
            var controller = new ItemtypesController(_itemtypeRepo);
            var result = await controller.DeleteItemtype(id);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
