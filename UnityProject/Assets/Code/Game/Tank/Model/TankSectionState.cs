using System;
using System.Collections.Generic;

namespace TankGame.Game
{
	[Serializable]
	public class TankSectionState : ICloneable
	{
		public TankSection tankSection;
		public int maxHealth;
		public int health;
		public List<string> abilityIds;

		public object Clone()
		{
			return new TankSectionState
			{
				tankSection = tankSection,
				maxHealth = maxHealth,
				health = health,
				abilityIds = abilityIds.Clone()
			};
		}
	}
}