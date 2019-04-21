using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TankGame.Views;
using UnityEngine;

namespace TankGame.Game
{
	public class BattleHUD
	{
		private readonly ViewController viewController;
		private readonly ActiveCardsPanelLifecycleHandler activeCardsPanelLifecycleHandler;
		private readonly AbilitiesPanelLifecycleHandler abilitiesPanelLifecycleHandler;
		private readonly TankPanelLifecycleHandler tankPanelLifecycleHandler;
		private readonly DragAndDropArrowController dragAndDropArrow;

		public event Action OnEndTurn = delegate { };
		public event Action<TankAbility, CardData> OnResolveAbility = delegate { };

		private BattleState battleState;

		private TankAbility pressedAbility; //TODO This should not be stored here!


		public BattleHUD(ViewController viewController, ActiveCardsPanelLifecycleHandler activeCardsPanelLifecycleHandler, AbilitiesPanelLifecycleHandler abilitiesPanelLifecycleHandler, TankPanelLifecycleHandler tankPanelLifecycleHandler, DragAndDropArrowController dragAndDropArrow)
		{
			this.viewController = viewController;
			this.activeCardsPanelLifecycleHandler = activeCardsPanelLifecycleHandler;
			this.abilitiesPanelLifecycleHandler = abilitiesPanelLifecycleHandler;
			this.tankPanelLifecycleHandler = tankPanelLifecycleHandler;
			this.dragAndDropArrow = dragAndDropArrow;
		}

		public void Run(BattleState battleState)
		{
			this.battleState = battleState;
			abilitiesPanelLifecycleHandler.OnHoverAbility += OnHoverAbility;
			abilitiesPanelLifecycleHandler.OnPressedAbility += OnPressedAbility;
			activeCardsPanelLifecycleHandler.OnReleasedOnCard += OnReleasedOnCard;

			var battleView = viewController.ShowView<BattleView>();
			battleView.OnEndTurn += () => OnEndTurn();

			activeCardsPanelLifecycleHandler.Initialize(battleState);
			abilitiesPanelLifecycleHandler.Initialize(battleState.gameState);
			tankPanelLifecycleHandler.Initialize(battleState);
		}

		public void UpdateHUD() 
		{
			activeCardsPanelLifecycleHandler.UpdateActiveCards(battleState);
			abilitiesPanelLifecycleHandler.UpdateAbilities(battleState.gameState.crewMemberStates);
			tankPanelLifecycleHandler.UpdateState(battleState);
		}

		private void OnPressedAbility(TankSectionAbility ability)
		{
			if (battleState.CanUseAbility())
			{
				dragAndDropArrow.StartDrag(ability.CenterPosition);
				pressedAbility = ability.TankAbility;

			}
		}

		private void OnHoverAbility(TankSectionAbility ability)
		{

		}

		private void OnReleasedOnCard(CardPanel card)
		{
			if (battleState.CanTargetCard(pressedAbility, card.Card))
			{
				OnResolveAbility(pressedAbility, card.Card);
			}
		}

		public void ResolveAbility(TankAbility ability, CardData card, Action onComplete)
		{
			//UpdateHUD();
			onComplete();
		}

		public void AnimateStartOfRound(Action onComplete)
		{
			activeCardsPanelLifecycleHandler.AnimateDealCards(battleState, onComplete);
		}

		public void AnimateEndOfRound()
		{
			UpdateHUD();
		}
	}
}