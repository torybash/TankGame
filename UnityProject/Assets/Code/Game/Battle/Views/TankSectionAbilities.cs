using System;
using System.Collections;
using System.Collections.Generic;
using TankGame.Views;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame.Game
{
	public class TankSectionAbilities : MonoBehaviour
	{
		[SerializeField] private Text sectionNameText;
		[SerializeField] private CrewMemberPanel crewMemberPanel;

		[SerializeField] private TemplatePool sectionAbilityPool;

		private List<TankSectionAbility> sectionAbilities = new List<TankSectionAbility>();

		public TankSection TankSection { get; private set; }

		public void Initialize(TankSection tankSection, CrewMemberState crewMember)
		{
			TankSection = tankSection;
			sectionNameText.text = tankSection.ToString();

			UpdateCrewState(crewMember);
		}

		public void RegisterAbility(int slot, TankAbility tankAbility, Action<TankSectionAbility> onHoverAbility, Action<TankSectionAbility> onPressedAbility)
		{
			var sectionAbility = sectionAbilityPool.GetInstance<TankSectionAbility>();
			sectionAbility.Initialize(tankAbility, onPressedAbility, onHoverAbility);

			sectionAbilities.Add(sectionAbility);
		}

		public void UpdateCrewState(CrewMemberState crewMember)
		{
			crewMemberPanel.SetCrewMember(crewMember, true);

		}
	}
}
