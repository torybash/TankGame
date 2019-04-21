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

		private List<TankSection> tankSections = new List<TankSection>();

		public void InitializeTankParts(GameState gameState) // List<TankPartState> tankPartStates)
		{
			for (int i = 0; i < gameState.tankState.tankPartStates.Count; i++)
			{
				var tankPartState = gameState.tankState.tankPartStates[i];

				var crewMember = gameState.crewMemberStates.FirstOrDefault(x => x.TankPart == tankPartState.tankPart);
				var sectionPanel = tankSectionPanelPool.GetInstance<TankSection>();
				sectionPanel.Initialize(tankPartState.tankPart, crewMember);

				tankSections.Add(sectionPanel);
			}
		}

		public void SetupTankAbility(int slot, TankPart tankPart, TankAbility ability, Action<TankSectionAbility> onHoverAbility, Action<TankSectionAbility> onPressedAbility)
		{
			var tankSection = tankSections.FirstOrDefault(x => x.TankPart == tankPart);
			tankSection.RegisterAbility(slot, ability, onHoverAbility, onPressedAbility);
		}

		public void UpdateAbilities(List<CrewMemberState> crewMemberStates)
		{
			foreach (var crewMember in crewMemberStates)
			{
				var tankSection = tankSections.FirstOrDefault(x => x.TankPart == crewMember.TankPart);
				tankSection.UpdateCrewState(crewMember); 
			}
		}
	}
}