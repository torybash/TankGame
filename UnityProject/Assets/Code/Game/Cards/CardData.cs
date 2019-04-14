using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TankGame.Game
{
	[Serializable]
	public class CardData
	{
		public string id;
		public string displayName;
		public Sprite sprite;
		public PowerChange powerCost;
		public CardEffect destroyEffect;
		public CardEffect endTurnEffect;
	}
}