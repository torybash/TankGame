using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TankGame.Lifecycles;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame.Game
{
	public class ActiveCardsPanel : Lifecycle<ActiveCardsPanelLifecycleHandler>
	{

		[SerializeField] private CardPanel cardPanelPrefab;

		[SerializeField] private List<Transform> cardPositions;
		[SerializeField] private Transform deckPosition;
		[SerializeField] private Text deckSizeText;

		private List<CardPanel> activeCardPanels = new List<CardPanel>();

		public IEnumerator AnimateDealCards(List<CardData> cards)
		{
			yield return null;

			UpdateActiveCards(cards);
			foreach (var card in activeCardPanels)
			{
				card.transform.position = deckPosition.position;
			}

			var sequence = DOTween.Sequence();
			const float INTERVAL = 0.2f;
			const float DURATION = 0.4f;
			float delayTime = 0f;
			foreach (var card in activeCardPanels)
			{
				sequence.Insert(delayTime, card.transform.DOMove(cardPositions[card.Card.CardPosition].position, DURATION));
				delayTime += INTERVAL;
			}
			yield return sequence.WaitForCompletion();
		}

		public void RegisterMouseUp(CardData card, Action<CardPanel> onReleasedOnCard)
		{
			var cardPanel = activeCardPanels.FirstOrDefault(x => x.Card.Guid == card.Guid);
			cardPanel.OnMouseUp += () => onReleasedOnCard(cardPanel);
		}

		public void UpdateDeckSize(int count)
		{
			deckSizeText.text = string.Format("{0} Cards", count.ToString());
		}

		public void UpdateActiveCards(List<CardData> cards)
		{
			foreach (var cardPanel in activeCardPanels)
			{
				Destroy(cardPanel.gameObject); //TODO Use pool instead!
			}
			activeCardPanels.Clear();

			foreach (var card in cards)
			{
				var cardPanel = Instantiate(cardPanelPrefab, transform);
				cardPanel.Initialize(card); 
				cardPanel.transform.position = cardPositions[card.CardPosition].position;
				activeCardPanels.Add(cardPanel);
			}
		}
	}
}