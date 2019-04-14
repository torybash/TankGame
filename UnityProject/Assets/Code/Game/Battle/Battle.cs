using System;
using System.Collections;
using TankGame.Views;
using UnityEngine;

namespace TankGame.Game
{
	public class Battle
	{
		private readonly ViewController viewController;
		private readonly BattleState battleState;
		private readonly TankDatabase tankDatabase;
		private readonly BattleHUD battleHUD;

		private BattleView battleView;


		public Battle(ViewController viewController, BattleState battleState, TankDatabase tankDatabase, BattleHUD battleHUD)
		{
			this.viewController = viewController;
			this.battleState = battleState;
			this.tankDatabase = tankDatabase;
			this.battleHUD = battleHUD;
		}

		public IEnumerator Run()
		{
			battleHUD.Run(battleState);
			battleHUD.OnEndTurn += OnEndTurn;
			battleHUD.OnResolveAbility += OnResolveAbility;

			yield return PlayBattle();

			while (battleView.IsVisible())
			{
				yield return null;
			}
		}

		private void OnResolveAbility(TankSectionAbility ability, CardPanel card)
		{
			Debug.Log("OnResolveAbility - ability: " + ability.TankAbility.id + ", card: " + card.Card.id);

			battleState.SpentSectionTurn(ability.TankAbility);
			battleState.DestroyCard(card.Card);

			ChangePhase(BattlePhase.RESOLVING_PLAYER_ACTION);

			battleHUD.UpdateHUD(); //TODO Should be handled with animations instead!!

			ChangePhase(BattlePhase.PLAYER_ACTION);
		}

		private void OnEndTurn()
		{
			if (battleState.battlePhase != BattlePhase.PLAYER_ACTION) return;

			ChangePhase(BattlePhase.END_OF_ROUND);
		}

		private void ChangePhase(BattlePhase battlePhase)
		{
			battleState.battlePhase = battlePhase;
		}

		private IEnumerator PlayBattle()
		{

			const int DEBUG_MAX_ROUND_COUNT = 5;
			while (battleState.round < DEBUG_MAX_ROUND_COUNT)
			{
				switch (battleState.battlePhase)
				{
				case BattlePhase.START_OF_ROUND:
					battleState.DealCards();
					battleState.ResetCrew();
					yield return battleHUD.AnimateBattleStart(battleState);
					ChangePhase(BattlePhase.PLAYER_ACTION);
					break;
				case BattlePhase.PLAYER_ACTION:
					break;
				case BattlePhase.RESOLVING_PLAYER_ACTION:
					break;
				case BattlePhase.END_OF_ROUND:

					battleState.round++;
					ChangePhase(BattlePhase.START_OF_ROUND);
					break;
				}

				
				yield return null;
			}
		}
	}
}