using System;

namespace TankGame.Game
{
	[Serializable]
	public enum CardEffectTarget
	{
		PLAYER_TANK,
		TANK_PART,
		RANDOM_TANK_PART,
		NONE,
	}
}