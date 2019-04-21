using System;
using System.Collections.Generic;
using System.Linq;
using TankGame.Lifecycles;
using UnityEngine;

namespace TankGame.Game
{
	public class AbilitiesPanelLifecycleHandler : LifecycleHandler<AbilitiesPanel>
	{
		private readonly TankDatabase tankDatabase;

		private AbilitiesPanel abilitiesPanel; //TODO Should have controller for each instance 

		public event Action<TankSectionAbility> OnHoverAbility = delegate { };
		public event Action<TankSectionAbility> OnPressedAbility = delegate { };

		public AbilitiesPanelLifecycleHandler(TankDatabase tankDatabase)
		{
			this.tankDatabase = tankDatabase;
		}

		public override void InstanceAwake(AbilitiesPanel instance)
		{
			abilitiesPanel = instance;
		}

		public void Initialize(GameState gameState)
		{
			abilitiesPanel.InitializeTankParts(gameState);

			foreach (var sectionState in gameState.tankState.tankSectionState)
			{
				var abilityIds = sectionState.abilityIds;
				for (int i = 0; i < abilityIds.Count; i++)
				{
					string abilityId = abilityIds[i];
					var ability = tankDatabase.GetTankAbility(abilityId);
					ability.SetState(sectionState.tankSection, i);
					abilitiesPanel.SetupTankAbility(i, sectionState.tankSection, ability, OnHoverAbility, OnPressedAbility);
				}
			}

			UpdateAbilities(gameState.crewMemberStates);
		}

		public void UpdateAbilities(List<CrewMemberState> crewMemberStates)
		{
			abilitiesPanel.UpdateAbilities(crewMemberStates);

		}
	}
}