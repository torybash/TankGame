using System;
using System.Collections;
using TankGame.Views;
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
		}

		private void OnResolveAbility(TankSectionAbility ability, CardPanel card)
		{
			//Debug.Log("OnResolveAbility - ability: " + ability.TankAbility.id + ", card: " + card.Card.id);

			battleState.SpentSectionTurn(ability.TankAbility);
			battleState.DestroyCard(card.Card);

			//ChangePhase(BattlePhase.ANIMATING);
			battleHUD.UpdateHUD(); //TODO Should be handled with animations instead!!
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
		}
	}
}