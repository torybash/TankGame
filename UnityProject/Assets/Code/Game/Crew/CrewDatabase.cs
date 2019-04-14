using System.Collections.Generic;
using System.Linq;
using TankGame.Databases;
using UnityEngine;

namespace TankGame.Game
{
	[CreateAssetMenu]
	public class CrewDatabase : Database
	{
		[SerializeField] private List<CrewMemberState> crewMemberStates;
		[SerializeField] private List<CrewDefinition> crewDefinitions;


		public CrewMemberState GetCrewMemberState(string memberId)
		{
			var state = crewMemberStates.FirstOrDefault(x => x.memberId == memberId);
			return (CrewMemberState)state.Clone();
		}

		public List<CrewMemberState> GetCrew(string crewId)
		{
			var crewDefinition = crewDefinitions.FirstOrDefault(x => x.crewId == crewId);
			var crewMemberState = new List<CrewMemberState>();
			foreach (var memberId in crewDefinition.memberIds)
			{
				var crewMember = GetCrewMemberState(memberId);
				crewMemberStates.Add(crewMember);
			}
			return crewMemberStates;
		}
	}
}