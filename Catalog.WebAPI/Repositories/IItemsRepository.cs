using Catalog.WebAPI.Entities;
using System;
using System.Collections.Generic;

namespace Catalog.WebAPI.Repositories
{
    public interface IItemsRepository
    {
        Item GetItem(Guid id);
        List<Item> GetItems();

        void CreateItem(Item item);

        void UpdateItem(Item item);

        void DeleteItem(Guid id);
    }
}