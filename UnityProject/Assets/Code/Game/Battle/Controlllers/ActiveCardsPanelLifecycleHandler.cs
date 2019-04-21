using System;
using TankGame.Lifecycles;
using TankGame.Views;
using UnityEngine;

namespace TankGame.Game
{
	public class ActiveCardsPanelLifecycleHandler : LifecycleHandler<ActiveCardsPanel>
	{
		private ActiveCardsPanel activeCardsPanel; //TODO Should have controller for each instance 

		private ViewController viewController;
		public event Action<CardPanel> OnReleasedOnCard;

		private Routine routine = new Routine();

		public ActiveCardsPanelLifecycleHandler(ViewController viewController)
		{
			this.viewController = viewController;
		}

		public override void InstanceAwake(ActiveCardsPanel instance)
		{
			activeCardsPanel = instance;
		}

		public void Initialize(BattleState battleState)
		{
			activeCardsPanel.UpdateDeckSize(battleState.deck.Count);
		}

		public void AnimateDealCards(BattleState battleState, Action onComplete)
		{
			routine = new Routine();
			routine.Start(
				activeCardsPanel.AnimateDealCards(battleState.activeCards)
			);

			routine.OnComplete += onComplete;
		}

		public void UpdateActiveCards(BattleState battleState)
		{
			activeCardsPanel.UpdateDeckSize(battleState.deck.Count);
			activeCardsPanel.UpdateActiveCards(battleState.activeCards);

			foreach (var card in battleState.activeCards)
			{
				activeCardsPanel.RegisterMouseUp(card, OnReleasedOnCard);
			}
		}
	}
}