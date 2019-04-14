using System.Collections;
using System.Collections.Generic;
using TankGame.Flow;
using UnityEngine;

namespace TankGame.Game
{
	public class GameFlow : IFlow
	{
		private GameControllerFactory gameControllerFactory;

		public GameFlow(GameControllerFactory gameControllerFactory)
		{
			this.gameControllerFactory = gameControllerFactory;
		}

		public void Entered()
		{

		}

		public void Ended()
		{

		}

		public IEnumerator Flow()
		{
			var game = gameControllerFactory.GetGame();

			yield return game.Run();

		}

	}

}
