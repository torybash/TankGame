using System;

namespace TankGame.Game
{
	[Serializable]
	public class TankAbility
	{
		public string id;
		public PowerChange power;
		public int fatigueDamage;

		public TankPart TankPart { get; private set; }
		public int Slot { get; private set; }

		internal void SetState(TankPart tankPart, int slot)
		{
			TankPart = tankPart;
			Slot = slot;
		}
	}

}

