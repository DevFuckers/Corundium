using System.Collections.Generic;
using CodeBase.Inventory.View;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Inventory.Architecture
{
    public class InventoriesEntryPoint : MonoBehaviour
    {
        [SerializeField] private InventoriesController _controller;
        [FormerlySerializedAs("_itemDataProvider")] [SerializeField] private ItemsDataProvider itemsDataProvider;
        [SerializeField] private Canvas _canvas;

        [SerializeField] private CellHolder _holderPrefab;
        [SerializeField] private DragCellView _dragCellPrefab;
        [SerializeField] private InventoryView _inventoryPrefab;

        private ObjectFactory _objectFactory;
        private InventoryWindowFabric _windowFabric;
        private InventoryContentFabric _contentFabric;

        private Inventory _chestInventory;
        private Inventory _playerInventory;

        private void Start()
        {
            _objectFactory = new ObjectFactory();
            _windowFabric = new InventoryWindowFabric(_objectFactory, _canvas, _inventoryPrefab);
            _contentFabric = new InventoryContentFabric(_objectFactory, _holderPrefab, _dragCellPrefab, _canvas);

            _chestInventory = CreateInventory("Chest Inventory", 10);
            _playerInventory = CreateInventory("Player's Inventory", 15);
            
            _controller.Construct(_chestInventory, _playerInventory, _canvas);
            _controller.CloseBoth();
            
            _playerInventory.TryAddItem("Stones");
            
            List<InventoryView> inventories = new() { _playerInventory.View, _chestInventory.View };
            CellDragger dragger = new(inventories, _contentFabric, _playerInventory.View);
        }

        private Inventory CreateInventory(string name, int capacity)
        {
            InventoryView view = _windowFabric.CreateInventory(Vector3.zero);

            view.Construct(itemsDataProvider, _contentFabric, capacity);
            
            return new(name, itemsDataProvider, view, capacity);
        }

    }
}