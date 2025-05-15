using Microsoft.AspNetCore.Mvc;
using To_do_list_Application.Interfaces;
using To_do_list_Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace To_do_list_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListItemController : ControllerBase
    {

        private readonly IListItemService _listItemService;

        public ListItemController(IListItemService _listItemService) 
        { 
            this._listItemService = _listItemService;
        }

        //Get: api/ListItem/{Id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<ListItem>> GetById(int Id)
        {
            return Ok(await _listItemService.GetItemByIdAsync(Id));
        }

        //Get: api/ListItem/listid/{Id}
        [HttpGet("listid/{ListId}")]
        public async Task<ActionResult<ListItem>> GetItemsByListId(int ListId)
        {
            return Ok(await _listItemService.GetItemsByListIdAsync(ListId));
        }

        //Post: api/ListItem
        [HttpPost]
        public async Task<ActionResult> AddItem(ListItem item)
        {
            await _listItemService.AddItemAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = item.listItemId }, item);
        }

        //Put: api/ListItem/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(int id, ListItem item)
        {
            if (id != item.listItemId) return BadRequest();
            await _listItemService.UpdateItemAsync(item);
            return NoContent();
        }

        //Put: api/ListItem/Check/{id}
        [HttpPut("Check/{id}")]
        public async Task<ActionResult> UpdateItemStatus(int id)
        {
            await _listItemService.UpdateItemStatusAsync(id);
            return NoContent();
        }

        //Delete: api/ListItem/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            await _listItemService.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
