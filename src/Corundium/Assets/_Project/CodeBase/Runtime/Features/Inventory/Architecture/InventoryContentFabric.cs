using System.Collections.Generic;
using CodeBase.Inventory.View;
using UnityEngine;

namespace CodeBase.Inventory.Architecture
{
    public class InventoryContentFabric
    {
        private readonly IObjectFactory _factory;
        private readonly CellHolder _holderPrefab;
        private DragCellView _dragCellPrefab;
        private readonly Canvas _canvas;

        public InventoryContentFabric(IObjectFactory factory, CellHolder holderPrefab, DragCellView dragCellPrefab, Canvas canvas)
        {
            _factory = factory;
            _holderPrefab = holderPrefab;
            _dragCellPrefab = dragCellPrefab;
            _canvas = canvas;
        }

        public List<CellHolder> CreateHolders(int amount, Transform content)
        {
            List<CellHolder> holders = new(amount);

            for (int i = 0; i < amount; i++)
            {
                var holder = CreateHolder(content);
                holders.Add(holder);
            }

            return holders;
        }

        public DragCellView CreateDragCell(Vector3 position)
        {
            DragCellView cell = _factory.Instantiate(_dragCellPrefab, _canvas.transform);
            cell.transform.position = position;

            return cell;
        }

        public void Destroy<T>(T obj) where T : MonoBehaviour => 
            _factory.Destroy(obj);

        public CellHolder CreateHolder(Transform content)
        {
            var holder = _factory.Instantiate(_holderPrefab, content);
            holder.CellView.Hide();
            return holder;
        }

        public void DeleteHolders(List<CellHolder> holders)
        {
            foreach (var holder in holders)
                _factory.Destroy(holder);

            holders.Clear();
        }
    }
}