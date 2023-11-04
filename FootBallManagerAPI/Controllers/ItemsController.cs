using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Repositories;

namespace FootBallManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepo;

        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepo = itemRepository;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            try
            {
                return Ok(await _itemRepo.GetAllItemsAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            try
            {

                var item = await _itemRepo.GetItemAsync(id);
                return item == null ? NotFound() : Ok(item);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            try
            {

                if (id != item.Id)
                {
                    return NotFound();
                }
                await _itemRepo.updateItemAsync(id, item);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            try
            {
                var newItem = await _itemRepo.addItemAsync(item);
                return newItem == null ? NotFound() : Ok(newItem);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {

                await _itemRepo.deleteItemAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
