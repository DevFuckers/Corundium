using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.SO
{
    [CreateAssetMenu(fileName = "PlayerMotionConfig", menuName = "SO/PlayerMotionConfig")]
    public class PlayerMotionConfig : ScriptableObject
    {
        [field: SerializeField] public float WalkSpeed { get; private set; } = 5f;
        [field: SerializeField] public float SmoothWalkDeltaTime { get; private set; } = 0.25f;
        [field: SerializeField] public float StoppingDeltaTime { get; private set; } = 0.1f;
        [field: SerializeField] public float RunSpeed { get; private set; } = 5f;
        [field: SerializeField] public float SmoothRunDeltaTime { get; private set; } = 0.25f;
        [field: SerializeField] public float CameraRotateSpeed { get; private set; } = 5f;
        [field: SerializeField] public float JumpForce { get; private set; } = 5f;
        [field: SerializeField] public float Gravity { get; private set; } = 20f;
        
        [field: Header("Camera Bobbing: Common")]
        [field: SerializeField] public float BobbingFadeSpeed { get; private set; } = 0.5f;
        [field: SerializeField] public float BobbingSetDestinationSpeed { get; private set; } = 0.5f;

        [field: Header("Camera Bobbing: Walk")]
        [field: SerializeField] public float WalkBobbingFrequencyX { get; private set; } = 0.5f;
        [field: SerializeField] public float WalkBobbingFrequencyY { get; private set; } = 0.5f;
        [field: SerializeField] public float WalkBobbingVerticalAmplitude { get; private set; } = 0.5f;
        [field: SerializeField] public float WalkBobbingHorizontalAmplitude { get; private set; } = 0.5f;

        [field: Header("Camera Bobbing: Run")]
        [field: SerializeField] public float RunBobbingFrequencyX { get; private set; } = 0.5f;
        [field: SerializeField] public float RunBobbingFrequencyY { get; private set; } = 0.5f;
        [field: SerializeField] public float RunBobbingVerticalAmplitude { get; private set; } = 0.5f;
        [field: SerializeField] public float RunBobbingHorizontalAmplitude { get; private set; } = 0.5f;
    }
}
