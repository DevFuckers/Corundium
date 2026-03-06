using UnityEngine;

namespace CodeBase.Inventory
{
    public class ItemData
    {
        public string Id { get; private set; }
        
        public string Name { get; private set; }
        public ItemType Type { get; }
        public int MaxStackAmount { get; private set; }
        
        public Sprite Icon { get; private set; }
        public string Description { get; private set; }

        public ItemData(string id, string name, ItemType type, int maxStackAmount, Sprite icon,
            string description)
        {
            Id = id;
        
            Name = name;
            Type = type;
            MaxStackAmount = maxStackAmount;

            Icon = icon;
            Description = description;
        }
    }
}