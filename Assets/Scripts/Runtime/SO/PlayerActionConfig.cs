using UnityEngine;

namespace DevFuckers
{
    [CreateAssetMenu(fileName = "PlayerActionConfig", menuName = "SO/PlayerActionConfig")]
    public class PlayerActionConfig : ScriptableObject
    {
        [field: SerializeField] public float AttackTime { get; private set; } = 1f;
    }
}
