using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Features.Actor.Data
{
    [CreateAssetMenu(fileName = "ActorMotorData", menuName = "Actor/ActorMotorData")]
    public class ActorMotorData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; set; } = 8;
    }
}
