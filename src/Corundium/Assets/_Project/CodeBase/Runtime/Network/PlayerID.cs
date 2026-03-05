using Mirror;

namespace DevFuckers._Project.CodeBase.Runtime.Network
{
	/// <summary>
	/// Network message carrying a player's identifier assigned on connection.
	/// </summary>
	public struct PlayerID : NetworkMessage
	{
		public int ID;
	}
}