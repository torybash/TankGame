using UnityEngine;
using UnityEngine.UI;

namespace TankGame.Game
{
	public class CrewMemberPanel : MonoBehaviour
	{
		[SerializeField] private Image portraitImage;
		[SerializeField] private Text fatigueText;
		[SerializeField] private GameObject hasActedImage;

		public void SetCrewMember(CrewMemberState crewMember, bool showState)
		{
			fatigueText.text = showState ? (crewMember.fatigue + "/" + crewMember.maxFatigue) : "";
			hasActedImage.SetActive(crewMember.HasActed && showState);
			portraitImage.sprite = crewMember.portrait;
		}
	}
}
