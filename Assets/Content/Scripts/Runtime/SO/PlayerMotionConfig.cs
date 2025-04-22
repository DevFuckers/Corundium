using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.SO
{
    [CreateAssetMenu(fileName = "PlayerMotionConfig", menuName = "SO/PlayerMotionConfig")]
    public class PlayerMotionConfig : ScriptableObject
    {
        [field: SerializeField] public float WalkSpeed { get; private set; } = 5f;
        [field: SerializeField] public float CameraRotateSpeed { get; private set; } = 5f;
    }
}
