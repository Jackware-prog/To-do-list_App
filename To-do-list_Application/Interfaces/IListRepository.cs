using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_do_list_Core.Entities;

namespace To_do_list_Application.Interfaces
{
    public interface IListRepository
    {
        Task<IEnumerable<List>> GetAllListsAsync();

        Task<List> GetListByIdAsync(int id);

        Task AddListAsync(List list);

        Task UpdateListAsync(List list);

        Task DeleteListAsync(int id);
    }
}
