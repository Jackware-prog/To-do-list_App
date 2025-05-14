using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_do_list_Application.Interfaces;
using To_do_list_Core.Entities;
using To_do_list_Infrastructure.Persistence;

namespace To_do_list_Infrastructure.Repositories
{
    public class ListItemRepository : IListItemRepository
    {
        private readonly To_do_list_DbContext _context;

        public ListItemRepository(To_do_list_DbContext to_Do_List_DbContext) 
        {
            this._context = to_Do_List_DbContext;
        }
        public async Task AddItemAsync(ListItem item)
        {
            _context.ListItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _context.ListItems.FindAsync(id);

            if(item != null)
            {
                _context.ListItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ListItem> GetItemByIdAsync(int id)
        {
            return await _context.ListItems.FindAsync(id);
        }

        public async Task<IEnumerable<ListItem>> GetItemsByListIdAsync(int ListId)
        {
            return await _context.ListItems.Where(i => i.listId == ListId).ToListAsync();
        }

        public async Task UpdateItemAsync(ListItem item)
        {
            _context.ListItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemStatusAsync(int Id)
        {
            var item = await _context.ListItems.FindAsync(Id);
            if (item != null)
            {
                item.isActive = !item.isActive;
                await _context.SaveChangesAsync();
            }
        }
    }
}
