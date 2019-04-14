using System;
using System.Collections;
using System.Collections.Generic;
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
		[SerializeField] private TankSectionAbility[] sectionAbilities;

		private Dictionary<TankAbility, TankSectionAbility> sectionAbilityLookup = new Dictionary<TankAbility, TankSectionAbility>();

		private void Start()
		{
			foreach (var sectionAbility in sectionAbilities)
			{
				if (!sectionAbilityLookup.ContainsValue(sectionAbility))
				{
					sectionAbility.gameObject.SetActive(false);
				}
			}
			crewHasActedImage.gameObject.SetActive(false);
		}

		public void Initialize(TankPart tankPart, CrewMemberState crewMember)
		{
			gameObject.SetActive(true);
			sectionNameText.text = tankPart.ToString();
		}

		public void RegisterAbility(int slot, TankAbility tankAbility, Action<TankSectionAbility> onHoverAbility, Action<TankSectionAbility> onPressedAbility)
		{
			var sectionAbility = sectionAbilities[slot];
			sectionAbility.gameObject.SetActive(true);
			sectionAbilityLookup.Add(tankAbility, sectionAbility);

			sectionAbility.TankAbility = tankAbility;
			sectionAbility.OnPressed += () => onPressedAbility(sectionAbility);
			sectionAbility.OnHover += () => onHoverAbility(sectionAbility);
		}

		public void UpdateCrewState(CrewMemberState crewMember)
		{
			crewMemberFatigueText.text = crewMember.fatigue + "/" + crewMember.maxFatigue;
			crewHasActedImage.gameObject.SetActive(crewMember.HasActed);
		}
	}
}
