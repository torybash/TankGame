using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace TankGame.Game
{
	public class BattleState
	{
		public int round;
		public BattlePhase battlePhase;
		public List<CardData> deck;
		public List<CardData> activeCards;
		public GameState gameState;

		public void DealCards()
		{
			while (activeCards.Count < 5 && deck.Count > 0)
			{
				var card = deck[deck.Count - 1];
				activeCards.Add(card);
				deck.RemoveAt(deck.Count - 1);
			}
		}
	}
}