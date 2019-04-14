using System;
using System.Collections;
using TankGame.Views;

namespace TankGame.Game
{
	public class Battle
	{
		private readonly ViewController viewController;
		private readonly BattleState battleState;

		private readonly ActiveCardsPanelLifecycleHandler activeCardsPanelLifecycleHandler;
		private readonly AbilitiesPanelLifecycleHandler abilitiesPanelLifecycleHandler;
		private readonly TankPanelLifecycleHandler tankPanelLifecycleHandler;

		private BattleView battleView;


		public Battle(ViewController viewController, BattleState battleState)
		{
			this.viewController = viewController;
			this.battleState = battleState;

			activeCardsPanelLifecycleHandler = new ActiveCardsPanelLifecycleHandler(viewController);
			abilitiesPanelLifecycleHandler = new AbilitiesPanelLifecycleHandler();
			tankPanelLifecycleHandler = new TankPanelLifecycleHandler();
		}

		public IEnumerator Run()
		{
			battleView = viewController.ShowView<BattleView>();
			battleView.OnEndTurn += OnEndTurn;

			yield return PlayBattle();

			while (battleView.IsVisible())
			{
				yield return null;
			}
		}

		private void OnEndTurn()
		{
			throw new NotImplementedException();
		}

		private IEnumerator PlayBattle()
		{
			activeCardsPanelLifecycleHandler.Initialize(battleState);
			abilitiesPanelLifecycleHandler.Initialize(battleState);
			tankPanelLifecycleHandler.Initialize(battleState);

			const int DEBUG_MAX_ROUND_COUNT = 5;
			while (battleState.round < DEBUG_MAX_ROUND_COUNT)
			{
				switch (battleState.battlePhase)
				{
				case BattlePhase.START_OF_ROUND:
					battleState.DealCards();
					yield return activeCardsPanelLifecycleHandler.AnimateDealCards(battleState.activeCards);
					battleState.battlePhase = BattlePhase.PLAYER_ACTION;
					break;
				case BattlePhase.PLAYER_ACTION:
					break;
				case BattlePhase.END_OF_ROUND:

					battleState.round++;
					break;
				}

				
				yield return null;
			}
		}
	}
}