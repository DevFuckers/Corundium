using System;
using UnityEngine;
using Zenject;

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