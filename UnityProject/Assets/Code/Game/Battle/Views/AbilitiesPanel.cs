using System;
using System.Collections.Generic;
using System.Linq;
using TankGame.Lifecycles;
using TankGame.Views;
using UnityEngine;

namespace TankGame.Game
{
	public class AbilitiesPanel : Lifecycle<AbilitiesPanelLifecycleHandler>
	{
		[SerializeField] private TemplatePool tankSectionPanelPool;

		private List<TankSectionAbilities> abilitySections = new List<TankSectionAbilities>();

		public void InitializeTankParts(GameState gameState) // List<TankPartState> tankPartStates)
		{
			for (int i = 0; i < gameState.tankState.tankSectionState.Count; i++)
			{
				var tankPartState = gameState.tankState.tankSectionState[i];

				var crewMember = gameState.crewMemberStates.FirstOrDefault(x => x.TankPart == tankPartState.tankSection);
				var sectionPanel = tankSectionPanelPool.GetInstance<TankSectionAbilities>();
				sectionPanel.Initialize(tankPartState.tankSection, crewMember);

				abilitySections.Add(sectionPanel);
			}
		}

		public void SetupTankAbility(int slot, TankSection tankPart, TankAbility ability, Action<TankSectionAbility> onHoverAbility, Action<TankSectionAbility> onPressedAbility)
		{
			var tankSection = abilitySections.FirstOrDefault(x => x.TankSection == tankPart);
			tankSection.RegisterAbility(slot, ability, onHoverAbility, onPressedAbility);
		}

		public void UpdateAbilities(List<CrewMemberState> crewMemberStates)
		{
			foreach (var crewMember in crewMemberStates)
			{
				var tankSection = abilitySections.FirstOrDefault(x => x.TankSection == crewMember.TankPart);
				tankSection.UpdateCrewState(crewMember); 
			}
		}
	}
}