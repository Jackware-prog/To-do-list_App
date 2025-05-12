using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_do_list_Application.Interfaces;
using To_do_list_Core.Entities;

namespace To_do_list_Application.Services
{
    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;

        public ListService(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public async Task AddListAsync(List list)
        {
            await _listRepository.AddListAsync(list);
        }

        public async Task DeleteListAsync(int id)
        {
            await _listRepository.DeleteListAsync(id);
        }

        public async Task<IEnumerable<List>> GetAllListsAsync()
        {
            return await _listRepository.GetAllListsAsync();
        }

        public async Task<List> GetListByIdAsync(int id)
        {
            return await _listRepository.GetListByIdAsync(id);
        }

        public async Task UpdateListAsync(List list)
        {
            await _listRepository.UpdateListAsync(list);
        }
    }
}
