using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Inventory;
using CodeBase.Inventory.View;
using UnityEngine;

namespace CodeBase.Inventory
{
    public class Inventory
    {
        public string Name { get; }
        public InventoryView View { get; }
        public int Capacity => _cells.Count;

        private readonly ItemsDataProvider _dataProvider;

        private List<Cell> _cells;

        public Inventory(string name, ItemsDataProvider dataProvider, InventoryView view, int capacity)
        {
            Name = name;
            _dataProvider = dataProvider;
            View = view;

            SetCells(capacity);

            View.SetTitle(name);
            View.CellRemoved += OnCellRemoved;
            View.CellChanged += OnCellChanged;
        }

        public void LoadData(List<Cell> cells)
        {
            if (_cells.Count != cells.Count)
                throw new Exception("Lists lengths don't match");

            _cells = cells;
            UpdateView();
        }

        public List<Cell> GetData()
        {
            return new List<Cell>(_cells);
        }

        public void UpdateView()
        {
            //if (_cells.Count > 0)
                View.UpdateCells(_cells);
        }

        private void OnCellChanged(int index, string id, int amount)
        {
            Debug.Log($"{Name}: cell №{index} changed to Id {id}");

            RemoveItems(index, 1);
            AddItems(index, id, amount, out var remained);
        }

        private void OnCellRemoved(int index)
        {
            Debug.Log($"{Name}: cell №{index} removed");

            RemoveItems(index, 1);
        }

        public bool TryAddItem(string id)
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                if (_cells[i] is null)
                {
                    SetCell(i, new Cell(id, 1));
                    return true;
                }
            }

            UpdateView();
            return false;
        }

        public void AddItems(int index, string id, int amount, out int remained)
        {
            CheckIndexValidity(index);
            CheckIdValidity(id);

            Cell cell = _cells[index];

            remained = amount;

            if (IsCellEmpty(index))
            {
                SetCell(index, new Cell(id, amount));
                remained = 0;
            }
            else if (cell.Id == id)
            {
                cell.Add(amount);
                remained = 0;
            }
        }

        private Cell RemoveItems(int index, int amount)
        {
            CheckIndexValidity(index);

            Cell cell = _cells[index];
            ClearCell(index);

            return cell;
        }

        private void MergeCell(Cell cell, int index)
        {
        }

        private void SetCell(int index, Cell cell)
        {
            _cells[index] = cell;
            UpdateView();
        }

        private void ClearCell(int index) =>
            _cells[index] = null;

        private bool IsCellEmpty(int index) =>
            _cells[index] is null;

        private bool IsInsideBounds(int index) =>
            index >= 0 && index < _cells.Count;

        private void SetCells(int capacity)
        {
            capacity = capacity < 0 ? 0 : capacity;

            _cells = new Cell[capacity].ToList();
        }

        private void CheckIdValidity(string id)
        {
            if (!_dataProvider.IsValid(id))
                throw new Exception($"Invalid id exception {id}");
        }

        private void CheckIndexValidity(int index)
        {
            if (!IsInsideBounds(index))
                throw new IndexOutOfRangeException($"Index: {index}, Max: {_cells.Count - 1}");
        }
    }
}