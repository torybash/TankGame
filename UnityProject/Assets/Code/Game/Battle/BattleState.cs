using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
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
			foreach (var item in deck)
			{
				Debug.Log("deck card guid: " + item.Guid);

			}

			while (activeCards.Count < 5 && deck.Count > 0)
			{
				var card = deck[deck.Count - 1];
				deck.RemoveAt(deck.Count - 1);

				activeCards.Add(card);
				Debug.Log("activeCard added guid: " + card.Guid);
			}
		}

		public void DestroyCard(CardData card)
		{
			var activeCard = activeCards.FirstOrDefault(x => x.Guid == card.Guid);
			activeCards.Remove(activeCard);

			var destroyEffect = activeCard.destroyEffect;
			if (destroyEffect.cardEffectTarget != CardEffectTarget.NONE)
			{
				if (destroyEffect.healthChange != 0)
				{
					if (destroyEffect.cardEffectTarget == CardEffectTarget.PLAYER_TANK)
					{
						gameState.tankState.hullHp = Mathf.Clamp(gameState.tankState.hullHp + destroyEffect.healthChange, 0, gameState.tankState.maxHp);
					}else if (destroyEffect.cardEffectTarget == CardEffectTarget.TANK_PART)
					{
						DamageTankPart(destroyEffect.tankPart, destroyEffect.healthChange);
					}else if (destroyEffect.cardEffectTarget == CardEffectTarget.RANDOM_TANK_PART)
					{
						var targetTypes = typeof(TankPart).GetEnumValues().Cast<TankPart>();
						var randomPart = targetTypes.OrderBy(x => UnityEngine.Random.Range(0, int.MaxValue)).FirstOrDefault();
						DamageTankPart(randomPart, destroyEffect.healthChange);
					}
				}
			}
		}

		private void DamageTankPart(TankPart part, int healthChange)
		{
			var partState = gameState.tankState.tankPartStates.FirstOrDefault(x => x.tankPart == part);
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