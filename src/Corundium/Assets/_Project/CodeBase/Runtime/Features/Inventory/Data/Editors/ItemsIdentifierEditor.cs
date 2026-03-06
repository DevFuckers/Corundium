using CodeBase.Inventory;
using DevFuckers._Project.CodeBase.Runtime.Common.InspectorFeatures.ButtonEditor;
using UnityEditor;

namespace DevFuckers._Project.CodeBase.Runtime.Features.Inventory.Data.Editors
{
    [CustomEditor(typeof(ItemsIdentifierSO))]
    public class ItemsIdentifierEditor : ButtonEditor
    {
        private void OnEnable()
        {
            SetButtonName("Reload & Identify");
        }
    }
}