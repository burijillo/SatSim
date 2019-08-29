using System;
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
					DateTime epoch = new DateTime();

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
					if (TLE_Data_AuxMethods.GetDateTimeFromEpoch(_TLE_sat.Sat_ElementSetEpoch, out epoch)) _TLE_sat.Sat_EpochDateTime = epoch;

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

		/// <summary>
		/// This method is used to store in a list all processed TLE parameters for each epoch time
		/// 
		/// This output list is different from the one obtained from TLE_Lines_DataExtractor in the processing. In this list, each data which is obtained from the same day is promediated, so there are no repeated parameters for the same day. This is made to make clearer all plots and get better referencing.
		/// Each entry of the return list is a TLE_Sat variable with its parameters defined except for the name (which is not necessary as every entry is related to the same satellite)
		/// </summary>
		/// <param name="data_raw">List of array of bytes with every TLE (its two lines) for each entry</param>
        /// 
        /// 
        /// <returns>List of TLE_Sat containing processed data</returns>
        public static List<TLE_Sat> TLE_HistoricData_Processed(List<TLE_Sat> TLE_List_raw)
		{
			try
			{
				List<TLE_Sat> TLE_individualSat_List_processed = new List<TLE_Sat>();
				List<TLE_Sat> TLE_buffer_list = new List<TLE_Sat>();
				// This variable is used to compare in case the next epoch_day is repeated
				int _epoch_day_ref = 0;
				int counter = 0;

				// Check for repeated epoch days
				for (int i = 0; i < TLE_List_raw.Count; i++)
				{
					// Epoch day of the current TLE_Sat in the iteration
					int _epoch_day_current = Convert.ToInt32(Math.Floor(TLE_List_raw[i].Sat_EpochDay));
					// Reference epoch day is the next one in the iteration
					if (i < (TLE_List_raw.Count - 1)) _epoch_day_ref = Convert.ToInt32(Math.Floor(TLE_List_raw[i + 1].Sat_EpochDay));
					if (_epoch_day_current == _epoch_day_ref && i != (TLE_List_raw.Count - 1))
					{
						// In this case as the current epoch day is the same as the ref one, we need to store data to promediate after that
						TLE_buffer_list.Add(TLE_List_raw[i]);

						counter++;
						Debug.WriteLine("Repeticion numero: " + counter + "; con " + _epoch_day_current);
					}
					else
					{
						// In this case as the current epoch day is different from the ref one, this TLE_Sat data will be stored in the processed list
						TLE_buffer_list.Add(TLE_List_raw[i]);
						// Promediate data
						TLE_Sat _TLE_processed = TLE_PromediateList_Processed(TLE_buffer_list);

						// Add final TLE data to final list
						TLE_individualSat_List_processed.Add(_TLE_processed);

						// Reset buffer list
						TLE_buffer_list.Clear();
					}
				}

				Debug.WriteLine("Lista anterior: " + TLE_List_raw.Count + "; Lista ultima: " + TLE_individualSat_List_processed.Count);

				return TLE_individualSat_List_processed;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return null;
			}
		}

		/// <summary>
		/// This method is needed to promediate raw data
		/// 
		/// In case there is only one entry in the input list, it will not change data except datetime epoch
		/// </summary>
		/// <param name="TLE_buffer_list">Raw TLE list data input</param>
		/// <returns>TLE instance with promediated data</returns>
		private static TLE_Sat TLE_PromediateList_Processed(List<TLE_Sat> TLE_buffer_list)
		{
			try
			{
				TLE_Sat result = new TLE_Sat();

				// Get datetime epoch. In this case hours, minutes and seconds will be 0. As it doesnt matter which entry is used, we get the first one
				int day;
				int month = TLE_Data_AuxMethods.GetMonthAndDayFromEpoch(TLE_buffer_list[0].Sat_ElementSetEpoch, out day);
				DateTime _TLE_epochDatetime;
				if (TLE_buffer_list.Count != 1)
					_TLE_epochDatetime = new DateTime((int)TLE_buffer_list[0].Sat_EpochYear, month, day, 0, 0, 0);
				else
					_TLE_epochDatetime = TLE_buffer_list[0].Sat_EpochDateTime;

				// Promediate certain parameters
				double _Sat_ElementSetEpoch_double = 0;
				double _Sat_FirstMeanMotionDer = 0;
				double _Sat_SecMeanMotionDer = 0;
				double _Sat_BSTARDrag = 0;
				double _Sat_Inclination = 0;
				double _Sat_RightAscension = 0;
				double _Sat_Eccentricity = 0;
				double _Sat_ArgumentPerigee = 0;
				double _Sat_MeanAnomaly = 0;
				double _Sat_MeanMotion = 0;
				uint _Sat_RevNumber = 0;

				for (int i = 0; i < TLE_buffer_list.Count; i++)
				{
					_Sat_ElementSetEpoch_double += Convert.ToDouble(TLE_buffer_list[i].Sat_ElementSetEpoch, System.Globalization.CultureInfo.InvariantCulture);
					_Sat_FirstMeanMotionDer += TLE_buffer_list[i].Sat_FirstMeanMotionDer;
					_Sat_SecMeanMotionDer += TLE_buffer_list[i].Sat_SecMeanMotionDer;
					_Sat_BSTARDrag += TLE_buffer_list[i].Sat_BSTARDrag;
					_Sat_Inclination += TLE_buffer_list[i].Sat_Inclination;
					_Sat_RightAscension += TLE_buffer_list[i].Sat_RightAscension;
					_Sat_Eccentricity += TLE_buffer_list[i].Sat_Eccentricity;
					_Sat_ArgumentPerigee += TLE_buffer_list[i].Sat_ArgumentPerigee;
					_Sat_MeanAnomaly += TLE_buffer_list[i].Sat_MeanAnomaly;
					_Sat_MeanMotion += TLE_buffer_list[i].Sat_MeanMotion;
					_Sat_RevNumber += TLE_buffer_list[i].Sat_RevNumber;
				}

				_Sat_ElementSetEpoch_double = _Sat_ElementSetEpoch_double / TLE_buffer_list.Count;
				_Sat_FirstMeanMotionDer = _Sat_FirstMeanMotionDer / TLE_buffer_list.Count;
				_Sat_SecMeanMotionDer = _Sat_SecMeanMotionDer / TLE_buffer_list.Count;
				_Sat_BSTARDrag = _Sat_BSTARDrag / TLE_buffer_list.Count;
				_Sat_Inclination = _Sat_Inclination / TLE_buffer_list.Count;
				_Sat_RightAscension = _Sat_RightAscension / TLE_buffer_list.Count;
				_Sat_Eccentricity = _Sat_Eccentricity / TLE_buffer_list.Count;
				_Sat_ArgumentPerigee = _Sat_ArgumentPerigee / TLE_buffer_list.Count;
				_Sat_MeanAnomaly = _Sat_MeanAnomaly / TLE_buffer_list.Count;
				_Sat_MeanMotion = _Sat_MeanMotion / TLE_buffer_list.Count;
				_Sat_RevNumber = _Sat_RevNumber / (uint)TLE_buffer_list.Count;

				// Fill all TLE_Sat output. All data not promediated will be the first entry
				result.Sat_Number = TLE_buffer_list[0].Sat_Number;
				result.Sat_Classification = TLE_buffer_list[0].Sat_Classification;
				result.Sat_IntDesignator = TLE_buffer_list[0].Sat_IntDesignator;
				result.Sat_ElementSetEpoch = string.Format("{0:0.00000000}",_Sat_ElementSetEpoch_double);
				result.Sat_FirstMeanMotionDer = _Sat_FirstMeanMotionDer;
				result.Sat_SecMeanMotionDer = _Sat_SecMeanMotionDer;
				result.Sat_BSTARDrag = _Sat_BSTARDrag;
				result.Sat_SetType = TLE_buffer_list[0].Sat_SetType;
				result.Sat_SetNumber = TLE_buffer_list[0].Sat_SetNumber;
				result.Sat_Inclination = _Sat_Inclination;
				result.Sat_RightAscension = _Sat_RightAscension;
				result.Sat_Eccentricity = _Sat_Eccentricity;
				result.Sat_ArgumentPerigee = _Sat_ArgumentPerigee;
				result.Sat_MeanAnomaly = _Sat_MeanAnomaly;
				result.Sat_MeanMotion = _Sat_MeanMotion;
				result.Sat_RevNumber = _Sat_RevNumber;
				result.Sat_LaunchYear = TLE_Data_AuxMethods.GetLaunchYearFromDesignator(result.Sat_IntDesignator);
				result.Sat_LaunchNumber = TLE_Data_AuxMethods.GetLaunchNumberFromDesignator(result.Sat_IntDesignator);
				result.Sat_LaunchPiece = TLE_Data_AuxMethods.GetLaunchPieceFromDesignator(result.Sat_IntDesignator);
				result.Sat_EpochYear = TLE_Data_AuxMethods.GetYearFromEpoch(result.Sat_ElementSetEpoch);
				result.Sat_EpochDay = TLE_Data_AuxMethods.GetDayFromEpoch(result.Sat_ElementSetEpoch);
				result.Sat_EpochDateTime = _TLE_epochDatetime;

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return null;
			}
		}
	}
}
