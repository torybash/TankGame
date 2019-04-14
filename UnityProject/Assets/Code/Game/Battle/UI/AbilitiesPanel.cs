using System;
using System.Collections.Generic;
using System.Linq;
using TankGame.Lifecycles;
using UnityEngine;

namespace TankGame.Game
{
	public class AbilitiesPanel : Lifecycle<AbilitiesPanelLifecycleHandler>
	{
		[SerializeField] private List<TankSection> tankSectionPanels;

		private Dictionary<TankPart, TankSection> tankSectionLookup = new Dictionary<TankPart, TankSection>();

		public void InitializeTankParts(GameState gameState) // List<TankPartState> tankPartStates)
		{

			foreach (var section in tankSectionPanels)
			{
				section.gameObject.SetActive(false);
			}

			for (int i = 0; i < gameState.tankState.tankPartStates.Count; i++)
			{

				var tankPartState = gameState.tankState.tankPartStates[i];

				var crewMember = gameState.crewMemberStates.FirstOrDefault(x => x.TankPart == tankPartState.tankPart);
				var sectionPanel = tankSectionPanels[i];
				sectionPanel.Initialize(tankPartState.tankPart, crewMember);

				tankSectionLookup.Add(tankPartState.tankPart, sectionPanel);
			}
		}

		public void SetupTankAbility(int slot, TankPart tankPart, TankAbility ability, Action<TankSectionAbility> onHoverAbility, Action<TankSectionAbility> onPressedAbility)
		{
			var tankSection = tankSectionLookup[tankPart];
			tankSection.RegisterAbility(slot, ability, onHoverAbility, onPressedAbility);
		}

		public void UpdateAbilities(List<CrewMemberState> crewMemberStates)
		{
			foreach (var crewMember in crewMemberStates)
			{
				var tankSection = tankSectionLookup[crewMember.TankPart];
				tankSection.UpdateCrewState(crewMember); 
			}
		}
	}
}