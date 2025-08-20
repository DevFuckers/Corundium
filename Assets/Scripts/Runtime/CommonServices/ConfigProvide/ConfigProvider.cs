using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.AssetProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.SO;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide
{
    public class ConfigProvider
    {
        private readonly IAssetLoader _assetLoader;

        public ConfigProvider(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        public PlayerMotionConfig PayerMotionConfig {get; private set; }
        public PlayerActionConfig PlayerActionConfig {get; private set; }

        public void LoadAll()
        {
            PayerMotionConfig = _assetLoader.Load<PlayerMotionConfig>("PlayerMotionConfig");
            PlayerActionConfig = _assetLoader.Load<PlayerActionConfig>("PlayerActionConfig");
        
            if (PayerMotionConfig == null)
            {
                throw new System.Exception("PlayerMotionConfig not found");
            }
            
            if (PlayerActionConfig == null)
            {
                throw new System.Exception("PlayerActionConfig not found");
            }
        }
    }
}
