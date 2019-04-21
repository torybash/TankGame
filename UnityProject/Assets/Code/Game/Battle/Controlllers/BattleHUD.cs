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
		public event Action<TankSectionAbility, CardPanel> OnResolveAbility = delegate { };

		private Routine dragAndDropRoutine = new Routine();
		private BattleState battleState;

		private CardPanel card; //TODO Should be stored inside routine/class instead!

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
			if (CanUseAbility(ability))
			{
				dragAndDropRoutine.Start(DragAndDropAbilty(ability));
			}
		}

		private IEnumerator DragAndDropAbilty(TankSectionAbility ability)
		{
			card = null;

			dragAndDropArrow.StartedDrag(ability.CenterPosition);

			while (Input.GetMouseButton(0) || card != null)
			{
				var viewCamera = viewController.GetViewCamera();
				var mouseWorldPosition = viewCamera.ScreenToWorldPoint(Input.mousePosition);
				dragAndDropArrow.UpdateDrag(mouseWorldPosition);

				if (CanTargetCard(ability, card))
				{
					OnResolveAbility(ability, card);
					card = null;
				}
				if (card != null) break;
				
				yield return null;
			}

			dragAndDropArrow.EndedDrag();
		}

		private void OnHoverAbility(TankSectionAbility ability)
		{

		}

		private void OnReleasedOnCard(CardPanel card)
		{
			this.card = card;
			//Debug.Log("OnReleasedOnCard - card: " + card.Card.id + ", guid: " + card.Card.Guid);
		}

		public IEnumerator AnimateStartOfRound(BattleState battleState)
		{
			yield return activeCardsPanelLifecycleHandler.AnimateDealCards(battleState);

			UpdateHUD();
		}

		public IEnumerator AnimateEndOfRound(BattleState battleState)
		{
			UpdateHUD();

			yield break;
		}

		private bool CanUseAbility(TankSectionAbility ability)
		{
			if (dragAndDropRoutine.IsRunning) return false;
			if (battleState.battlePhase != BattlePhase.PLAYER_ACTION) return false;

			return true;
		}

		private bool CanTargetCard(TankSectionAbility ability, CardPanel card)
		{
			if (card == null) return false;
			if (battleState.battlePhase != BattlePhase.PLAYER_ACTION) return false;
			if (ability.TankAbility.power.powerType != card.Card.powerCost.powerType) return false;
			if (ability.TankAbility.power.cost < card.Card.powerCost.cost) return false;
			var crewMember = battleState.gameState.crewMemberStates.FirstOrDefault(x => x.TankPart == ability.TankAbility.TankPart);
			if (crewMember == null || crewMember.HasActed) return false;
			return true;
		}
	}
}