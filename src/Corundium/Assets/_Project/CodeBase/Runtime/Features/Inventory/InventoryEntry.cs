using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Inventory
{
    public class InventoryEntry : MonoBehaviour
    {
        [SerializeField] private ItemsIdentifierSO _itemsDataController;

        private void Start()
        {
            List<ItemData> itemData = _itemsDataController.GetItemsData();

            ItemsSorter sorter = new();
            List<ItemData> items = sorter.GetEmptySorted(itemData, out var emptyItem);

            ItemsDataProvider provider = new(items, emptyItem);
                
            Debug.Log(provider.GetItemById("emerald").MaxStackAmount);

        }
    }
}