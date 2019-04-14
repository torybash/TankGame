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

		internal void Initialize(BattleState battleState)
		{
			//throw new NotImplementedException();
		}
	}
}