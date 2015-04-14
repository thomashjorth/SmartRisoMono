using System;
using DataAggregator.Models;
using System.Collections.Generic;
namespace DataAggregator.Utils
{
	public static class Configuration
	{
		public static List<DER> DerConfig(bool simulatedTest){
			List<DER> ders;
			if (simulatedTest) {
				DER der = new DER ("localhost", "8080");
				ders = new List<DER> { 
					der
				};
			} else {

				ders = new List<DER> {
					new DER ("syslab-01", "8080"),
					new DER ("syslab-02", "8080"),
					new DER ("syslab-03", "8080"),
					new DER ("syslab-05", "8080"),
					new DER ("syslab-07", "8080"),
					new DER ("syslab-08", "8080"),
					new DER ("syslab-10", "8080"),
					new DER ("syslab-11", "8080"),
					new DER ("syslab-12", "8080"),
					new DER ("syslab-16", "8080"),
					new DER ("syslab-18", "8080"),
					new DER ("syslab-21", "8080"),
					new DER ("syslab-22", "8080"),
					new DER ("syslab-23", "8080"),
					new DER ("syslab-24", "8080"),
					new DER ("syslab-26", "8080"),
					new DER ("syslab-27", "8080"),
					new DER ("syslab-c06", "8080")


				};
				
			}
			return ders;
		}
	}
}

/*
	Diesel("syslab-02", "genset1", SyslabTypes.DIESEL_TYPE, DEIFDieselGenset.class),
	Gaia("syslab-03", "gaia1", SyslabTypes.WTGS_TYPE, GaiaWindTurbine.class),
	Dumpload("syslab-05", "load1", SyslabTypes.LOAD_TYPE, GenericLoad.class),
	// Syslab-06 not used, empty config.
	PV117("syslab-07", "inv1", SyslabTypes.PV_TYPE, PVSystem.class),
	Flexhouse("syslab-08", "house1", SyslabTypes.FLEXHOUSE_TYPE, FlexHouse.class),
	Meteo("syslab-08", "meteo1", SyslabTypes.METEO_TYPE, MeteoStation.class),
	// TODO: Get it working!
	Price("syslab-08", "price1", SyslabTypes.PRICE_TYPE, GenericPriceServer.class),
	SwitchBoard715_2("syslab-09", "715-2", SyslabTypes.SUBSTATION_TYPE, StandardSubstation.class),
	PV715("syslab-10", "inv1", SyslabTypes.PV_TYPE, PVSystem.class),
	SwitchBoard117_2("syslab-11", "117-2", SyslabTypes.SUBSTATION_TYPE, StandardSubstation.class),
	SwitchBoard117_4("syslab-11", "117-4", SyslabTypes.SUBSTATION_TYPE, StandardSubstation.class),
	SwitchBoard117_5("syslab-11", "117-5", SyslabTypes.SUBSTATION_TYPE, StandardSubstation.class),
	VRBBattery("syslab-12", "batt1", SyslabTypes.BATTERY_TYPE, VRBBattery.class),
	// Syslab-13 not used, no config.
	// Syslab-14 not used, no config.
	// Syslab-15 not used, no config.
	Mobile16("syslab-16", "mobload1", SyslabTypes.LOAD_TYPE, GenericLoad.class),
	// Syslab-17 not used, no config.
	Mobile18("syslab-18", "mobload3", SyslabTypes.LOAD_TYPE, GenericLoad.class),
	// Syslab-19 not used, almost empty config.
	// Syslab-20 not used, empty config.
	EVBattery("syslab-21", "edison1", SyslabTypes.BATTERY_TYPE, LithiumBattery.class),
	LithiumBattery117_2("syslab-22", "edison1", SyslabTypes.BATTERY_TYPE, LithiumBattery.class),
	LithiumBattery117_3("syslab-23", "edison1", SyslabTypes.BATTERY_TYPE, LithiumBattery.class),
	PV319("syslab-24", "inv1", SyslabTypes.PV_TYPE, PVSystem.class),
	// Syslab-25 not used, empty config.
	SwitchBoard117_6("syslab-26", "117-6", SyslabTypes.SUBSTATION_TYPE, StandardSubstation.class),
	// TODO: Get it working!
	SmartBuilding("syslab-27", "house3", SyslabTypes.BUILDING_TYPE, SmartBuilding.class),
	SwitchBoard776_1("syslab-v06", "776-1", SyslabTypes.SUBSTATION_TYPE, StandardSubstation.class),
*/