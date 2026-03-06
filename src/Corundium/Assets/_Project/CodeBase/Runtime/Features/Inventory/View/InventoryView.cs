using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Inventory;
using CodeBase.Inventory.Architecture;
using CodeBase.Inventory.View;
using TMPro;
using UnityEngine;

namespace CodeBase.Inventory.View
{
    public class InventoryView : MonoBehaviour, IInventoryView, IUpdatable
    {
        [SerializeField] private Transform _content;
        [SerializeField] private TMP_Text _title;
        
        public Transform Content => _content;

        public event Action Updated;
        
        public event Action<int, string, int> CellChanged;
        public event Action<int> CellRemoved;
        
        public int Capacity { get; private set; }

        private ItemsDataProvider _provider;
        private InventoryContentFabric _fabric;
        private CellDragger _cellDragger;

        private List<CellHolder> _holders;
        private List<CellView> _viewCells;

        private int _lastHighLightIndex;
        
        public void Construct(ItemsDataProvider provider, InventoryContentFabric fabric, int capacity)
        {
            Capacity = capacity;
            _fabric = fabric;
            _provider = provider;

            _holders = fabric.CreateHolders(capacity, _content);
            _viewCells = _holders.Select(h => h.CellView).ToList();

            for (int i = 0; i < capacity; i++) 
                ClearCell(i);
        }

        private void Update()
        {
            Updated?.Invoke();
        }

        public void SetTitle(string name) => 
            _title.text = name;

        public void UpdateCells(List<Cell> cellsData)
        {
            CheckCountMatch(_viewCells.Count, cellsData.Count);

            for (int i = 0; i < cellsData.Count; i++)
                ChangeCell(i, cellsData[i]);
        }

        public bool IsCellEmpty(int index)
        {
            //validation
            if (_provider.IsEmptyItem(_viewCells[index].ItemData.Id))
            {
                return true;
            }

            return false;
        }
        
        private void ChangeCell(int index, Cell cell)
        {
            if (cell is null)
                ClearCell(index);
            else
                SetCell(index, cell.Id, cell.Amount);
        }

        public void InputSetCell(int index, string id, int amount)
        {
            CellChanged?.Invoke(index, id, amount);
            
            SetCell(index, id, amount);
        }

        public void InputRemoveCell(int index)
        {
            CellRemoved?.Invoke(index);
            
            ClearCell(index);
        }

        public void SetCell(int index, string id, int amount)
        {
            //validation
            
            ItemData Data = _provider.GetItemById(id);
            var cell = _viewCells[index];

            cell.Set(Data, amount);
            cell.Show();
        }

        public bool IsSameId(int cellIndex, string id)
        {
            //validation

            return _viewCells[cellIndex].ItemData.Id == id;
        }
        
        public CellView GetCell(int index)
        {
            //validation 
            
            return _viewCells[index];
        }

        public void ClearCell(int index)
        {
            if (_viewCells[index] == null || _provider.EmptyItem == null)
                return;
            
            _viewCells[index].Clear(_provider.EmptyItem);
        }

        private void CheckCountMatch(int viewCount, int dataCount)
        {
            if (viewCount != dataCount)
                throw new($"Inventory data cells amount ({dataCount}) doesn't match view cells amount({viewCount})!");
        }

        public void Drop(int index)
        {
            Debug.Log($"Dropped cell {index}");
            //throw new NotImplementedException();
        }
    }
}