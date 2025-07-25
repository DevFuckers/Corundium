using Mirror;
using UnityEngine;

namespace DevFuckers
{
    public interface IVehicle
    {
        Transform DriverPlace { get; }

        PlayerController GetDriver();
        void SetDriver(PlayerController value);

        NetworkIdentity Identity { get; }
        void EnableControl(IInputHandler inputHandler);
        void DisableControl(IInputHandler inputHandler);
    }
}
