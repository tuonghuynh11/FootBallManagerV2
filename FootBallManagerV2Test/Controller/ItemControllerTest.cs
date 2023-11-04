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
    public class ItemControllerTest
    {
        public readonly IItemRepository _itemRepo;
        public ItemControllerTest()
        {
            this._itemRepo = A.Fake<IItemRepository>();
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
            var Items = A.Fake<List<Item>>();
            A.CallTo(() => _itemRepo.GetAllItemsAsync()).Returns(Items);
            var controller = new ItemsController(_itemRepo);
            var result = await controller.GetItems();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllMethodFail()
        {
            A.CallTo(() => _itemRepo.GetAllItemsAsync()).Throws<Exception>();
            var controller = new ItemsController(_itemRepo);

            var result = await controller.GetItems();
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task GetMethod()
        {
            var Item = A.Fake<Item>();
            A.CallTo(() => _itemRepo.GetItemAsync(1)).Returns(Item);

            var controller = new ItemsController(_itemRepo);
            var result = await controller.GetItem(1);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetMethodNull()
        {
            var Item = A.Fake<Item>();
            Item = null;
            A.CallTo(() => _itemRepo.GetItemAsync(1)).Returns(Item);

            var controller = new ItemsController(_itemRepo);
            var result = await controller.GetItem(1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMethodFail()
        {
            A.CallTo(() => _itemRepo.GetItemAsync(1)).Throws<Exception>();
            var controller = new ItemsController(_itemRepo);

            var result = await controller.GetItem(1);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Put()
        {
            var Item = new Item();
            Item.Id = 1;
            Item.Idthongtintrandau = 1;
            Item.Iddoibong = "abc";
            Item.Iditemtype = 1;
            Item.Thoigian = "1-1-2024";
            Item.Idcauthu = 3;
            Item.IdcauthuNavigation = new Cauthu();
            Item.IddoibongNavigation = new Doibong();
            Item.IditemtypeNavigation = new Itemtype();
            Item.IdthongtintrandauNavigation = new Thongtintrandau();
            A.CallTo(() => _itemRepo.updateItemAsync(1, Item)).Invokes(() => {
                Assert.AreEqual(Item.Id, 1);
            });
            var controller = new ItemsController(_itemRepo);

            var result = await controller.PutItem(1, Item);
            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task PutNotFound()
        {
            var Item = new Item();
            Item.Id = 2;
            A.CallTo(() => _itemRepo.updateItemAsync(1, Item)).Invokes(() => {
                Assert.AreEqual(Item.Id, 1);
            });
            var controller = new ItemsController(_itemRepo);

            var result = await controller.PutItem(1, Item);
            var objectResult = (NotFoundResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutFail()
        {
            var Item = new Item();
            Item.Id = 1;
            A.CallTo(() => _itemRepo.updateItemAsync(1, Item)).Throws<Exception>();
            var controller = new ItemsController(_itemRepo);

            var result = await controller.PutItem(1, Item);
            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post()
        {
            var Item = new Item();
            Item.Id = 1;
            A.CallTo(() => _itemRepo.addItemAsync(Item)).Returns(Item);
            var controller = new ItemsController(_itemRepo);

            var result = await controller.PostItem(Item);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }

        [TestMethod]
        public async Task PostNull()
        {
            var Item = new Item();
            Item = null;
            A.CallTo(() => _itemRepo.addItemAsync(Item)).Returns(Item);
            var controller = new ItemsController(_itemRepo);

            var result = await controller.PostItem(Item);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult);
        }

        [TestMethod]
        public async Task PostFail()
        {
            var Item = new Item();
            Item.Id = 1;
            A.CallTo(() => _itemRepo.addItemAsync(Item)).Throws<Exception>();
            var controller = new ItemsController(_itemRepo);

            var result = await controller.PostItem(Item);
            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }

        [TestMethod]
        public async Task Delete()
        {
            int id = 1;
            A.CallTo(() => _itemRepo.deleteItemAsync(id)).Invokes(() => {
                Assert.AreEqual(id, 1);
            });
            var controller = new ItemsController(_itemRepo);
            var result = await controller.DeleteItem(id);

            var objectResult = (OkResult)result;

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            int id = 1;
            A.CallTo(() => _itemRepo.deleteItemAsync(id)).Throws<Exception>();
            var controller = new ItemsController(_itemRepo);
            var result = await controller.DeleteItem(id);

            var objectResult = (BadRequestResult)result;

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
