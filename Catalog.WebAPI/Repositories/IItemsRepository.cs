using Catalog.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.WebAPI.Repositories
{
    public interface IItemsRepository
    {
        Task<Item> GetItemAsync(Guid id);
        Task<List<Item>> GetItemsAsync();
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
    }
}