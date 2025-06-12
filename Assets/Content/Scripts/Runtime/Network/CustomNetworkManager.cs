using System;
using Mirror;
using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Network
{
    public struct PlayerID : NetworkMessage
    {
        public int ID;
    }

    public class CustomNetworkManager : NetworkManager
    {
        public event Action OnServerStarted = delegate { };

        public void Init()
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
                ID = UnityEngine.Random.Range(1, 1000)
            };

            NetworkClient.Send(randomPlayerID);
            OnServerStarted.Invoke();
        }

        void OnCreateCharacter(NetworkConnectionToClient conn, PlayerID id)
        {
            if (playerPrefab == null)
                throw new NullReferenceException("PLAYER prefab is empty");

            var t = FindObjectOfType<NetworkStartPosition>(); // костыль
            GameObject playerObject;

            if (t != null)
                playerObject = Instantiate(playerPrefab, t.transform.position, Quaternion.identity);
            else
                playerObject = Instantiate(playerPrefab);


            NetworkServer.AddPlayerForConnection(conn, playerObject);
        }
    }
}
