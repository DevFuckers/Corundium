using CodeBase.Inventory.View;
using UnityEngine;

namespace CodeBase.Inventory.Architecture
{
    public class InventoryWindowFabric
    {
        private readonly IObjectFactory _factory;
        private readonly Canvas _canvas;

        private readonly InventoryView _inventoryPrefab;

        public InventoryWindowFabric(IObjectFactory factory, Canvas canvas, InventoryView inventoryPrefab)
        {
            _factory = factory;
            _canvas = canvas;

            _inventoryPrefab = inventoryPrefab;
        }

        public InventoryView CreateInventory(Vector3 position)
        {
            InventoryView view = CreateWindow(position);

            return view;
        }

        private InventoryView CreateWindow(Vector3 position)
        {
            InventoryView window = _factory.Instantiate(_inventoryPrefab, _canvas.transform);

            position.x += _canvas.pixelRect.width / 2f;
            position.y = _canvas.pixelRect.height / 2f;

            window.transform.position = position;

            return window;
        }
    }
}