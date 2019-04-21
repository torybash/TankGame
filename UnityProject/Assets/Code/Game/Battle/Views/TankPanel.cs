using System;
using System.Collections.Generic;
using System.Linq;
using TankGame.Lifecycles;
using TankGame.Views;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame.Game
{
	public class TankPanel : Lifecycle<TankPanelLifecycleHandler>
	{
		[SerializeField] private TemplatePool sectionPanelPool;
		[SerializeField] private Text hullText;

		private readonly List<TankSectionPanel> sectionPanels = new List<TankSectionPanel>();

		public void Initialize(GameState gameState)
		{
			foreach (var sectionState in gameState.tankState.tankSectionState)
			{
				var tankSection = sectionPanelPool.GetInstance<TankSectionPanel>();
				tankSection.Initialize(sectionState.tankSection);
				sectionPanels.Add(tankSection);
			}
			UpdateState(gameState);
		}

		public void UpdateState(GameState gameState)
		{
			hullText.text = string.Format("{0}/{1}", gameState.tankState.hullHp.ToString(), gameState.tankState.maxHp.ToString());
			foreach (var sectionState in gameState.tankState.tankSectionState)
			{
				var crewMember = gameState.crewMemberStates.FirstOrDefault(x => x.TankPart == sectionState.tankSection);

				var tankSection = sectionPanels.FirstOrDefault(x => x.TankSection == sectionState.tankSection);
				tankSection.UpdateState(sectionState, crewMember);
				sectionPanels.Add(tankSection);
			}
		}
	}
}