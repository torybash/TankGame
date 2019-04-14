using System;
using System.Collections.Generic;

namespace TankGame.Game
{
	[Serializable]
	public class TankPartState : ICloneable
	{
		public TankPart tankPart;
		public int maxHealth;
		public int health;
		public List<string> abilityIds;

		public object Clone()
		{
			return new TankPartState
			{
				tankPart = tankPart,
				maxHealth = maxHealth,
				health = health,
				abilityIds = abilityIds.Clone()
			};
		}
	}
}