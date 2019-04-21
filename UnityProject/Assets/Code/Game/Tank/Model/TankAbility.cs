using System;

namespace TankGame.Game
{
	[Serializable]
	public class TankAbility
	{
		public string id;
		public PowerChange power;
		public int fatigueDamage;

		public TankSection TankPart { get; private set; }
		public int Slot { get; private set; }

		internal void SetState(TankSection tankPart, int slot)
		{
			TankPart = tankPart;
			Slot = slot;
		}
	}

}

