using System.Collections.Generic;

namespace CodeBase.Inventory.View
{
    public interface IInventoryView
    {
        void UpdateCells(List<Cell> cellsData);
        //void Construct(ItemDataProvider provider);
    }
}