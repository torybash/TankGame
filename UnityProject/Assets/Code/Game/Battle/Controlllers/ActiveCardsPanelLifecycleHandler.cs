using System;
using System.Collections;
using System.Collections.Generic;
using TankGame.Lifecycles;
using TankGame.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TankGame.Game
{
	public class ActiveCardsPanelLifecycleHandler : LifecycleHandler<ActiveCardsPanel>
	{
		private ActiveCardsPanel activeCardsPanel; //TODO Should have controller for each instance 

		private ViewController viewController;
		public event Action<CardPanel> OnReleasedOnCard;

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

		public IEnumerator AnimateDealCards(BattleState battleState)
		{
			yield return activeCardsPanel.AnimateDealCards(battleState.activeCards);
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