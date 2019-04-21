using System;
using System.Collections;
using System.Collections.Generic;
using TankGame.Views;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame.Game
{
	public class TankSection : MonoBehaviour
	{
		[SerializeField] private Text sectionNameText;
		[SerializeField] private Text crewMemberFatigueText;
		[SerializeField] private Image crewProfileImage;
		[SerializeField] private Image crewHasActedImage;
		[SerializeField] private TankSectionAbility[] oldSectionAbilities;

		[SerializeField] private TemplatePool sectionAbilityPool;

		private List<TankSectionAbility> sectionAbilities = new List<TankSectionAbility>();

		public TankPart TankPart { get; private set; }

		public void Initialize(TankPart tankPart, CrewMemberState crewMember)
		{
			TankPart = tankPart;
			sectionNameText.text = tankPart.ToString();
			crewHasActedImage.gameObject.SetActive(false);
		}

		public void RegisterAbility(int slot, TankAbility tankAbility, Action<TankSectionAbility> onHoverAbility, Action<TankSectionAbility> onPressedAbility)
		{
			var sectionAbility = sectionAbilityPool.GetInstance<TankSectionAbility>();
			sectionAbility.Initialize(tankAbility, onPressedAbility, onHoverAbility);

			sectionAbilities.Add(sectionAbility);
		}

		public void UpdateCrewState(CrewMemberState crewMember)
		{
			crewMemberFatigueText.text = crewMember.fatigue + "/" + crewMember.maxFatigue;
			crewHasActedImage.gameObject.SetActive(crewMember.HasActed);
		}
	}
}
