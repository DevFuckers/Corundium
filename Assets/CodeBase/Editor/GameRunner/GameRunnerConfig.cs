using UnityEngine;

namespace CodeBase.Infrastructure
{
    [CreateAssetMenu(fileName = "GameRunnerConfig", menuName = "Configs/GameRunnerConfig")]
    public class GameRunnerConfig : ScriptableObject
    {
        public bool Enabled => _enabled;
        
        [SerializeField] private bool _enabled = true;
    }
}