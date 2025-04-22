using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.AssetProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.SO;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide
{
    public class ConfigProvider
    {
        private IAssetLoader _assetLoader;

        public ConfigProvider(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        public PlayerMotionConfig PayerMotionConfig {get; private set; }

        public void LoadAll()
        {
            PayerMotionConfig = _assetLoader.Load<PlayerMotionConfig>("PlayerMotionConfig");
        
            if (PayerMotionConfig == null)
            {
                throw new System.Exception("PlayerMotionConfig not found");
            }
        }
    }
}
