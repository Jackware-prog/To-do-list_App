using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_do_list_Application.Interfaces;
using To_do_list_Core.Entities;
using To_do_list_Infrastructure.Persistence;

namespace To_do_list_Infrastructure.Repositories
{
    public class ListRepository : IListRepository
    {   
        private readonly To_do_list_DbContext _context;

        public ListRepository(To_do_list_DbContext to_do_list_DbContext)
        {
            this._context = to_do_list_DbContext;
        }

        public async Task AddListAsync(List list)
        {
            _context.Lists.Add(list);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteListAsync(int id)
        {
            var list = await _context.Lists.FindAsync(id);

            if (list != null)
            {
                _context.Lists.Remove(list);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<List>> GetAllListsAsync()
        {
            return await _context.Lists.ToListAsync();
        }

        public async Task<List> GetListByIdAsync(int id)
        {
            return await _context.Lists.FindAsync(id);
        }

        public async Task UpdateListAsync(List list)
        {
            _context.Lists.Update(list);
            await _context.SaveChangesAsync();
        }
    }
}
