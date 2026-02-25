using UnityEngine;
using UnityEngine.UI;

public class LocalPlayerBarUI : MonoBehaviour
{
    [SerializeField] private Slider _Slider;

    public void UpdateUI(float currentValue, float maxValue)
    {
        _Slider.value = currentValue / maxValue;
    }
}
