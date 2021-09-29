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

        public List<Item> GetItems()
        {
            try
            {
                return items;
            }
            catch { }
            return null;
        }

        public Item GetItem(Guid id)
        {
            try
            {
                return items.Where(item => item.Id == id).FirstOrDefault();
            }
            catch { }
            return null;
        }

        public void CreateItem(Item item)
        {
            try
            {
                items.Add(item);
            }
            catch { }
        }

        public void UpdateItem(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
        }

        public void DeleteItem(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
        }
    }
}
