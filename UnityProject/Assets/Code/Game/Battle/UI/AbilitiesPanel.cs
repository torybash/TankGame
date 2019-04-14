using System.Collections.Generic;
using TankGame.Lifecycles;
using UnityEngine;

namespace TankGame.Game
{
	public class AbilitiesPanel : Lifecycle<AbilitiesPanelLifecycleHandler>
	{
		[SerializeField] private List<TankSection> tankSectionPanels;
	}
}