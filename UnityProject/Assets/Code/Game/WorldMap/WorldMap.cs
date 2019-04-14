using System;
using System.Collections;
using TankGame.Views;

namespace TankGame.Game
{
	public class WorldMap
	{
		//private ViewController viewController;
		//private readonly GameControllerFactory gameControllerFactory;

		//public WorldMap(ViewController viewController, GameControllerFactory gameControllerFactory)
		//{
		//	this.viewController = viewController;
		//	this.gameControllerFactory = gameControllerFactory;
		//}

		//public IEnumerator Run()
		//{
		//	var mapView = viewController.ShowView<WorldMapView>();
		//	mapView.OnDestinationSelected += OnDestinationSelected;

		//	while (mapView.IsVisible())
		//	{
		//		yield return null;
		//	}
		//}

		//private void OnDestinationSelected(string destinationId)
		//{
		//	var battle = gameControllerFactory.GetBattle();
		//	battle.Run();
		//}
	}
}