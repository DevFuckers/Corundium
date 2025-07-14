using UnityEngine;
using UnityEngine.UI;

public class LocalPlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    public void UpdateUI(float currentValue, float maxValue)
    {
        _healthSlider.value = currentValue / maxValue;
    }
}
