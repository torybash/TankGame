using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
				deck.RemoveAt(deck.Count - 1);

				card.CardPosition = activeCards.Count;

				activeCards.Add(card);
			}
		}

		public void DestroyCard(CardData card)
		{
			var activeCard = activeCards.FirstOrDefault(x => x.Guid == card.Guid);
			activeCards.Remove(activeCard);

			var destroyEffect = activeCard.destroyEffect;
			if (destroyEffect.cardEffectTarget != CardEffectTarget.NONE)
			{
				ResolveCardEffect(destroyEffect);
			}
		}

		private void ResolveCardEffect(CardEffect cardEffect)
		{
			if (cardEffect.healthChange != 0)
			{
				if (cardEffect.cardEffectTarget == CardEffectTarget.PLAYER_TANK)
				{
					gameState.tankState.hullHp = Mathf.Clamp(gameState.tankState.hullHp + cardEffect.healthChange, 0, gameState.tankState.maxHp);
				} else if (cardEffect.cardEffectTarget == CardEffectTarget.TANK_PART)
				{
					DamageTankPart(cardEffect.tankPart, cardEffect.healthChange);
				} else if (cardEffect.cardEffectTarget == CardEffectTarget.RANDOM_TANK_PART)
				{
					var targetTypes = typeof(TankSection).GetEnumValues().Cast<TankSection>();
					var randomPart = targetTypes.OrderBy(x => UnityEngine.Random.Range(0, int.MaxValue)).FirstOrDefault();
					DamageTankPart(randomPart, cardEffect.healthChange);
				}
			}
		}

		public void ResolveEndOfTurnCards()
		{
			var cards = new List<CardData>(activeCards);
			foreach (var card in cards)
			{
				if (card.endTurnEffect.cardEffectTarget != CardEffectTarget.NONE)
				{
					ResolveCardEffect(card.endTurnEffect);
				}
				activeCards.Remove(card);
			}
		}

		private void DamageTankPart(TankSection part, int healthChange)
		{
			var partState = gameState.tankState.tankSectionState.FirstOrDefault(x => x.tankSection == part);
			partState.health = Mathf.Clamp(partState.health + healthChange, 0, partState.maxHealth);
			gameState.tankState.hullHp = Mathf.Clamp(gameState.tankState.hullHp + healthChange, 0, gameState.tankState.maxHp);
		}

		public void SpentSectionTurn(TankAbility tankAbility)
		{
			var crewMember = gameState.crewMemberStates.FirstOrDefault(x => x.TankPart == tankAbility.TankPart);
			crewMember.HasActed = true;
			crewMember.fatigue = Mathf.Clamp(crewMember.fatigue + tankAbility.fatigueDamage, 0, crewMember.maxFatigue);
		}

		public void ResetCrew()
		{
			foreach (var crewMember in gameState.crewMemberStates)
			{
				crewMember.HasActed = false;
			}
		}
	}
}