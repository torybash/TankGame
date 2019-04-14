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

		public void Entered()
		{
			var mainMenu = mainMenuControllerFactory.GetMainMenu();
			mainMenu.Run();
		}

		public void Ended()
		{

		}


		public IEnumerator Flow()
		{
			Debug.Log("Started!");

			while (true) {
				yield return null;
			}
		}
	}
}