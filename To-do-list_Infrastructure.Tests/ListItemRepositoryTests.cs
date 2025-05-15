using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using To_do_list_Application.Interfaces;
using To_do_list_Application.Services;
using To_do_list_Core.Entities;
using To_do_list_Infrastructure.Persistence;
using To_do_list_Infrastructure.Repositories;
using Xunit;

namespace To_do_list_Infrastructure.Tests
{
    public class ListItemRepositoryTests
    {
        private readonly To_do_list_DbContext _mockDbContext;
        private readonly ListItemRepository _listItemRepository;


        public ListItemRepositoryTests()
        {
            var _connection = new Microsoft.Data.Sqlite.SqliteConnection("Filename=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<To_do_list_DbContext>()
                .UseSqlite(_connection)
                .Options;


            _mockDbContext = new To_do_list_DbContext(options);
            _mockDbContext.Database.EnsureCreated();

            _listItemRepository = new ListItemRepository(_mockDbContext);

            _mockDbContext.Lists.AddRange(
                new List { listId = 1, title = "List 1", description = "list 1 desp", createdDate = DateTime.Now, updatedDate = DateTime.Now },
                new List { listId = 2, title = "List 2", description = "list 2 desp", createdDate = DateTime.Now, updatedDate = DateTime.Now }
            );

            _mockDbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task GetItemByIdAsync_ReturnSpecificItems()
        {
            //Arrange
            _mockDbContext.ListItems.AddRange(
                new ListItem { listItemId = 1, listId = 1, title = "task 1", description = "task 1", isActive = false, dueDate = DateTime.Now, createdDate = DateTime.Now },
                new ListItem { listItemId = 2, listId = 2, title = "task 2", description = "task 2", isActive = false, dueDate = DateTime.Now, createdDate = DateTime.Now },
                new ListItem { listItemId = 3, listId = 2, title = "task 3", description = "task 3", isActive = false, dueDate = DateTime.Now, createdDate = DateTime.Now }
            );

            await _mockDbContext.SaveChangesAsync();

            //Act
            var items = await _listItemRepository.GetItemByIdAsync(1);

            //Assert
            Assert.True(items != null);
            Assert.IsType<ListItem>(items);
        }

        [Fact]
        public async Task GetItemsByListIdAsync_ReturnListItems()
        {
            //Arrange
            _mockDbContext.ListItems.AddRange(
                new ListItem { listItemId = 1, listId = 1, title = "task 1", description = "task 1", isActive = false, dueDate = DateTime.Now, createdDate = DateTime.Now },
                new ListItem { listItemId = 2, listId = 2, title = "task 2", description = "task 2", isActive = false, dueDate = DateTime.Now, createdDate = DateTime.Now },
                new ListItem { listItemId = 3, listId = 2, title = "task 3", description = "task 3", isActive = false, dueDate = DateTime.Now, createdDate = DateTime.Now }
            );

            await _mockDbContext.SaveChangesAsync();

            //Act
            var items = await _listItemRepository.GetItemsByListIdAsync(2);

            //Assert
            Assert.True(items != null);
            Assert.IsType<List<ListItem>>(items);
            Assert.Equal(2, items.Count());
        }

        [Fact]
        public async Task AddItemAsync_ShouldAddNewRow()
        {
            //Arrange
            _mockDbContext.ListItems.AddRange(
                new ListItem { listItemId = 1, listId = 1, title = "task 1", description = "task 1", isActive = false, dueDate = DateTime.Now, createdDate = DateTime.Now },
                new ListItem { listItemId = 2, listId = 2, title = "task 2", description = "task 2", isActive = false, dueDate = DateTime.Now, createdDate = DateTime.Now },
                new ListItem { listItemId = 3, listId = 2, title = "task 3", description = "task 3", isActive = false, dueDate = DateTime.Now, createdDate = DateTime.Now }
            );

            await _mockDbContext.SaveChangesAsync();

            var newItem = new ListItem { listId = 1, title = "new title", description = "new desc" };

            //Act
            await _listItemRepository.AddItemAsync(newItem);
            var fetchNewItem = await _listItemRepository.GetItemByIdAsync(4);
            var fetchNewItemList = await _listItemRepository.GetItemsByListIdAsync(1);

            //Assert
            Assert.True(fetchNewItem != null);
            Assert.IsType<ListItem>(fetchNewItem);
            Assert.True(fetchNewItemList.Count() == 2);
        }

        [Fact]
        public async Task UpdateItemAsync_ShouldUpdateItem()
        {
            //Arrange
            var originalItem = new ListItem { listItemId = 1, listId = 1, title = "task 1", description = "task 1", isActive = false, dueDate = DateTime.Now, createdDate = DateTime.Now };

            _mockDbContext.ListItems.Add(originalItem);
            await _mockDbContext.SaveChangesAsync();

            _mockDbContext.Entry(originalItem).State = EntityState.Detached;

            var updatedItem = new ListItem { listItemId = 1, listId = 1, title = "task update", description = "task update desp" };

            //Act
            await _listItemRepository.UpdateItemAsync(updatedItem);

            var fetchUpdatedItem = await _listItemRepository.GetItemByIdAsync(1);

            //Assert
            Assert.True(fetchUpdatedItem != null);
            Assert.True(fetchUpdatedItem.title == "task update" && fetchUpdatedItem.description == "task update desp");
        }

        [Fact]
        public async Task UpdateItemStatusAsync_ShouldUpdateItemStatus()
        {
            //Arrange
            var originalItem = new ListItem { listItemId = 2, listId = 1, title = "task 1", description = "task 1", isActive = false, dueDate = DateTime.Now, createdDate = DateTime.Now };

            _mockDbContext.Add(originalItem);
            await _mockDbContext.SaveChangesAsync();

            int TargetItemId = 2;

            //Act
            await _listItemRepository.UpdateItemStatusAsync(TargetItemId);

            var fetchUpdatedItem = await _listItemRepository.GetItemByIdAsync(TargetItemId);

            //Assert
            Assert.True(fetchUpdatedItem != null);
            Assert.True(fetchUpdatedItem.isActive == true);
        }

        [Fact]
        public async Task DeleteItemAsync_ShouldRemoveItem()
        {
            //Arrange
            _mockDbContext.ListItems.AddRange(
                new ListItem { listItemId = 1, listId = 1, title = "task 1", description = "task 1", isActive = false, dueDate = DateTime.Now, createdDate = DateTime.Now }
            );

            await _mockDbContext.SaveChangesAsync();

            //Act
            await _listItemRepository.DeleteItemAsync(1);
            var items = await _listItemRepository.GetItemByIdAsync(1);


            //Assert
            Assert.True(items == null);
        }
    }
}
