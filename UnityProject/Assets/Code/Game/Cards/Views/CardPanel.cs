using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame.Game
{
	public class CardPanel : MonoBehaviour
	{
		[SerializeField] private Text cardNameText;
		[SerializeField] private Text destroyCostText;
		[SerializeField] private Image cardImage;
		[SerializeField] private Text destroyEffectText;
		[SerializeField] private Text endTurnEffectText;

		public CardData Card { get; private set; }

		public event Action OnMouseUp = delegate { };

		public void OnMouseUpTriggered()
		{
			OnMouseUp();
		}

		public void Initialize(CardData card)
		{
			Card = card;
			cardNameText.text = card.displayName;
			destroyCostText.text = card.powerCost.GetDescription();
			cardImage.sprite = card.sprite;
			destroyEffectText.text = card.destroyEffect.GetDescription(CardEffectType.ON_DESTROY);
			endTurnEffectText.text = card.endTurnEffect.GetDescription(CardEffectType.ON_END_TURN);
		}
	}
}