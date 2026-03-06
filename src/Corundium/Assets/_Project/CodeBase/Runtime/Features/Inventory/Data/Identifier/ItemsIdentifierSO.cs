using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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

        private async UniTask Initialize()
        {
            _loader = new ItemsLoader(_path);
            _identifier = new Identifier();
            
            await LoadItems();
            IdentifyItems(_items);
        }

        public void OnButtonPressed()
        {
            
#if UNITY_EDITOR
            if (Application.isPlaying)
                return;
            
            Initialize().Forget();
#endif            
            
        }

        public List<ItemData> GetItemsData() =>
            _items.Select(item => item.GetItemData()).ToList();

        private async UniTask LoadItems() =>
            _items = await _loader.Load();

        private void IdentifyItems(List<ItemDataSO> items)
        {
            List<string> names = items.Select(item => item.Name).ToList();
            List<string> ids = _identifier.GetIdsByNames(names, null);

            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetId(ids[i]);
                UnityEditor.EditorUtility.SetDirty(items[i]);
            }
            
            UnityEditor.AssetDatabase.SaveAssets();
        }
    }
}