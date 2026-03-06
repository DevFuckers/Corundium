using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Inventory
{
    public class ItemsDataProvider 
    {
        public ItemData EmptyItem => _emptyItem;
        
        private readonly List<ItemData> _items;
        private readonly ItemData _emptyItem;
        
        public ItemsDataProvider(List<ItemData> items, ItemData emptyItem)
        {
            _items = items;
            _emptyItem = emptyItem;
        }
        
        public ItemData GetItemById(string id)
        {
            foreach (var item in _items)
                if (item.Id == id)
                    return item;

            throw GetException(id);
        }

        public bool IsValid(string id)
        {
            bool result = false;
            
            _items.ForEach(item =>
            {
                if (item.Id == id)
                    result = true;
            });

            return result;
        }
        
        public bool IsEmptyItem(string id) => 
            id == _emptyItem.Id;

        private static Exception GetException(string id) =>
            new($"No itemData with id {id} found.");
    }
}