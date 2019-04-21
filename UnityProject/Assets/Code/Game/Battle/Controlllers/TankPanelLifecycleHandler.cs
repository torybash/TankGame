using System;
using TankGame.Lifecycles;

namespace TankGame.Game
{
	public class TankPanelLifecycleHandler : LifecycleHandler<TankPanel>
	{
		private TankPanel tankPanel; //TODO Should have controller for each instance 

		public override void InstanceAwake(TankPanel instance)
		{
			tankPanel = instance;
		}

		public void Initialize(BattleState battleState)
		{
			tankPanel.Initialize(battleState.gameState);
		}

		public void UpdateState(BattleState battleState)
		{
			tankPanel.UpdateState(battleState.gameState);
		}
	}
}