using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TankGame.Lifecycles;
using UnityEngine;

namespace TankGame.Game
{
	public class ActiveCardsPanel : Lifecycle<ActiveCardsPanelLifecycleHandler>
	{

		[SerializeField] private CardPanel cardPanelPrefab;

		[SerializeField] private List<Transform> cardPositions;
		[SerializeField] private Transform deckPosition;

		private List<CardPanel> activeCardPanels = new List<CardPanel>();



		public IEnumerator AnimateDealCards(List<CardData> cards)
		{
			yield return null;

			UpdateActiveCards(cards);

			yield break;
		}

		public void RegisterMouseUp(CardData card, Action<CardPanel> onReleasedOnCard)
		{
			var cardPanel = activeCardPanels.FirstOrDefault(x => x.Card.Guid == card.Guid);
			cardPanel.OnMouseUp += () => onReleasedOnCard(cardPanel);
		}

		public void UpdateActiveCards(List<CardData> cards)
		{
			foreach (var cardPanel in activeCardPanels)
			{
				Destroy(cardPanel.gameObject); //TODO Use pool instead!
			}
			activeCardPanels.Clear();

			for (int i = 0; i < cards.Count; i++)
			{
				var card = cards[i];
				var cardPanel = Instantiate(cardPanelPrefab, transform);
				cardPanel.Initialize(card); 
				cardPanel.transform.position = cardPositions[i].position;
				activeCardPanels.Add(cardPanel);
			}
		}
	}
}