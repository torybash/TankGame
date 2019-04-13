using UnityEngine;
using System.Collections;
using TankGame.Flow;

namespace TankGame.MainMenu
{
	public class MainMenuFlow : IFlow
	{
		private MainMenuControllerFactory mainMenuControllerFactory;


		public MainMenuFlow(MainMenuControllerFactory mainMenuControllerFactory)
		{
			this.mainMenuControllerFactory = mainMenuControllerFactory;
		}

		public IEnumerator Flow()
		{
			Debug.Log("Started!");

			var mainMenu = mainMenuControllerFactory.GetMainMenu();

			yield return mainMenu.Run();

			Debug.Log("Finished!");
		}
	}
}