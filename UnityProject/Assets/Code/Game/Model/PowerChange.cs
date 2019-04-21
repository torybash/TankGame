using System;

namespace TankGame.Game
{
	[Serializable]
	public class PowerChange
	{
		public PowerType powerType;
		public int cost;

		public string GetDescription()
		{
			return powerType.ToString()[0] + cost.ToString(); //TODO Should use sprite instead
		}
	}
}