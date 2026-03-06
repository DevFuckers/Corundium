using System.Collections.Generic;
using CodeBase.Inventory.View;
using UnityEngine;

namespace CodeBase.Inventory.Architecture
{
    public class InventoriesController : MonoBehaviour
    {
        private Inventory _chestInventory;
        private Inventory _playerInventory;
        private Canvas _canvas;
        Vector3 _origin;

        public void Construct(Inventory chestInventory, Inventory playerInventory, Canvas canvas)
        {
            _canvas = canvas;
            _origin = _canvas.pixelRect.size / 2f;

            _playerInventory = playerInventory;
            _chestInventory = chestInventory;
        }

        Rect GetWorldSapceRect(RectTransform rt)
        {
            var r = rt.rect;
            r.center = rt.TransformPoint(r.center);
            r.size = rt.TransformVector(r.size);
            return r;
        }

        public void OpenPlayerInventory()
        {
            ShowInventory(_playerInventory.View);
            _playerInventory.View.transform.position = _origin;
        }

        public void ClosePlayerInventory()
        {
            HideInventory(_playerInventory.View);
        }

        public void OpenBoth()
        {
            ShowInventory(_chestInventory.View);
            ShowInventory(_playerInventory.View);

            _chestInventory.View.transform.position = _origin + Vector3.right * 400;
            _playerInventory.View.transform.position = _origin + Vector3.right * -400;
        }

        public void CloseBoth()
        {
            HideInventory(_chestInventory.View);
            HideInventory(_playerInventory.View);
        }

        public void AddItemToPlayerInventory(string id) =>
            AddItem(id, _playerInventory);

        public void AddItemToChestInventory(string id) =>
            AddItem(id, _chestInventory);

        private void AddItem(string id, Inventory inventory)
        {
            if (inventory.TryAddItem(id) == false)
            {
                Debug.Log($"{inventory.Name} is full!");
            }
        }

        private void ShowInventory(InventoryView inventory)
        {
            inventory.gameObject.SetActive(true);
        }

        private void HideInventory(InventoryView inventory)
        {
            inventory.gameObject.SetActive(false);
        }
    }
}