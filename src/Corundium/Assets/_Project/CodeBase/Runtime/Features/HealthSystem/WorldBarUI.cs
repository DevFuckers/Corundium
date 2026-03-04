using UnityEngine;
using UnityEngine.UI;

public class WorldBarUI : MonoBehaviour
{
    [SerializeField] private Slider _Slider;
    private Camera _mainCam;

    void Start()
    {
        if (Camera.main != null)
            _mainCam = Camera.main;
    }

    public void UpdateUI(float currentValue, float maxValue)
    {
        if (_Slider != null && maxValue > 0)
            _Slider.value = currentValue / maxValue;
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

