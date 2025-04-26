using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _ZeroLayerWeight = 1f;
        [SerializeField] private float _FirstLayerWeight = 1f;
        // [SerializeField] private AnimationClip[] _animationClips;

        private void Start()
        {
            // _animator.layers[0].weight = 1f; // Слой движения
            // _animator.layers[1].weight = 1f; // Слой действий
            // _animator.SetLayerWeight(0, 1);
            // _animator.SetLayerWeight(1, 0.9f);
        }

        public void SetAnimationState(string stateName, int layerIndex, float transitionTime = 0.1f, float weight = 199)
        {
            if (_animator == null)
            {
                Debug.LogError("Animator is not assigned.");
                return;
            }

            if (string.IsNullOrEmpty(stateName))
            {
                Debug.LogError("State name is null or empty.");
                return;
            }

            if ( weight != 199)
            {
                _animator.SetLayerWeight(layerIndex, weight);
            }

            _animator.CrossFadeInFixedTime(stateName, transitionTime, layerIndex);
        }

        public void SetAnimatorLayerWeight(int layerIndex, float weight)
        {
            _animator.SetLayerWeight(layerIndex, weight);
        }

        // private void Update()
        // {
        //     _animator.SetLayerWeight(0, _ZeroLayerWeight);
        //     _animator.SetLayerWeight(1, _FirstLayerWeight);
        // }
    }
}
