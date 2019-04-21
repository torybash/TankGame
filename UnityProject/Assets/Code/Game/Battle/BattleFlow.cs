using System.Collections;
using TankGame.Flow;
using UnityEngine;

namespace TankGame.Game
{
	public class BattleFlow : IFlow
	{
		private readonly GameControllerFactory gameControllerFactory;
		private readonly BattleState battleState;
		private Battle battle;

		public BattleFlow(GameControllerFactory gameControllerFactory, BattleState battleState)
		{
			this.gameControllerFactory = gameControllerFactory;
			this.battleState = battleState;
		}

		public void Entered()
		{

		}

		public void Ended()
		{

		}


		public IEnumerator Flow()
		{
			battle = gameControllerFactory.GetBattle(battleState);
			battle.Initialize();
			yield return PlayBattle();
		}

		public IEnumerator PlayBattle()
		{
			const int DEBUG_MAX_ROUND_COUNT = 5;
			while (battleState.round < DEBUG_MAX_ROUND_COUNT)
			{
				switch (battleState.battlePhase)
				{
				case BattlePhase.START_OF_ROUND:
					battleState.DealCards();
					battleState.ResetCrew();
					yield return battle.BattleHUD.AnimateStartOfRound(battleState);

					battle.ChangePhase(BattlePhase.PLAYER_ACTION);
					break;
				case BattlePhase.PLAYER_ACTION:
					break;
				case BattlePhase.ANIMATING:
					break;
				case BattlePhase.END_OF_ROUND:
					battleState.ResolveEndOfTurnCards();
					yield return battle.BattleHUD.AnimateEndOfRound(battleState);

					if (battleState.deck.Count == 0)
					{
						Debug.Log("YOU WIN!");
						battle.ChangePhase(BattlePhase.END_OF_BATTLE);
					} else
					{
						battleState.round++;
						battle.ChangePhase(BattlePhase.START_OF_ROUND);
					}

					break;
				case BattlePhase.END_OF_BATTLE:
					break;
				}


				yield return null;
			}
		}
	}

}