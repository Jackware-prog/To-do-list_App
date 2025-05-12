using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using To_do_list_Application.Interfaces;
using To_do_list_Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace To_do_list_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListService _listService;

        public ListController(IListService _listService)
        {
            this._listService = _listService;
        }


        // GET: api/List
        [HttpGet]
        public async Task<ActionResult<List>> GetAllList()
        {
            return Ok(await _listService.GetAllListsAsync());
        }

        // GET api/List/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<List>> GetListById(int id)
        {
            return Ok(await _listService.GetListByIdAsync(id));
        }

        // POST api/List
        [HttpPost]
        public async Task<ActionResult> AddList(List list)
        {
            await _listService.AddListAsync(list);
            return CreatedAtAction(nameof(GetListById), new { id = list.listId }, list);
        }

        // PUT api/List/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateList(int id, List list)
        {
            if (id != list.listId) return BadRequest();
            await _listService.UpdateListAsync(list);
            return NoContent();
        }

        // DELETE api/List/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteList(int id)
        {
            await _listService.DeleteListAsync(id);
            return NoContent();
        }
    }
}
