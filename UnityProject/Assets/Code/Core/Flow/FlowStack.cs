﻿using System.Collections.Generic;
using TankGame.Screen;

namespace TankGame.Flow
{
	public class FlowStack
	{
		private readonly ScreenController screenController;
		private Stack<FlowRoutine> flows = new Stack<FlowRoutine>();

		public FlowStack(ScreenController screenController)
		{
			this.screenController = screenController;
		}

		public void Push(IFlow flow)
		{
			if (flows.Count > 0)
			{
				var currentFlow = flows.Peek();
				currentFlow.routine.Pause();
				currentFlow.flow.Ended();
			}
			var flowRoutine = new FlowRoutine(flow);
			flows.Push(flowRoutine);
			flowRoutine.routine.finished += FlowFinished;
		}

		private void FlowFinished(bool manual)
		{
			flows.Pop();
			if (flows.Count > 0)
			{
				var previousFlow = flows.Peek();
				previousFlow.routine.Unpause();
				previousFlow.flow.Entered();
			}
		}

	}
}