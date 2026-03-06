using System.Collections.Generic;
using CodeBase.Inventory.Architecture;
using UnityEngine;

namespace CodeBase.Inventory.View
{
    //Fix data matryoshka
    //Seperate
    //Hash GetComponent

    public class CellDragger
    {
        private readonly List<InventoryView> _inventories;
        private readonly InventoryContentFabric _fabric;
        private readonly InventoryView _inventoryView;
        private readonly IUpdatable _updatable;

        private DragCellView _dragCell;
        private CellIndexed _draggedCell;

        private Vector3 _position;
        private bool _doDrag;

        private List<CellIndexed> _cells;

        public CellDragger(List<InventoryView> inventories, InventoryContentFabric fabric, IUpdatable updatable)
        {
            _inventories = inventories;
            _fabric = fabric;

            _updatable = updatable;

            _cells = GetIndexedCells(_inventories);
            Subscribe();
        }

        private void Subscribe() =>
            _cells.ForEach(cell => cell.Pressed += Pick);

        private void Unsubscribe() =>
            _cells.ForEach(cell => cell.Pressed -= Pick);

        private void Update()
        {
            UpdatePosition(UnityEngine.Input.mousePosition);

            if (_dragCell is not null && _doDrag)
            {
                Move();
            }
        }

        private void Pick(Vector3 position, CellIndexed draggedCell)
        {
            if (_dragCell is not null)
                return;

            DragCellView cell = CreateDragCell(position, draggedCell);

            _dragCell = cell;
            _draggedCell = draggedCell;

            _doDrag = true;

            _dragCell.Pressed += Release;
            _updatable.Updated += Update;
        }

        private void Move() =>
            _dragCell.transform.position = _position;

        private void Release(Vector3 position)
        {
            _dragCell.Pressed -= Release;
            _updatable.Updated -= Update;
            InventoryView inventory = _inventories[_draggedCell.InventoryIndex];
            _doDrag = false;

            if (IsOverInventoryWindow(out int inventoryIndex))
            {
                if (IsOverCell(inventoryIndex, out int cellIndex))
                    Set(_draggedCell, inventoryIndex, cellIndex);
                else
                    Return(_draggedCell);
            }
            else
            {
                Drop(inventory, _draggedCell.CellIndex);
            }

            Unsubscribe();
            _cells = GetIndexedCells(_inventories);
            Subscribe();
        }

        private void Drop(InventoryView inventory, int cellIndex)
        {
            Debug.Log("DROP");

            inventory.Drop(cellIndex);
            ClearDestroy();
        }

        private void Set(CellIndexed draggedCell, int setInventoryIndex, int cellIndex)
        {
            Debug.Log("SET");

            InventoryView getInventory = _inventories[draggedCell.InventoryIndex];
            InventoryView setInventory = _inventories[setInventoryIndex];

            if (setInventory.IsCellEmpty(cellIndex))
            {
                Debug.Log("SET TO EMPTY");

                string id = _dragCell.ItemData.Id;
                int amount = _dragCell.ItemAmount;

                getInventory.InputRemoveCell(draggedCell.CellIndex);
                setInventory.InputSetCell(cellIndex, id, amount);

                ClearDestroy();
            }
            else
            {
                Debug.Log("REPLACE");
               
                string id = draggedCell.Data.Id;
                int amount = draggedCell.Amount;

                CellView cell = setInventory.GetCell(cellIndex);
                draggedCell = new CellIndexed(cell, setInventoryIndex, cellIndex);
                Debug.Log(draggedCell.Data.Id);
                
                setInventory.InputSetCell(cellIndex, id, amount);
                getInventory.InputRemoveCell(draggedCell.CellIndex);

                _dragCell.Set(draggedCell.Data, draggedCell.Amount);
                
                setInventory.InputRemoveCell(cellIndex);
                setInventory.InputSetCell(cellIndex, id, amount);

                _doDrag = true;
                _updatable.Updated += Update;
                _dragCell.Pressed += Release;
            }

            //Set(_indexedCell, inventoryIndex, cellIndex);
        }

        private void Return(CellIndexed draggedCell)
        {
            string id = draggedCell.Data.Id;
            int index = draggedCell.CellIndex;
            int amount = draggedCell.Amount;

            _inventories[draggedCell.InventoryIndex].SetCell(index, id, amount);

            ClearDestroy();
        }

        private DragCellView CreateDragCell(Vector3 position, CellIndexed indexedCell)
        {
            int inventoryIndex = indexedCell.InventoryIndex;
            int cellIndex = indexedCell.CellIndex;

            DragCellView cell = _fabric.CreateDragCell(position);
            cell.Set(indexedCell.Data, indexedCell.Amount);

            _inventories[inventoryIndex].ClearCell(cellIndex);
            return cell;
        }

        private void ClearDestroy()
        {
            _fabric.Destroy(_dragCell);

            Clear();
        }

        private void Clear()
        {
            _dragCell.Pressed -= Release;

            _dragCell = null;
            _draggedCell = null;
        }

        private void Reset()
        {
            
        }

        private List<CellIndexed> GetIndexedCells(List<InventoryView> inventories)
        {
            List<CellIndexed> result = new();

            for (int i = 0; i < inventories.Count; i++)
            {
                for (int j = 0; j < inventories[i].Capacity; j++)
                {
                    CellView cell = inventories[i].GetCell(j);
                    CellIndexed indexed = new(cell, i, j);

                    result.Add(indexed);
                }
            }

            return result;
        }

        private bool IsOverCell(int inventoryIndex, out int cellIndex)
        {
            var inventory = _inventories[inventoryIndex];
            cellIndex = 0;

            for (int i = 0; i < inventory.Capacity; i++)
            {
                var cell = inventory.GetCell(i);

                var cellRect = GetWorldSpaceRect(cell.GetComponent<RectTransform>());
                var dragCellRect = GetWorldSpaceRect(_dragCell.GetComponent<RectTransform>());

                if (dragCellRect.Overlaps(cellRect))
                {
                    cellIndex = i;
                    return true;
                }
            }

            return false;
        }

        private void UpdatePosition(Vector3 position) =>
            _position = position;

        private bool IsOverInventoryWindow(out int inventoryIndex)
        {
            inventoryIndex = 0;

            for (int i = 0; i < _inventories.Count; i++)
            {
                var inventoryRect = GetWorldSpaceRect(_inventories[i].GetComponent<RectTransform>());
                var dragCellRect = GetWorldSpaceRect(_dragCell.GetComponent<RectTransform>());

                if (dragCellRect.Overlaps(inventoryRect))
                {
                    inventoryIndex = i;
                    return true;
                }
            }

            return false;
        }

        private Rect GetWorldSpaceRect(RectTransform rectTransform)
        {
            var rect = rectTransform.rect;

            rect.center = rectTransform.TransformPoint(rect.center);
            rect.size = rectTransform.TransformVector(rect.size);

            return rect;
        }
    }
}