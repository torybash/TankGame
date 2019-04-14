using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TankGame.Lifecycles;
using UnityEngine;

namespace TankGame.Game
{
	public class ActiveCardsPanel : Lifecycle<ActiveCardsPanelLifecycleHandler>
	{

		[SerializeField] private CardPanel cardPanelPrefab;

		[SerializeField] private List<Transform> cardPositions;
		[SerializeField] private Transform deckPosition;

		private List<CardPanel> activeCards = new List<CardPanel>();



		public IEnumerator AnimateDealCards(List<CardData> cards)
		{
			yield return null;

			for (int i = 0; i < cards.Count; i++)
			{
				var card = cards[i];
				var cardPanel = Instantiate(cardPanelPrefab, transform);
				cardPanel.Initialize(card);
				cardPanel.transform.position = cardPositions[i].position;
				activeCards.Add(cardPanel);
			}

			yield break;
		}
	}
}