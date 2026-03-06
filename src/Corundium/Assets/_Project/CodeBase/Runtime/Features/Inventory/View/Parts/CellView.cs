using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.Inventory.View
{
    public class CellView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMPro.TMP_Text _text;

        public event Action<Vector3> Pressed;

        public ItemData ItemData { get; private set; }
        public int ItemAmount { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed?.Invoke(eventData.pressPosition);
        }

        public void Set(ItemData item, int amount)
        {
            ItemData = item;
            ItemAmount = amount;

            SetView(item.Icon, item.Name, amount);
            Show();
        }

        public void Set(CellView cell) => 
            Set(cell.ItemData, cell.ItemAmount);

        public void Clear(ItemData emptyItem)
        {
            ItemData = emptyItem;
            ItemAmount = -1;

            SetView(emptyItem.Icon, emptyItem.Name, 0);
            Hide();
        }

        private void SetView(Sprite icon, string name, int amount)
        {
            _icon.sprite = icon;
            _text.text = $"{name} ";
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}