using System;
using UnityEngine;

namespace TankGame.Game
{
	[Serializable]
	public class CrewMemberState : ICloneable
	{
		public string memberId;
		public int maxFatigue;
		public int fatigue;
		public Sprite portrait;

		public bool HasActed { get; set; }
		public TankSection TankPart { get; set; }

		public object Clone()
		{
			return new CrewMemberState
			{
				memberId = memberId,
				maxFatigue = maxFatigue,
				fatigue = fatigue,
				portrait = portrait
			};
		}
	}
}