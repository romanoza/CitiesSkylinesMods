﻿using System.IO;
using System.Xml.Serialization;

namespace ApocDev.CitySkylines.Mod
{
	public class ModSettings
	{
		public const string SettingsPath = "ApocDevModSettings.xml";


		[XmlIgnore]
		private static ModSettings _instance;


#if !MOD_FIRESPREAD
		public bool ModifyTransportCapacities { get; set; }
		public int PassengerTrainCapacity { get; set; }
		public int MetroCapacity { get; set; }
		public int BusCapacity { get; set; }
		public int PassengerPlaneCapacity { get; set; }
		public int PassengerShipCapacity { get; set; }
#endif
		public bool EnableFireSpread { get; set; }
		public float FireSpreadModifier { get; set; }
		public float BaseFireSpreadChance { get; set; }
		public float NoWaterFireSpreadAdditional { get; set; }
		public float UneducatedFireSpreadAdditional { get; set; }

		public void Save()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(ModSettings));
		}

		public static void Load()
		{
			if (_instance != null)
				return;

			XmlSerializer serializer = new XmlSerializer(typeof(ModSettings));

			// If a settings file doesn't exist, have the serializer generate us one.
			if (!File.Exists(SettingsPath))
			{
				_instance = new ModSettings();
				using (var fs = File.Create(SettingsPath))
					serializer.Serialize(fs, _instance);
			}
			else
			{
				using (var fs = File.OpenRead(SettingsPath))
					_instance = serializer.Deserialize(fs) as ModSettings;
			}
		}

		[XmlIgnore]
		public static ModSettings Instance
		{
			get
			{
				if (_instance == null)
					Load();
				return _instance;
			}
		}

		public ModSettings()
		{
#if !MOD_FIRESPREAD
			// Default: 30
			BusCapacity = 90;
			// Default: 30
			PassengerTrainCapacity = 480;
			// Default: 100
			PassengerShipCapacity = 100;
			// Default: 30
			PassengerPlaneCapacity = 350;
			// Default: 30
			MetroCapacity = 360;
#endif


			FireSpreadModifier = 0.00725f;
			BaseFireSpreadChance = 1.5f;
			NoWaterFireSpreadAdditional = 7f;
			UneducatedFireSpreadAdditional = 1f;
		}
	}
}
