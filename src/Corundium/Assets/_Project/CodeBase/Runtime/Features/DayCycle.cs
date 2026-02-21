using UnityEngine;

namespace DevFuckers
{
    public class DayCycle : MonoBehaviour
    {
        [Header("Time Settings")] [SerializeField, Range(0, 1)]
        private float _progress;

        [SerializeField] private float _duration = 30f;

        [Header("Celestial Bodies")] [SerializeField]
        private Light _sun;

        [SerializeField] private Light _moon;

        [Header("Animation Curves")] [SerializeField]
        private AnimationCurve _sunIntensityCurve;

        [SerializeField] private AnimationCurve _moonIntensityCurve;
        [SerializeField] private AnimationCurve _skyboxBlendCurve;

        [Header("Skybox")] [SerializeField] private Material _skyboxMaterial;

        [Header("Skybox Settings")] [SerializeField]
        private float _dayExposure = 1.3f;

        [SerializeField] private float _nightExposure = 0.5f;
        [SerializeField] private Color _dayTint = Color.white;
        [SerializeField] private Color _nightTint = new Color(0.2f, 0.2f, 0.4f);

        private float _sunIntensity;
        private float _moonIntensity;

        private static readonly int CubemapTransition = Shader.PropertyToID("_CubemapTransition");
        private static readonly int CubemapExposure = Shader.PropertyToID("_CubemapExposure");
        private static readonly int CubemapTint = Shader.PropertyToID("_CubemapTintColor");

        private void Start()
        {
            _sunIntensity = _sun.intensity;
            _moonIntensity = _moon.intensity;
            RenderSettings.skybox = _skyboxMaterial;
        }

        private void Update()
        {
            _progress += Time.deltaTime / _duration;
            if (_progress >= 1f) _progress -= 1f;

            UpdateSkybox();
            UpdateCelestialBodies();
        }

        private void UpdateSkybox()
        {
            float blend = _skyboxBlendCurve.Evaluate(_progress);
            float reversedBlend = _skyboxBlendCurve.Evaluate(1 - _progress);

            _skyboxMaterial.SetFloat(CubemapTransition, reversedBlend);
            _skyboxMaterial.SetFloat(CubemapExposure, Mathf.Lerp(_nightExposure, _dayExposure, blend));
            _skyboxMaterial.SetColor(CubemapTint, Color.Lerp(_nightTint, _dayTint, blend));

            RenderSettings.sun = blend > 0.5f ? _sun : _moon;

            if (Time.frameCount % 5 == 0)
                DynamicGI.UpdateEnvironment();
        }

        private void UpdateCelestialBodies()
        {
            _sun.transform.localRotation = Quaternion.Euler(_progress * 360f, 180f, 0f);
            _moon.transform.localRotation = Quaternion.Euler(_progress * 360f + 180f, 180f, 0f);

            _sun.intensity = _sunIntensity * _sunIntensityCurve.Evaluate(_progress);
            _moon.intensity = _moonIntensity * _moonIntensityCurve.Evaluate(1 - _progress);
        }
    }
}