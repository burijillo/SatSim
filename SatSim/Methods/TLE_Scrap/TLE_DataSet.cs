using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace SatSim.Methods.TLE_Scrap
{
	public class TLE_DataSet
	{
		public List<TLE_Sat> _TLE_Sat_List = new List<TLE_Sat>();
		public DataSet _TLE_Sat_DataSet = new DataSet();
		public TLE_Sat _TLE_Sat_Selected = new TLE_Sat();
		public Sat_Constants _sat_constants = new Sat_Constants();

		public bool _TLE_LOADED = false;
		public bool _SAT_SELECTED = false;

		#region Singleton

		private static TLE_DataSet _instance;
		public static TLE_DataSet GetInstance()
		{
			if (_instance == null) _instance = new TLE_DataSet();
			return _instance;
		}

		#endregion

		public List<TLE_Sat> ParseTLEString(byte[] TLE_data)
		{
			try
			{
				// POR AHORA SE PIERDE EL ULTIMO SATELITE, POR COMO ESTA PENSADA LA RECUPERACION
				int counter = 0;
				int _internal_counter = 0;
				TLE_Sat tle_sat = new TLE_Sat();
				byte[] buffer = new byte[250];
				//List<TLE_Sat> final_list = new List<TLE_Sat>();
				while(counter < (TLE_data.Length - 1))
				{
					if (TLE_data[counter] == 10 && TLE_data[counter + 1] == 49)
					{
						tle_sat.Sat_Name = System.Text.Encoding.Default.GetString(buffer).Substring(2, 24).Replace("\0",String.Empty).Replace("\r",String.Empty);

						Array.Clear(buffer, 0, buffer.Length);
						_internal_counter = 0;
					}
					else if (TLE_data[counter] == 10 && TLE_data[counter + 1] == 50)
					{
						tle_sat.Sat_Number = Convert.ToUInt32((System.Text.Encoding.Default.GetString(buffer)).Substring(2, 5));
						tle_sat.Sat_Classification = (System.Text.Encoding.Default.GetString(buffer)).Substring(7, 1);
						tle_sat.Sat_IntDesignator = (System.Text.Encoding.Default.GetString(buffer)).Substring(9, 8);
						tle_sat.Sat_ElementSetEpoch = (System.Text.Encoding.Default.GetString(buffer)).Substring(18, 14);
						tle_sat.Sat_FirstMeanMotionDer = Convert.ToDouble((System.Text.Encoding.Default.GetString(buffer)).Substring(33, 10), System.Globalization.CultureInfo.InvariantCulture);
						tle_sat.Sat_SecMeanMotionDer = GetDoubleFromScientificNotation((System.Text.Encoding.Default.GetString(buffer)).Substring(44, 8));
						tle_sat.Sat_BSTARDrag = GetDoubleFromScientificNotation((System.Text.Encoding.Default.GetString(buffer)).Substring(53, 8));
						tle_sat.Sat_SetType = (System.Text.Encoding.Default.GetString(buffer)).Substring(62, 1);
						tle_sat.Sat_SetNumber = (System.Text.Encoding.Default.GetString(buffer)).Substring(64, 4);

						// Processed items
						tle_sat.Sat_LaunchYear = GetLaunchYearFromDesignator(tle_sat.Sat_IntDesignator);
						tle_sat.Sat_LaunchNumber = GetLaunchNumberFromDesignator(tle_sat.Sat_IntDesignator);
						tle_sat.Sat_LaunchPiece = GetLaunchPieceFromDesignator(tle_sat.Sat_IntDesignator);
						tle_sat.Sat_EpochYear = GetYearFromEpoch(tle_sat.Sat_ElementSetEpoch);
						tle_sat.Sat_EpochDay = GetDayFromEpoch(tle_sat.Sat_ElementSetEpoch);

						Array.Clear(buffer, 0, buffer.Length);
						_internal_counter = 0;
					}
					else if (TLE_data[counter] == 10 && TLE_data[counter + 1] == 48)
					{
						tle_sat.Sat_Inclination = Convert.ToDouble((System.Text.Encoding.Default.GetString(buffer)).Substring(8, 8), System.Globalization.CultureInfo.InvariantCulture);
						tle_sat.Sat_RightAscension = Convert.ToDouble((System.Text.Encoding.Default.GetString(buffer)).Substring(17, 8), System.Globalization.CultureInfo.InvariantCulture);
						tle_sat.Sat_Eccentricity = Convert.ToDouble(("0." + (System.Text.Encoding.Default.GetString(buffer)).Substring(26, 7)), System.Globalization.CultureInfo.InvariantCulture);
						tle_sat.Sat_ArgumentPerigee = Convert.ToDouble((System.Text.Encoding.Default.GetString(buffer)).Substring(34, 8), System.Globalization.CultureInfo.InvariantCulture);
						tle_sat.Sat_MeanAnomaly = Convert.ToDouble((System.Text.Encoding.Default.GetString(buffer)).Substring(43, 8), System.Globalization.CultureInfo.InvariantCulture);
						tle_sat.Sat_MeanMotion = Convert.ToDouble((System.Text.Encoding.Default.GetString(buffer)).Substring(52, 11), System.Globalization.CultureInfo.InvariantCulture);
						tle_sat.Sat_RevNumber = Convert.ToUInt32((System.Text.Encoding.Default.GetString(buffer)).Substring(63, 5));

						Array.Clear(buffer, 0, buffer.Length);
						_internal_counter = 0;
						_TLE_Sat_List.Add(tle_sat);
						tle_sat = new TLE_Sat();
					}
					else
					{
						buffer[_internal_counter] = TLE_data[counter];
						_internal_counter++;
					}
					counter++;
				}
				// Parse ROWS
				/*string[] raw_rows = TLE_data.Split('\n');
				List<string> final_rows = new List<string>();
				string _row = "";

				for (int i = 0; i < raw_rows.Count(); i++)
				{
					_row += " " + raw_rows[i];
					if ((i + 1) % 3 == 0)
					{
						final_rows.Add(_row);
						_row = "";
					}
				}

				// Parse COLUMNS
				List<string[]> final_list = new List<string[]>();
				char[] delimiters = new char[] { ' ', '\r' };

				foreach(var item in final_rows)
				{
					string[] raw_final_row = item.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
					final_list.Add(raw_final_row);
					//Debug.WriteLine("Count: " + final_list.Count + "; Elements: " + raw_final_row.Length);
				}*/

				return _TLE_Sat_List;
				//return null;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return null;
			}
		}

		public DataSet InitializeTLEDataSet()
		{
			try
			{
				//DataSet result = new DataSet();
				DataTable tabla = new DataTable("Sat_table");
				tabla.Columns.Add(new DataColumn("Num", Type.GetType("System.Int32")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Name.ToString(), Type.GetType("System.String")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Id.ToString(), Type.GetType("System.UInt32")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Classification.ToString(), Type.GetType("System.String")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Int_Designator.ToString(), Type.GetType("System.String")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Element_Set_Epoch.ToString(), Type.GetType("System.String")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.First_Mean_Motion_Derivative.ToString(), Type.GetType("System.Double")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Second_Mean_Motion_Derivative.ToString(), Type.GetType("System.Double")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.B_Drag.ToString(), Type.GetType("System.Double")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Set_Type.ToString(), Type.GetType("System.String")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Set_Number.ToString(), Type.GetType("System.String")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Inclination.ToString(), Type.GetType("System.Double")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Right_Ascension.ToString(), Type.GetType("System.Double")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Eccentricity.ToString(), Type.GetType("System.Double")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Argument_Perigee.ToString(), Type.GetType("System.Double")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Mean_Anomaly.ToString(), Type.GetType("System.Double")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Mean_Motion.ToString(), Type.GetType("System.Double")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Revolution_Number.ToString(), Type.GetType("System.UInt32")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Launch_Year.ToString(), Type.GetType("System.UInt32")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Launch_Number.ToString(), Type.GetType("System.UInt32")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Launch_Piece.ToString(), Type.GetType("System.String")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Epoch_Year.ToString(), Type.GetType("System.UInt32")));
				tabla.Columns.Add(new DataColumn(tle_sat_variables.Epoch_Day.ToString(), Type.GetType("System.Double")));

				_TLE_Sat_DataSet.Tables.Add(tabla);

				return _TLE_Sat_DataSet;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return null;
			}
		}

		public List<TLE_Sat> GetTLEDataSet(byte[] TLE_data, out DataSet _dataSet)
		{
			try
			{
				List<TLE_Sat> dataSet = ParseTLEString(TLE_data);
				_dataSet = InitializeTLEDataSet();

				for (int i = 0; i < dataSet.Count; i++)
				{
					DataRow fila = _dataSet.Tables[0].NewRow();

					fila[0] = i;
					fila[1] = dataSet[i].Sat_Name;
					fila[2] = dataSet[i].Sat_Number;
					fila[3] = dataSet[i].Sat_Classification;
					fila[4] = dataSet[i].Sat_IntDesignator;
					fila[5] = dataSet[i].Sat_ElementSetEpoch;
					fila[6] = dataSet[i].Sat_FirstMeanMotionDer;
					fila[7] = dataSet[i].Sat_SecMeanMotionDer;
					fila[8] = dataSet[i].Sat_BSTARDrag;
					fila[9] = dataSet[i].Sat_SetType;
					fila[10] = dataSet[i].Sat_SetNumber;
					fila[11] = dataSet[i].Sat_Inclination;
					fila[12] = dataSet[i].Sat_RightAscension;
					fila[13] = dataSet[i].Sat_Eccentricity;
					fila[14] = dataSet[i].Sat_ArgumentPerigee;
					fila[15] = dataSet[i].Sat_MeanAnomaly;
					fila[16] = dataSet[i].Sat_MeanMotion;
					fila[17] = dataSet[i].Sat_RevNumber;
					fila[18] = dataSet[i].Sat_LaunchYear;
					fila[19] = dataSet[i].Sat_LaunchNumber;
					fila[20] = dataSet[i].Sat_LaunchPiece;
					fila[21] = dataSet[i].Sat_EpochYear;
					fila[22] = dataSet[i].Sat_EpochDay;

					_dataSet.Tables[0].Rows.Add(fila);
				}

				_TLE_Sat_DataSet = _dataSet;

				return dataSet;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				_dataSet = null;
				return null;
			}
		}

		public void SaveTLEDataSet_xml(string filePath, DataSet dataSet)
		{
			try
			{
				if (_TLE_LOADED)
				{
					dataSet.WriteXml(filePath, XmlWriteMode.WriteSchema);
				}
				else
				{
					MessageBox.Show("No TLE database loaded yet", "Error saving xml", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		public void LoadTLEDataSet_xml(string filePath)
		{
			try
			{
				if (_TLE_LOADED)
				{
					if (MessageBox.Show("TLE database is already loaded.\nDo you want to reload it?", "TLE databse loaded", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
					{
						_TLE_Sat_DataSet.Reset();
						_TLE_Sat_List.Clear();
						_TLE_Sat_DataSet.ReadXml(filePath, XmlReadMode.Auto);
					}
				}
				else
				{
					_TLE_Sat_DataSet.ReadXml(filePath, XmlReadMode.Auto);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		public bool GetSelectedSat_fromDataRow(DataRow input_dataRow)
		{
			try
			{
				TLE_Sat result = new TLE_Sat();

				result.Sat_Name = input_dataRow.ItemArray[1].ToString();
				result.Sat_Number = Convert.ToUInt32(input_dataRow.ItemArray[2]);
				result.Sat_Classification = input_dataRow.ItemArray[3].ToString();
				result.Sat_IntDesignator = input_dataRow.ItemArray[4].ToString();
				result.Sat_ElementSetEpoch = input_dataRow.ItemArray[5].ToString();
				result.Sat_FirstMeanMotionDer = Convert.ToDouble(input_dataRow.ItemArray[6]);
				result.Sat_SecMeanMotionDer = Convert.ToDouble(input_dataRow.ItemArray[7]);
				result.Sat_BSTARDrag = Convert.ToDouble(input_dataRow.ItemArray[8]);
				result.Sat_SetType = input_dataRow.ItemArray[9].ToString();
				result.Sat_SetNumber = input_dataRow.ItemArray[10].ToString();
				result.Sat_Inclination = Convert.ToDouble(input_dataRow.ItemArray[11]);
				result.Sat_RightAscension = Convert.ToDouble(input_dataRow.ItemArray[12]);
				result.Sat_Eccentricity = Convert.ToDouble(input_dataRow.ItemArray[13]);
				result.Sat_ArgumentPerigee = Convert.ToDouble(input_dataRow.ItemArray[14]);
				result.Sat_MeanAnomaly = Convert.ToDouble(input_dataRow.ItemArray[15]);
				result.Sat_MeanMotion = Convert.ToDouble(input_dataRow.ItemArray[16]);
				result.Sat_RevNumber = Convert.ToUInt32(input_dataRow.ItemArray[17]);
				result.Sat_LaunchYear = Convert.ToUInt32(input_dataRow.ItemArray[18]);
				result.Sat_LaunchNumber = Convert.ToUInt32(input_dataRow.ItemArray[19]);
				result.Sat_LaunchPiece = input_dataRow.ItemArray[20].ToString();
				result.Sat_EpochYear = Convert.ToUInt32(input_dataRow.ItemArray[21]);
				result.Sat_EpochDay = Convert.ToDouble(input_dataRow.ItemArray[22]);

				result.Sat_SemiAxis = GetSemiAxisFromPeriod(result.Sat_MeanMotion);

				_TLE_Sat_Selected = result;

				_SAT_SELECTED = true;
				return true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return false;
			}
		}

		#region Aux methods

		public uint GetLaunchYearFromDesignator(string designator)
		{
			try
			{
				uint result = 0;

				if (!string.IsNullOrWhiteSpace(designator))
				{
					uint _sub_designator = Convert.ToUInt32(designator.Substring(0, 2));

					if (_sub_designator >= 57) result = 1900 + _sub_designator;
					else result = 2000 + _sub_designator; 
				}

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return 0;
			}
		}

		public uint GetLaunchNumberFromDesignator(string designator)
		{
			try
			{
				uint result = 0;

				if (!string.IsNullOrWhiteSpace(designator))
				{
					result = Convert.ToUInt32(designator.Substring(2, 3));
				}

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return 0;
			}
		}
		public string GetLaunchPieceFromDesignator(string designator)
		{
			try
			{
				string result = "";

				if (!string.IsNullOrWhiteSpace(designator))
				{
					result = designator.Substring(5, 3);
				}

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return null;
			}
		}

		public double GetDoubleFromScientificNotation(string input_data)
		{
			try
			{
				double result = 0.0;

				if (!string.IsNullOrWhiteSpace(input_data))
				{
					input_data = input_data.Insert(input_data.Length - 2, "E");
					result = Convert.ToDouble(input_data, System.Globalization.CultureInfo.InvariantCulture);
				}

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return 0.0;
			}
		}

		public uint GetYearFromEpoch(string epoch)
		{
			try
			{
				uint result = 0;


				if (!string.IsNullOrWhiteSpace(epoch))
				{
					uint _sub_designator = Convert.ToUInt32(epoch.Substring(0, 2));

					if (_sub_designator >= 57) result = 1900 + _sub_designator;
					else result = 2000 + _sub_designator;
				}

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return 0;
			}
		}

		public double GetDayFromEpoch(string epoch)
		{
			try
			{
				double result = 0.0;

				if (!string.IsNullOrWhiteSpace(epoch))
				{
					result = Convert.ToDouble(epoch.Substring(2, 12), System.Globalization.CultureInfo.InvariantCulture);
				}

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return 0.0;
			}
		}

		public double GetSemiAxisFromPeriod(double mean_motion)
		{
			try
			{
				double result = 0.0;

				// (Seconds in a day) / (mean_motion)
				double period = (double)(60 * 60 * 24) / mean_motion;

				double prev_op = (_sat_constants.EARTH_MASS_constant * _sat_constants.G_constant * Math.Pow(period, 2)) / (4 * Math.Pow(Math.PI, 2));
				result = Math.Pow(prev_op, ((double)1 / (double)3));

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return 0.0;
			}
		}

		#endregion
	}
}
