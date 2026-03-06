using UnityEngine;

namespace CodeBase.Inventory.View
{
    public class CellHolder : MonoBehaviour
    {
        [field: SerializeField] public CellView CellView { get; private set; }
    }
}