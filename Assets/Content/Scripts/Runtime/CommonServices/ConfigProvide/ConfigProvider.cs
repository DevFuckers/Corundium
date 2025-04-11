using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.AssetProvide;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide
{
    public class ConfigProvider
    {
        private IAssetLoader _assetLoader;

        public ConfigProvider(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }
    }
}
