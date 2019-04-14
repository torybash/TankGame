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

		internal void Initialize(CardData card)
		{
			cardNameText.text = card.displayName;
			destroyCostText.text = card.powerCost.powerType.ToString()[0] + card.powerCost.cost.ToString();
			cardImage.sprite = card.sprite;
			destroyEffectText.text = card.destroyEffect.GetDescription(CardEffectType.ON_DESTROY);
			endTurnEffectText.text = card.endTurnEffect.GetDescription(CardEffectType.ON_END_TURN);
		}
	}
}