using System;
using System.Collections.Generic;

namespace TankGame.Game
{
	[Serializable]
	public class CrewDefinition
	{
		public string crewId;
		public List<string> memberIds;
	}
}