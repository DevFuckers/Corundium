using System;
using DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.GameStateMachine;
using DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.GameStateMachine.States;
using Mirror;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace DevFuckers._Project.CodeBase.Runtime.Network
{
    public struct PlayerID : NetworkMessage
    {
        public int ID;
    }

    public class CustomNetworkManager : NetworkManager
    {
        [Inject] private GameStateMachine _stateMachine;
        [Inject] private DiContainer _container;

        public override void Start()
        {
            base.Start();
            autoCreatePlayer = false;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            NetworkServer.RegisterHandler<PlayerID>(OnCreateCharacter);
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();

            var randomPlayerID = new PlayerID()
            {
                ID = Random.Range(1, 1000)
            };

            NetworkClient.Send(randomPlayerID);
            _stateMachine.EnterIn<GamePlayLoopState>();
        }
    
        void OnCreateCharacter(NetworkConnectionToClient conn, PlayerID id)
        {
            if (playerPrefab == null)
                throw new NullReferenceException("PLAYER prefab is empty");

            var t = FindFirstObjectByType<NetworkStartPosition>(); // костыль
            GameObject playerObject;
        
        
            if(t != null)
                playerObject = Instantiate(playerPrefab, t.transform.position, Quaternion.identity); 
            else
                playerObject = Instantiate(playerPrefab); 
        
            NetworkServer.AddPlayerForConnection(conn, playerObject);
        }
    }
}