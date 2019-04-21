using UnityEngine;

namespace TankGame.Game
{
	public class Battle
	{
		private readonly BattleState battleState;
		private readonly TankDatabase tankDatabase;
		private readonly BattleHUD battleHUD;

		public BattleHUD BattleHUD {
			get { return battleHUD; }
		}

		public Battle(BattleState battleState, TankDatabase tankDatabase, BattleHUD battleHUD)
		{
			this.battleState = battleState;
			this.tankDatabase = tankDatabase;
			this.battleHUD = battleHUD;
		}

		public void Initialize()
		{
			battleHUD.Run(battleState);
			battleHUD.OnEndTurn += OnEndTurn;
			battleHUD.OnResolveAbility += OnResolveAbility;

			ChangePhase(BattlePhase.START_OF_ROUND);
		}

		private void OnResolveAbility(TankAbility ability, CardData card)
		{
			Debug.Log("OnResolveAbility - ability: " + ability.id + ", card: " + card.id);

			battleState.SpentSectionTurn(ability);
			battleState.DestroyCard(card);

			battleHUD.ResolveAbility(ability, card, () => {
				battleHUD.UpdateHUD();
				ChangePhase(BattlePhase.PLAYER_ACTION);
			});
		}

		private void OnEndTurn()
		{
			if (battleState.battlePhase != BattlePhase.PLAYER_ACTION) return;

			ChangePhase(BattlePhase.END_OF_ROUND);
		}

		public void ChangePhase(BattlePhase battlePhase)
		{
			Debug.Log("ChangePhase: " + battlePhase + " was: " + battleState.battlePhase);
			battleState.battlePhase = battlePhase;

			switch (battlePhase)
			{
			case BattlePhase.START_OF_ROUND:
				battleState.DealCards();
				battleState.ResetCrew();
				BattleHUD.AnimateStartOfRound(() =>
				{
					ChangePhase(BattlePhase.PLAYER_ACTION);
					BattleHUD.UpdateHUD();
				});
				break;
			case BattlePhase.PLAYER_ACTION:
				break;
			case BattlePhase.END_OF_ROUND:
				battleState.ResolveEndOfTurnCards();
				BattleHUD.AnimateEndOfRound();

				if (battleState.deck.Count == 0)
				{
					Debug.Log("YOU WIN!");
					ChangePhase(BattlePhase.END_OF_BATTLE);
				} else
				{
					battleState.round++;
					ChangePhase(BattlePhase.START_OF_ROUND);
				}
				break;
			case BattlePhase.END_OF_BATTLE:
				break;
			default:
				break;
			}
		}
	}
}