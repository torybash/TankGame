using System;

namespace TankGame.Game
{
	[Serializable]
	public class CrewMemberState : ICloneable
	{
		public string memberId;
		public int maxFatigue;
		public int fatigue;

		public bool HasActed { get; set; }
		public TankPart TankPart { get; set; }

		public object Clone()
		{
			return new CrewMemberState
			{
				memberId = memberId,
				maxFatigue = maxFatigue,
				fatigue = fatigue
			};
		}
	}
}