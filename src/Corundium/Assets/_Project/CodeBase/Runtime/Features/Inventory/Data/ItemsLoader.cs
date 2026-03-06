using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Inventory
{
    public class ItemsLoader
    {
        private string _path;

        public ItemsLoader(string path)
        {
            _path = path;
        }

        public List<ItemDataSO> Load()
        {
            List<ItemDataSO> items = Resources.LoadAll<ItemDataSO>(_path).ToList();
           
            //Debug.Log($"Loaded {items.Count} items data.");

            return items;
        }
    }
}