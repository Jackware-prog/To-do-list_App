using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_do_list_Application.Interfaces;
using To_do_list_Application.Services;
using To_do_list_Core.Entities;
using Xunit;

namespace To_do_list_Application.Tests
{
    public class ListItemServiceTests
    {
        private readonly Mock<IListItemRepository> _mockListItemRepository;
        private readonly ListItemService _ListItemService;

        public ListItemServiceTests()
        {
            _mockListItemRepository = new Mock<IListItemRepository>();
            _ListItemService = new ListItemService(_mockListItemRepository.Object);
        }

        [Fact]
        public async Task GetItemByIdAsync_ReturnsItems()
        {
            //Arrange
            var expectedResult = new ListItem { listItemId = 1, title = "task 1", description = "task 1 desp" };

            _mockListItemRepository.Setup(repo => repo.GetItemByIdAsync(1)).ReturnsAsync(expectedResult);

            //Act
            var result = await _ListItemService.GetItemByIdAsync(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ListItem>(result);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task GetItemsByListIdAsync_ReturnsItems()
        {
            //Arrange
            var expectedResult = new List<ListItem> 
            { 
                new ListItem{listItemId = 1, listId = 2, title = "task 1", description = "task 1 desp"},
                new ListItem{listItemId = 3, listId = 2, title = "task n", description = "task n desp"},
            };

            _mockListItemRepository.Setup(repo => repo.GetItemsByListIdAsync(2)).ReturnsAsync(expectedResult);

            //Act
            var result = await _ListItemService.GetItemsByListIdAsync(2);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<ListItem>>(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task AddItemAsync_CallRepositoryMethod()
        {
            //Arrange
            var newItem = new ListItem {title = "task 1", description = "task 1 desp" };

            //Act
            await _ListItemService.AddItemAsync(newItem);

            //Assert
            _mockListItemRepository.Verify(repo => repo.AddItemAsync(newItem), Times.Once);
        }

        [Fact]
        public async Task UpdateItemAsync_CallRepositoryMethod()
        {
            //Arrange
            var updatedItem = new ListItem { listItemId = 1, title = "task update", description = "task update desp" };

            //Act
            await _ListItemService.UpdateItemAsync(updatedItem);

            //Assert
            _mockListItemRepository.Verify(repo => repo.UpdateItemAsync(updatedItem), Times.Once);
        }

        [Fact]
        public async Task UpdateItemStatusAsync_CallRepositoryMethod()
        {
            //Arrange
            int itemIdToUpdateStatus = 1;

            //Act
            await _ListItemService.UpdateItemStatusAsync(itemIdToUpdateStatus);

            //Assert
            _mockListItemRepository.Verify(repo => repo.UpdateItemStatusAsync(itemIdToUpdateStatus), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_CallRepositoryMethod()
        {
            //Arrange
            int itemIdToDelete = 1;

            //Act
            await _ListItemService.DeleteItemAsync(itemIdToDelete);

            //Assert
            _mockListItemRepository.Verify(repo => repo.DeleteItemAsync(itemIdToDelete), Times.Once);
        }
    }
}
