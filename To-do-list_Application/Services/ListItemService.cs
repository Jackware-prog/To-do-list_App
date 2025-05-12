using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_do_list_Application.Interfaces;
using To_do_list_Core.Entities;

namespace To_do_list_Application.Services
{
    public class ListItemService : IListItemService
    {

        private readonly IListItemRepository _listItemRepository;

        public ListItemService(IListItemRepository listItemRepository)
        {
            _listItemRepository = listItemRepository;
        }

        public async Task AddItemAsync(ListItem item)
        {
            await _listItemRepository.AddItemAsync(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            await _listItemRepository.DeleteItemAsync(id);
        }

        public async Task<ListItem> GetItemByIdAsync(int id)
        {
            return await _listItemRepository.GetItemByIdAsync(id);
        }

        public async Task<IEnumerable<ListItem>> GetItemsByListIdAsync(int ListId)
        {
            return await _listItemRepository.GetItemsByListIdAsync(ListId);
        }

        public async Task UpdateItemAsync(ListItem item)
        {
            await _listItemRepository.UpdateItemAsync(item);
        }
    }
}
