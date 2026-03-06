using System;
using DevFuckers._Project.CodeBase.Runtime.Common.InspectorFeatures.ReadOnlyInspector;
using UnityEngine;

namespace CodeBase.Inventory
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Item Data", menuName = "Items/New")]
    public class ItemDataSO : ScriptableObject
    {
        public string Name => _name;

        [SerializeField] [ReadOnlyInspector] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private ItemType _type;
        [SerializeField] private int _maxStackAmount = 10;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _description;

        public ItemData GetItemData() =>
            new(_id, _name, _type, _maxStackAmount, _icon, _description);

        public void SetId(string id) => _id = id;
    }
}