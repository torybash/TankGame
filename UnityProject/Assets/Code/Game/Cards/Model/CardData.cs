using System;
using UnityEngine;


namespace TankGame.Game
{
	[Serializable]
	public class CardData : ICloneable
	{
		public string id;
		public string displayName;
		public Sprite sprite;
		public PowerChange powerCost;
		public CardEffect destroyEffect;
		public CardEffect endTurnEffect;

		public string Guid { get; private set; }
		public int CardPosition { get; set; }

		public void Initialize()
		{
			Guid = System.Guid.NewGuid().ToString();
		}

		public object Clone()
		{
			return new CardData
			{
				id = id,
				displayName = displayName,
				sprite = sprite,
				powerCost = powerCost,
				destroyEffect = destroyEffect,
				endTurnEffect = endTurnEffect
			};
		}
	}
}