using System;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.EventBus;
using Mirror;
using UnityEngine;
using Zenject;
using Event = CodeBase.Event;
using Random = UnityEngine.Random;

namespace DevFuckers._Project.CodeBase.Runtime.Network
{
    public struct PlayerID : NetworkMessage
    {
        public int ID;
    }

    public class CustomNetworkManager : NetworkManager
    {
        private EventBus _eventBus;

        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
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
            _eventBus.Trigger(Event.StartGameplay);
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