using DevFuckers._Project.CodeBase.Runtime.Network;

namespace DevFuckers._Project.CodeBase.Runtime.Features.ChooseGameplayScene
{
    public class OnlineSceneSetter
    {
        private CustomNetworkManager _networkManager;

        //[Inject]
        public OnlineSceneSetter(CustomNetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void SetOnlineScene(string name)
        {
            _networkManager.onlineScene = name;
        }
    }
}