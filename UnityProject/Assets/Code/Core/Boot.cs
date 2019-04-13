using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : MonoBehaviour
{
    void Start()
    {
		BootGame();        
    }

	private void BootGame()
	{
		//Setup controllers, databases, flow-stack etc.
		var screenController = new ScreenController();
		var flowStack = new FlowStack(screenController);

		//Start Flow
		var menuFlow = new MainMenuFlow();
		flowStack.Push(menuFlow);
	}
}
