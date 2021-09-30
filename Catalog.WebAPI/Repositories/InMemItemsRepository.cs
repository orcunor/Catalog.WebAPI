using Catalog.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.WebAPI.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new List<Item>()
        {
            new Item(){Id = Guid.NewGuid(), Name = "Poision", Price = 9, CreatedDate = DateTimeOffset.UtcNow},
            new Item(){Id = Guid.NewGuid(), Name = "Iron Sword", Price = 19, CreatedDate = DateTimeOffset.UtcNow},
            new Item(){Id = Guid.NewGuid(), Name = "Bronze Shield", Price =15, CreatedDate = DateTimeOffset.UtcNow},
        };

        public async Task<List<Item>> GetItemsAsync()
        {
            try
            {
                return await Task.FromResult(items);
            }
            catch { }
            return null;
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            try
            {
                var item = items.Where(item => item.Id == id).FirstOrDefault();
                return await Task.FromResult(item);
            }
            catch { }
            return null;
        }

        public async Task CreateItemAsync(Item item)
        {
            try
            {
                items.Add(item);
                await Task.CompletedTask;
            }
            catch { }
        }

        public async Task UpdateItemAsync(Item item)
        {
            try
            {
                var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
                items[index] = item;
                await Task.CompletedTask;
            }
            catch {}
            
            
        }

        public async Task DeleteItemAsync(Guid id)
        {
            try
            {
                var index = items.FindIndex(existingItem => existingItem.Id == id);
                items.RemoveAt(index);
                await Task.CompletedTask;
            }
            catch {}
        }
    }
}
