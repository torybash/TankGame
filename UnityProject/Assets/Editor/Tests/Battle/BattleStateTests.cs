using System.Collections.Generic;
using NUnit.Framework;
using TankGame.Databases;
using TankGame.Game;

namespace Tests
{
	public class BattleStateTests
    {
        [Test]
        public void DealCards_SampleBattle_CorrentAmountActiveCards()
        {
			var gameState = GetSampleGameState();
			var battleState = GetSampleTestState(gameState);

			battleState.DealCards();

			Assert.AreEqual(battleState.activeCards.Count, 5);
		}

		[Test]
		public void CanTargetCard_SampleBattlCardTargeteValid_CanTarget()
		{
			var gameState = GetSampleGameState();
			var battleState = GetSampleTestState(gameState, BattlePhase.PLAYER_ACTION);
			var ability = GetSampleAbility();
			var card = GetSampleCard();

			bool canTarget = battleState.CanTargetCard(ability, card);	

			Assert.IsTrue(canTarget);
		}

		[Test]
		public void CanTargetCard_SampleBattleCardTargetWrongPower_CanTarget()
		{
			var gameState = GetSampleGameState();
			var battleState = GetSampleTestState(gameState, BattlePhase.PLAYER_ACTION);
			var ability = GetSampleAbility(PowerType.DRIVE);
			var card = GetSampleCard(PowerType.GUN);

			bool canTarget = battleState.CanTargetCard(ability, card);

			Assert.IsFalse(canTarget);
		}

		private GameState GetSampleGameState()
		{
			var tankDatabase = DatabaseHelper.Instance.Get<TankDatabase>();
			var crewDatabase = DatabaseHelper.Instance.Get<CrewDatabase>();

			var testState = new GameState
			{
				tankState = tankDatabase.GetTankState("Test"),
				crewMemberStates = crewDatabase.GetCrew("Test")
			};
			for (int i = 0; i < testState.tankState.tankSectionState.Count; i++)
			{
				var partState = testState.tankState.tankSectionState[i];
				testState.crewMemberStates[i].TankPart = partState.tankSection;
			}
			return testState;
		}

		private BattleState GetSampleTestState(GameState gameState, BattlePhase phase = BattlePhase.START_OF_ROUND)
		{
			var cardsDatabase = DatabaseHelper.Instance.Get<CardsDatabase>();

			var battleState = new BattleState
			{
				round = 0,
				battlePhase = phase,
				deck = cardsDatabase.GetRandomCards(20),
				activeCards = new List<CardData>(),
				gameState = gameState

			};
			return battleState;
		}


		private CardData GetSampleCard(PowerType powerType = PowerType.GUN, int cost = 1)
		{
			return new CardData
			{
				id = "sample",
				CardPosition = 0,
				powerCost = new PowerChange { cost = cost, powerType = powerType },
				destroyEffect = new CardEffect { cardEffectTarget = CardEffectTarget.PLAYER_TANK, healthChange = 2 },
				endTurnEffect = new CardEffect { cardEffectTarget = CardEffectTarget.PLAYER_TANK, healthChange = -2 },
			};
		}

		private TankAbility GetSampleAbility(PowerType powerType = PowerType.GUN, int cost = 1)
		{
			return new TankAbility
			{
				id = "sample",
				fatigueDamage = 1,
				power = new PowerChange { cost = cost, powerType = powerType },
			};
		}
	}
}
