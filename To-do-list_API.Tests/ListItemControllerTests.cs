using Microsoft.AspNetCore.Mvc;
using Moq;
using To_do_list_API.Controllers;
using To_do_list_Application.Interfaces;
using To_do_list_Core.Entities;
using Xunit;

namespace To_do_list_API.Tests
{
    public class ListItemControllerTests
    {
        private readonly Mock<IListItemService> _mockService;
        private readonly ListItemController _listItemController;

        public ListItemControllerTests()
        {
            _mockService = new Mock<IListItemService>();
            _listItemController = new ListItemController(_mockService.Object);
        }

        [Fact]
        public async Task GetById_ReturnItems()
        {
            //Arrange
            var items = new ListItem { listItemId = 1, listId = 1, title = "title 1", description = "description 1" };

            _mockService.Setup(service => service.GetItemByIdAsync(1)).ReturnsAsync(items);

            //Act
            var result = await _listItemController.GetById(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedItems = Assert.IsType<ListItem>(okResult.Value);
            Assert.True(returnedItems.listItemId == 1);
        }

        [Fact]
        public async Task GetItemsByListId_ReturnListItems()
        {
            //Arrange
            var items = new List<ListItem>{
                new ListItem { listItemId = 2, listId = 2, title = "title 2", description = "description 2" },
                new ListItem { listItemId = 3, listId = 2, title = "title 3", description = "description 3" }
            };

            _mockService.Setup(service => service.GetItemsByListIdAsync(2)).ReturnsAsync(items);

            //Act
            var result = await _listItemController.GetItemsByListId(2);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedItems = Assert.IsType<List<ListItem>>(okResult.Value);
            Assert.Equal(2, returnedItems.Count);
        }

        [Fact]
        public async Task AddItem_ReturnCreatedAtActionResult()
        {
            //Arrange
            var newItems = new ListItem { listItemId = 5, title = "New Task" };

            //Act
            var result = await _listItemController.AddItem(newItems);

            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetById", createdResult.ActionName);
            Assert.Equal(newItems.listItemId, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateItem_ReturnNoContent()
        {
            //Arrange
            var updatedItem = new ListItem { listItemId = 5, title = "Updated Task" };

            //Act
            var result = await _listItemController.UpdateItem(5 ,updatedItem);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateItem_ReturnBadRequestWhenIdNotMatch()
        {
            //Arrange
            var updatedItem = new ListItem { listItemId = 5, title = "Updated Task" };

            //Act
            var result = await _listItemController.UpdateItem(4, updatedItem);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task UpdateItemStatus_ReturnNoContent()
        {
            //Arrange
            int updatedItemId = 1;

            //Act
            var result = await _listItemController.UpdateItemStatus(updatedItemId);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteItem_SuccessReturnNoContent()
        {
            //Arrange
            int deleteItem = 1;

            //Act
            var result = await _listItemController.DeleteItem(deleteItem);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
