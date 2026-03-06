using System;

namespace CodeBase.Inventory.View
{
    public interface IUpdatable
    {
        public event Action Updated;
    }
}