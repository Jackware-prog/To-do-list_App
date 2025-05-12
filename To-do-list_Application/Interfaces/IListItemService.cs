using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_do_list_Core.Entities;

namespace To_do_list_Application.Interfaces
{
    public interface IListItemService
    {
        Task<ListItem> GetItemByIdAsync(int id);
        Task<IEnumerable<ListItem>> GetItemsByListIdAsync(int ListId);

        Task AddItemAsync(ListItem item);

        Task UpdateItemAsync(ListItem item);

        Task DeleteItemAsync(int id);
    }
}
