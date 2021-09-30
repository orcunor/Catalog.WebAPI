using Catalog.WebAPI.Dtos;
using Catalog.WebAPI.Entities;
using Catalog.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.WebAPI.Controllers
{
    [Route("[controller]")] //  /api/
    [ApiController]
    public class ItemsController : ControllerBase
    {
        #region Fields
        private readonly IItemsRepository _repository;
        #endregion

        #region Constructor
        public ItemsController(IItemsRepository repository)
        {
            this._repository = repository;
        }
        #endregion

        #region Http Methods
        // Get/items
        [HttpGet("")]
        public async Task<IActionResult> GetItemsAsync()
        {
            var items =  (await _repository.GetItemsAsync())
                         .Select(item => item.AsDto());

            if (items != null)
                return Ok(items);

            return NotFound();
        }

        // Get/items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
         {
            var item = await _repository.GetItemAsync(id);

            if (item != null)
                return Ok(item.AsDto());

            return NotFound();
        }

        // POST/items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new Item()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        // PUT/items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await _repository.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with   // with copysini yapıyor record sağolsun...
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await _repository.UpdateItemAsync(updatedItem);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var existingItem = await _repository.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            await _repository.DeleteItemAsync(id);
            return NoContent();
        }
        #endregion

    }
}
