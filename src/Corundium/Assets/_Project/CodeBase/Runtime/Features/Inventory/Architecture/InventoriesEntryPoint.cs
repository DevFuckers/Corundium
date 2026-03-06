using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Inventory.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Inventory.Architecture
{
    public class InventoriesEntryPoint : MonoBehaviour
    {
        [SerializeField] private InventoriesController _controller;
        [SerializeField] private Canvas _canvas;

        [SerializeField] private CellHolder _holderPrefab;
        [SerializeField] private DragCellView _dragCellPrefab;
        [SerializeField] private InventoryView _inventoryPrefab;
        [SerializeField] private ItemsIdentifierSO _itemsDataController;

        private ObjectFactory _objectFactory;
        private ItemsDataProvider _itemsDataProvider;
        private InventoryWindowFabric _windowFabric;
        private InventoryContentFabric _contentFabric;

        private Inventory _chestInventory;
        private Inventory _playerInventory;

        private void Start()
        {
            InitInventorySystem();
        }

        private void InitInventorySystem()
        {
            _objectFactory = new ObjectFactory();
            _windowFabric = new InventoryWindowFabric(_objectFactory, _canvas, _inventoryPrefab);
            _contentFabric = new InventoryContentFabric(_objectFactory, _holderPrefab, _dragCellPrefab, _canvas);
            _itemsDataProvider = SetupItemsDataProvider();

            _chestInventory = CreateInventory("Chest Inventory", 10);
            _playerInventory = CreateInventory("Player's Inventory", 15);
            
            _controller.Construct(_chestInventory, _playerInventory, _canvas);
            _controller.CloseBoth();
            
            // TEST
            _playerInventory.TryAddItem("stones");
            
            List<InventoryView> inventories = new() { _playerInventory.View, _chestInventory.View };
            CellDragger dragger = new(inventories, _contentFabric, _playerInventory.View);
        }

        private Inventory CreateInventory(string name, int capacity)
        {
            InventoryView view = _windowFabric.CreateInventory(Vector3.zero, name);

            view.Construct(_itemsDataProvider, _contentFabric, capacity);
            
            return new(name, _itemsDataProvider, view, capacity);
        }

        private ItemsDataProvider SetupItemsDataProvider()
        {
            List<ItemData> itemsData = _itemsDataController.GetItemsData();

            if (itemsData.Count == 0)
            {
                Debug.LogWarning("InventoriesEntryPoint::SetupItemsDataProvider(): No items data found.");
            }
            
            ItemsSorter sorter = new();
            List<ItemData> items = sorter.GetEmptySorted(itemsData, out var emptyItem);
            
            return new(items, emptyItem);
        }
    }
}