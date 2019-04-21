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
			//Start Battle
			battle = gameControllerFactory.GetBattle(battleState);
			battle.Initialize();
			while (battleState.battlePhase != BattlePhase.END_OF_BATTLE)
			{
				yield return null;
			}

			//After-battle end screen
			//TODO
		}


	}

}