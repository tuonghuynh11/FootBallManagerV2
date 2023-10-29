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
    public class ItemtypesController : ControllerBase
    {
        private readonly IItemtypeRepository _itemtypesRepo;

        public ItemtypesController(IItemtypeRepository itemtypeRepository)
        {
            _itemtypesRepo = itemtypeRepository;
        }

        // GET: api/Itemtypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Itemtype>>> GetItemtypes()
        {
            try
            {
                return Ok(await _itemtypesRepo.GetAllItemtypesAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Itemtypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Itemtype>> GetItemtype(int id)
        {
            var itemtype = await _itemtypesRepo.GetItemtypeAsync(id);
            return itemtype == null ? NotFound() : Ok(itemtype);
        }

        // PUT: api/Itemtypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemtype(int id, Itemtype itemtype)
        {
            if (id != itemtype.Id)
            {
                return NotFound();
            }
            await _itemtypesRepo.updateItemtypeAsync(id, itemtype);
            return Ok();
        }

        // POST: api/Itemtypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Itemtype>> PostItemtype(Itemtype itemtype)
        {
            try
            {
                var newItemtypeId = await _itemtypesRepo.addItemtypeAsync(itemtype);
                var itemtypeNew = await _itemtypesRepo.GetItemtypeAsync(newItemtypeId);
                return itemtypeNew == null ? NotFound() : Ok(itemtypeNew);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Itemtypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemtype(int id)
        {
            await _itemtypesRepo.deleteItemtypeAsync(id);
            return Ok();
        }

    }
}
