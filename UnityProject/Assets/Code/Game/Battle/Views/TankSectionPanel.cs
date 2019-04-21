using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame.Game
{

	public class TankSectionPanel : MonoBehaviour
	{
		[SerializeField] private Text sectionTypeText;
		[SerializeField] private Text stateText;
		[SerializeField] private CrewMemberPanel crewMemberPanel;

		[SerializeField] private Color[] colors; //TODO Should probably be somewhere else
		[SerializeField] private string[] states;

		public TankSection TankSection { get; private set; }

		public void Initialize(TankSection tankSection)
		{
			TankSection = tankSection;
		}

		public void UpdateState(TankSectionState sectionState, CrewMemberState crewMember)
		{
			crewMemberPanel.SetCrewMember(crewMember, false);
			sectionTypeText.text = sectionState.tankSection.GetPrettified();

			float hpFraction = sectionState.health / (float)sectionState.maxHealth; //TODO Maybe just have make health = damageState?
			int damageState = -1;

			if (hpFraction >= 1f)
			{
				damageState = 0;
			} else if (hpFraction > 0.7f)
			{
				damageState = 1;
			} else if (hpFraction > 0.3f)
			{
				damageState = 2;
			} else
			{
				damageState = 3;
			}

			var state = states[damageState];
			var color = colors[damageState];

			stateText.text = state;
			stateText.color = color;
		}
	}

}