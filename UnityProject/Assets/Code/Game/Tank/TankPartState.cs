using System;

namespace TankGame.Game
{
	[Serializable]
	public class TankPartState : ICloneable
	{
		public TankPart tankPart;
		public int maxHealth;
		public int health;

		public object Clone()
		{
			return new TankPartState
			{
				tankPart = tankPart,
				maxHealth = maxHealth,
				health = health
			};
		}
	}
}