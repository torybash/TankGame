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

		}

		public IEnumerator AnimateDealCards(List<CardData> cards)
		{
			yield return activeCardsPanel.AnimateDealCards(cards);
		}

	}
}