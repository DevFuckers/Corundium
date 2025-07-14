using UnityEngine;
using UnityEngine.UI;

public class WorldHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    private Camera _mainCam;

    void Start()
    {
        if (Camera.main != null)
            _mainCam = Camera.main;
    }

    public void UpdateUI(float currentValue, float maxValue)
    {
        if (_healthSlider != null && maxValue > 0)
            _healthSlider.value = currentValue / maxValue;
    }


    void LateUpdate()
    {
        if (_mainCam != null)
        {
            transform.LookAt(transform.position + _mainCam.transform.rotation * Vector3.forward,
                             _mainCam.transform.rotation * Vector3.up);
        }
    }
}

