using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_do_list_Application.Interfaces;
using To_do_list_Application.Services;

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


    }
}
