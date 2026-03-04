using UnityEngine;
using UnityEngine.UI;

namespace DevFuckers._Project.CodeBase.Runtime.Common.UI.Bars
{
    public class LocalBarUI : MonoBehaviour
    {
        [SerializeField] private Slider _Slider;

        public void UpdateUI(float currentValue, float maxValue)
        {
            _Slider.value = currentValue / maxValue;
        }
    }
}
