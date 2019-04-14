using System;
using UnityEngine;

namespace TankGame.Game
{
	[Serializable]
	public class CardEffect
	{
		public CardEffectTarget cardEffectTarget;
		public TankPart tankPart;
		public PowerChange powerChange;
		public int healthChange;

		public string GetDescription(CardEffectType cardEffectType)
		{
			string description = "";
			
			if (healthChange != 0) { 

				description = cardEffectType == CardEffectType.ON_DESTROY ? "On destroy: " : "On end turn: ";
				description += (healthChange > 0 ? "Repair " : "Deals ") + Mathf.Abs(healthChange) + " damage to ";

				if (cardEffectTarget == CardEffectTarget.TANK_PART)
				{
					description += tankPart;
				} else if (cardEffectTarget == CardEffectTarget.RANDOM_TANK_PART)
				{
					description += " random section";
				} else if (cardEffectTarget == CardEffectTarget.PLAYER_TANK)
				{
					description += " tank";
				}
			}

			return description;
		}
	}
}