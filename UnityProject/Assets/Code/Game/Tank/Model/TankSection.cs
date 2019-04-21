using System;

namespace TankGame.Game
{
	[Serializable]
	public enum TankSection
	{
		DRIVER_SEAT,
		MAIN_GUN,
		SECONDARY_GUN,
		RECON_STATION
	}

	static class Extensions
	{
		public static string GetPrettified(this TankSection tankSection) //TODO This should not be in code!
		{
			switch (tankSection)
			{
			case TankSection.DRIVER_SEAT:
				return "DRIVER SEAT";
			case TankSection.MAIN_GUN:
				return "MAIN GUN";
			case TankSection.SECONDARY_GUN:
				return "MACHINE GUN";
			case TankSection.RECON_STATION:
				return "RECON STATION";
			default:
				return "INVALID";
			}
		}
	}
}