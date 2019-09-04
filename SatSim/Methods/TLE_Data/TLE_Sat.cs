using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SatSim.Methods.TLE_Data
{
	public enum tle_sat_variables : uint
	{
		Num = 0,
		Name = 1,
		Id = 2,
		Classification = 3,
		Int_Designator = 4,
		Element_Set_Epoch = 5,
		First_Mean_Motion_Derivative = 6,
		Second_Mean_Motion_Derivative = 7,
		B_Drag = 8,
		Set_Type = 9,
		Set_Number = 10,
		Inclination = 11,
		Right_Ascension = 12,
		Eccentricity = 13,
		Argument_Perigee = 14,
		Mean_Anomaly = 15,
		Mean_Motion = 16,
		Revolution_Number = 17,
		Launch_Year = 18,
		Launch_Number = 19,
		Launch_Piece = 20,
		Epoch_Year = 21,
		Epoch_Day = 22,
		Epoch_DateTime = 23,
	}

	public enum tle_sat_serie_variables : uint
	{
		Inclination = 0,
		RAAN = 1,
		Radious = 2,
		Velocity = 3,
	}
	public class TLE_Sat
	{
		// Obtained items
		public string Sat_Name { get; set; }
		public uint Sat_Number { get; set; }
		public string Sat_Classification { get; set; }
		public string Sat_IntDesignator { get; set; }
		public string Sat_ElementSetEpoch { get; set; }
		public double Sat_FirstMeanMotionDer { get; set; }
		public double Sat_SecMeanMotionDer { get; set; }
		public double Sat_BSTARDrag { get; set; }
		public string Sat_SetType { get; set; }
		public string Sat_SetNumber { get; set; }
		public double Sat_Inclination { get; set; }
		public double Sat_RightAscension { get; set; }
		public double Sat_Eccentricity { get; set; }
		public double Sat_ArgumentPerigee { get; set; }
		public double Sat_MeanAnomaly { get; set; }
		public double Sat_MeanMotion { get; set; }
		public uint Sat_RevNumber { get; set; }

		// Processed items
		public uint Sat_LaunchYear { get; set; }
		public uint Sat_LaunchNumber { get; set; }
		public string Sat_LaunchPiece { get; set; }
		public uint Sat_EpochYear { get; set; }
		public double Sat_EpochDay { get; set; }
		public DateTime Sat_EpochDateTime { get; set; }

		// Post-processed items
		public double Sat_SemiAxis { get; set; }

		// Time-series
		public List<PointF> Sat_Inclination_series { get; set; }
		public List<PointF> Sat_RAAN_series { get; set; }
		public List<PointF> Sat_Radious_series { get; set; }
		public List<PointF> Sat_Velocity_series { get; set; }
	}

	public class Sat_Constants
    {
        public static double VISUALIZATION3D_SCALE = 0.1;

        public static double G_constant = 6.67259E-11;
		public static double EARTH_MASS_constant = 5.972E24;
		public static double EARTH_RADIOUS_constant = 6371;
	}
}
