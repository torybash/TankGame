using System;
using TankGame.Lifecycles;

namespace TankGame.Game
{
	public class AbilitiesPanelLifecycleHandler : LifecycleHandler<AbilitiesPanel>
	{
		public override void InstanceAwake(AbilitiesPanel instance)
		{
			

		}

		internal void Initialize(BattleState battleState)
		{
			//throw new NotImplementedException();
		}
	}
}