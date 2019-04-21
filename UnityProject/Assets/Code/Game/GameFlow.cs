using System.Collections;
using System.Collections.Generic;
using TankGame.Flow;
using UnityEngine;

namespace TankGame.Game
{
	public class GameFlow : IFlow
	{
		private readonly GameControllerFactory gameControllerFactory;

		private WorldMap worldMap;

		public GameFlow(GameControllerFactory gameControllerFactory)
		{
			this.gameControllerFactory = gameControllerFactory;
		}

		public void Entered()
		{
			worldMap = gameControllerFactory.GetWorldMap();
			worldMap.Run();
		}

		public void Ended()
		{

		}

		public IEnumerator Flow()
		{
			//
			while (worldMap.IsSelectingDestination)
			{
				yield return null;
			}
		}

	}

}
