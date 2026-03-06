using System;
using System.Collections.Generic;
using System.Linq;
using DevFuckers._Project.CodeBase.Runtime.Common.InspectorFeatures.ButtonEditor;
using DevFuckers._Project.CodeBase.Runtime.Common.InspectorFeatures.ReadOnlyInspector;
using UnityEngine;

namespace CodeBase.Inventory
{
    [CreateAssetMenu(fileName = nameof(ItemsIdentifierSO), menuName = "Items/Identifier")]
    public class ItemsIdentifierSO : ScriptableObject, IButtonPressedHandler
    {
        [SerializeField] private string _path = "";

        [SerializeField] [ReadOnlyInspector] private List<ItemDataSO> _items;

        private ItemsLoader _loader;
        private Identifier _identifier;

        private void Initialize()
        {
            _loader = new ItemsLoader(_path);
            _identifier = new Identifier();
            
            LoadItems();
            IdentifyItems(_items);
        }

        private void Awake()
        {
             Initialize();
        }

        public void OnButtonPressed()
        {
            Initialize();
        }

        public List<ItemData> GetItemsData() =>
            _items.Select(item => item.GetItemData()).ToList();

        private void LoadItems() =>
            _items = _loader.Load();

        private void IdentifyItems(List<ItemDataSO> items)
        {
            List<string> names = items.Select(item => item.Name).ToList();
            List<string> ids = _identifier.GetIdsByNames(names, null);

            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetId(ids[i]);
            }
        }
    }
}