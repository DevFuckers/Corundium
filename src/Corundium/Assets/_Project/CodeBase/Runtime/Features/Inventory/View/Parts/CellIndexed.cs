using System;
using UnityEngine;

namespace CodeBase.Inventory.View
{
    public class CellIndexed
    {
        public int InventoryIndex { get; }
        public int CellIndex { get; }

        public ItemData Data { get; private set; }
        public int Amount { get; private set; }

        public event Action<Vector3, CellIndexed> Pressed;

        private readonly CellView _cellView;

        public CellIndexed(CellView cellView, int inventoryIndex, int cellIndex)
        {
            _cellView = cellView;
            
            InventoryIndex = inventoryIndex;
            CellIndex = cellIndex;

            Data = _cellView.ItemData;
            Amount = _cellView.ItemAmount;

            _cellView.Pressed += OnPressed;
        }

        public void Destruct()
        {
            _cellView.Pressed -= OnPressed;
        }

        private void OnPressed(Vector3 position) =>
            Pressed?.Invoke(position, this);
    }
}