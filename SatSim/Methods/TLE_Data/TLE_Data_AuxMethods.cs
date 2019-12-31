using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;

using SatSim.Methods.TLE_Data;

namespace SatSim.Methods.TLE_Data
{
	class TLE_Data_AuxMethods
	{
		public static uint GetLaunchYearFromDesignator(string designator)
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

		public static uint GetLaunchNumberFromDesignator(string designator)
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
		public static string GetLaunchPieceFromDesignator(string designator)
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

		public static double GetDoubleFromScientificNotation(string input_data)
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

		public static uint GetYearFromEpoch(string epoch)
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

		public static double GetDayFromEpoch(string epoch)
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

		public static int GetMonthAndDayFromEpoch(string epoch, out int day)
		{
			int result = 0;
			DateTimeFormatInfo dtfi = DateTimeFormatInfo.CurrentInfo;

			day = Convert.ToInt32(epoch.Substring(2, 3));
			int year = Convert.ToInt32(GetYearFromEpoch(epoch));

			for (int i = 0; i < dtfi.MonthNames.Length - 1; i++)
			{
				if (String.IsNullOrEmpty(dtfi.MonthNames[i]))
					continue;

				// Check month
				int daysOfMonth = DateTime.DaysInMonth(year, i + 1);
				if ((day - daysOfMonth) <= 0) return i + 1;
				else day = day - daysOfMonth;
			}

			return result;
		}

		public static void GetTimeOfDayFromEpoch(string epoch, out int hour, out int minute, out int second)
		{
			double fractionOfDay = Convert.ToDouble("0" + epoch.Substring(5, 9), System.Globalization.CultureInfo.InvariantCulture);

			// Get hour
			hour = Convert.ToInt32(Math.Floor(24 * fractionOfDay));
			// Get minute
			double fractionOfHour = ((24 * fractionOfDay) - Math.Floor(24 * fractionOfDay));
			minute = Convert.ToInt32(Math.Floor(60 * fractionOfHour));
			// Get second
			double fractionOfMinute = ((60 * fractionOfHour) - Math.Floor(60 * fractionOfHour));
			second = Convert.ToInt32(Math.Floor(60 * fractionOfMinute));
		}

		public static bool GetDateTimeFromEpoch(string epoch, out DateTime dateTime_output)
		{
			try
			{
				// Select year
				int year = Convert.ToInt32(GetYearFromEpoch(epoch));
				// Select month and day, checking if year is leap
				int day;
				int month = GetMonthAndDayFromEpoch(epoch, out day);
				// Select time of day
				int hour, minute, second;
				GetTimeOfDayFromEpoch(epoch, out hour, out minute, out second);

				dateTime_output = new DateTime(year, month, day, hour, minute, second);

				return true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				dateTime_output = DateTime.Now;
				return false;
			}
		}

		public static double GetSemiAxisFromPeriod(double mean_motion)
		{
			try
			{
				double result = 0.0;

				// (Seconds in a day) / (mean_motion)
				double period = (double)(60 * 60 * 24) / mean_motion;

				double prev_op = (Sat_Constants.EARTH_MASS_constant * Sat_Constants.G_constant * Math.Pow(period, 2)) / (4 * Math.Pow(Math.PI, 2));
				result = Math.Pow(prev_op, ((double)1 / (double)3));

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return 0.0;
			}
		}

		public static double GetPerigee(double semiaxis, double eccentricity)
		{
			try
			{
				double result = 0.0;

				result = semiaxis * (1 - eccentricity);

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return 0.0;
			}
		}
		public static double GetApogee(double semiaxis, double eccentricity)
		{
			try
			{
				double result = 0.0;

				result = semiaxis * (1 + eccentricity);

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return 0.0;
			}
		}

		public static string GetSatIDfromName(DataTable input_table, string input_name)
        {
            try
            {
                string result = "";

                foreach(DataRow row in input_table.Rows)
                {
                    if (row.ItemArray[1].ToString() == input_name)
                    {
                        int index = input_table.Rows.IndexOf(row);
                        result = row.ItemArray[2].ToString();
                        return result;
                    }
                }

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
