using Catalog.WebAPI.Dtos;
using Catalog.WebAPI.Entities;
using Catalog.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.WebAPI.Controllers
{
    [Route("[controller]")] //  /api/
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _repository;

        public ItemsController(IItemsRepository repository)
        {
            this._repository = repository;
        }

        // Get/items
        [HttpGet("")]
        public IActionResult GetItems()
        {
            var items = _repository.GetItems().Select(item => item.AsDto());

            if (items != null)
                return Ok(items);

            return NotFound();
        }

        // Get/items/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
         {
            var item = _repository.GetItem(id);

            if (item != null)
                return Ok(item.AsDto());

            return NotFound();
        }

        // POST/items
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new Item()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }

        // PUT/items/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = _repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with   // with copysini yapıyor record sağolsun...
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            _repository.UpdateItem(updatedItem);

            return NoContent();

        }

        [HttpDelete]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = _repository.GetItem(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            _repository.DeleteItem(id);
            return NoContent();
        }


    }
}
