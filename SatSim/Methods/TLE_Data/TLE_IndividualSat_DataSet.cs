﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SatSim.Methods.TLE_Data
{
	class TLE_IndividualSat_DataSet
	{
		/// <summary>
		/// This method is used to divide a bulk raw data array into a list of different TLEs.
		/// 
		/// Each entry of the return list is a complete TLE
		/// </summary>
		/// <param name="data_raw">Array of bytes with all information obtained from scraping</param>
		/// <returns>List of array of bytes containing raw TLE data</returns>
		public static List<byte[]> TLE_Lines_Divider(byte[] data_raw)
		{
			List<byte[]> result_list = new List<byte[]>();
			byte[] byte_arr = new byte[141];

			int counter = 0;
			int _EOL_counter = 0;
			int _byte_arr_counter = 0;
			foreach(byte item in data_raw)
			{
				if (item == 10 || counter == (data_raw.Length - 1)) _EOL_counter++;
				if (_EOL_counter < 2)
				{
					byte_arr[_byte_arr_counter] = item;
					_byte_arr_counter++;
				}
				else
				{
					result_list.Add(byte_arr);
					_EOL_counter = 0;
					_byte_arr_counter = 0;
					byte_arr = new byte[141];
				}
				counter++;
			}

			//foreach(var item in result_list)
			//{
			//	Debug.WriteLine(Encoding.Default.GetString(item));
			//}

			return result_list;
		}

		/// <summary>
		/// This method is used to store in a list all TLE parameters for each epoch time
		/// 
		/// Each entry of the return list is a TLE_Sat variable with its parameters defined except for the name (which is not necessary as every entry is related to the same satellite)
		/// </summary>
		/// <param name="data_raw">List of array of bytes with every TLE (its two lines) for each entry</param>
		/// <returns>List of TLE_Sat containing processed data</returns>
		public static List<TLE_Sat> TLE_Lines_DataExtractor(List<byte[]> data_raw)
		{
			try
			{
				List<TLE_Sat> TLE_individualSat_List = new List<TLE_Sat>();

				foreach(byte[] byte_arr in data_raw)
				{
					TLE_Sat _TLE_sat = new TLE_Sat();

					// First row
					_TLE_sat.Sat_Number = Convert.ToUInt32((System.Text.Encoding.Default.GetString(byte_arr)).Substring(2, 5));
					_TLE_sat.Sat_Classification = (System.Text.Encoding.Default.GetString(byte_arr)).Substring(7, 1);
					_TLE_sat.Sat_IntDesignator = (System.Text.Encoding.Default.GetString(byte_arr)).Substring(9, 8);
					_TLE_sat.Sat_ElementSetEpoch = (System.Text.Encoding.Default.GetString(byte_arr)).Substring(18, 14);
					_TLE_sat.Sat_FirstMeanMotionDer = Convert.ToDouble((System.Text.Encoding.Default.GetString(byte_arr)).Substring(33, 10), System.Globalization.CultureInfo.InvariantCulture);
					_TLE_sat.Sat_SecMeanMotionDer = TLE_Data_AuxMethods.GetDoubleFromScientificNotation((System.Text.Encoding.Default.GetString(byte_arr)).Substring(44, 8));
					_TLE_sat.Sat_BSTARDrag = TLE_Data_AuxMethods.GetDoubleFromScientificNotation((System.Text.Encoding.Default.GetString(byte_arr)).Substring(53, 8));
					_TLE_sat.Sat_SetType = (System.Text.Encoding.Default.GetString(byte_arr)).Substring(62, 1);
					_TLE_sat.Sat_SetNumber = (System.Text.Encoding.Default.GetString(byte_arr)).Substring(64, 4);

					_TLE_sat.Sat_LaunchYear = TLE_Data_AuxMethods.GetLaunchYearFromDesignator(_TLE_sat.Sat_IntDesignator);
					_TLE_sat.Sat_LaunchNumber = TLE_Data_AuxMethods.GetLaunchNumberFromDesignator(_TLE_sat.Sat_IntDesignator);
					_TLE_sat.Sat_LaunchPiece = TLE_Data_AuxMethods.GetLaunchPieceFromDesignator(_TLE_sat.Sat_IntDesignator);
					_TLE_sat.Sat_EpochYear = TLE_Data_AuxMethods.GetYearFromEpoch(_TLE_sat.Sat_ElementSetEpoch);
					_TLE_sat.Sat_EpochDay = TLE_Data_AuxMethods.GetDayFromEpoch(_TLE_sat.Sat_ElementSetEpoch);

					// Second row
					_TLE_sat.Sat_Inclination = Convert.ToDouble((System.Text.Encoding.Default.GetString(byte_arr)).Substring(79, 8), System.Globalization.CultureInfo.InvariantCulture);
					_TLE_sat.Sat_RightAscension = Convert.ToDouble((System.Text.Encoding.Default.GetString(byte_arr)).Substring(88, 8), System.Globalization.CultureInfo.InvariantCulture);
					_TLE_sat.Sat_Eccentricity = Convert.ToDouble(("0." + (System.Text.Encoding.Default.GetString(byte_arr)).Substring(97, 7)), System.Globalization.CultureInfo.InvariantCulture);
					_TLE_sat.Sat_ArgumentPerigee = Convert.ToDouble((System.Text.Encoding.Default.GetString(byte_arr)).Substring(105, 8), System.Globalization.CultureInfo.InvariantCulture);
					_TLE_sat.Sat_MeanAnomaly = Convert.ToDouble((System.Text.Encoding.Default.GetString(byte_arr)).Substring(114, 8), System.Globalization.CultureInfo.InvariantCulture);
					_TLE_sat.Sat_MeanMotion = Convert.ToDouble((System.Text.Encoding.Default.GetString(byte_arr)).Substring(123, 11), System.Globalization.CultureInfo.InvariantCulture);
					_TLE_sat.Sat_RevNumber = Convert.ToUInt32((System.Text.Encoding.Default.GetString(byte_arr)).Substring(134, 5));

					TLE_individualSat_List.Add(_TLE_sat);
				}

				return TLE_individualSat_List;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return null;
			}
		}
	}
}
