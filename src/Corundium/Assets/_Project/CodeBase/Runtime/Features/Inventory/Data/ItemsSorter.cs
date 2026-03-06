using System.Collections.Generic;

namespace CodeBase.Inventory
{
    public class ItemsSorter
    {
        public List<ItemData> GetEmptySorted(List<ItemData> allItems, out ItemData emptyItem)
        {
            List<ItemData> items = new();
            
            emptyItem = null;

            foreach (var item in allItems)
            {
                if (item.Type != ItemType.Empty)
                {
                    items.Add(item);
                }
                else if (emptyItem is null)
                {
                    emptyItem = item;
                }
            }

            return items;
        }
    }
}