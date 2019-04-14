using System;
using TankGame.Lifecycles;

namespace TankGame.Game
{
	public class TankPanelLifecycleHandler : LifecycleHandler<TankPanel>
	{
		public override void InstanceAwake(TankPanel instance)
		{
			//throw new System.NotImplementedException();
		}

		internal void Initialize(BattleState battleState)
		{
			//throw new NotImplementedException();
		}
	}
}