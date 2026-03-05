using System;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.EventBus;
using Mirror;
using UnityEngine;
using Zenject;
using Event = CodeBase.Event;
using Random = UnityEngine.Random;

namespace DevFuckers._Project.CodeBase.Runtime.Network
{
    /// <summary>
    /// Custom network manager extending Mirror's NetworkManager to hook into the
    /// game's event system and spawn players with a generated ID.
    /// </summary>
    public class CustomNetworkManager : NetworkManager
    {
        private EventBus _eventBus;

        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        /// <summary>
        /// Called on network manager start. Disables automatic player creation.
        /// </summary>
        public override void Start()
        {
            base.Start();
            autoCreatePlayer = false;
        }

        /// <summary>
        /// Called on server start. Registers handler for player creation.
        /// </summary>
        public override void OnStartServer()
        {
            base.OnStartServer();
            NetworkServer.RegisterHandler<PlayerID>(OnCreateCharacter);
        }

        /// <summary>
        /// Called on client connection. Sends a random player ID to server and signals gameplay start.
        /// </summary>
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
    
        /// <summary>
        /// Handler that creates a character for the connected client and spawns it into the world.
        /// </summary>
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
