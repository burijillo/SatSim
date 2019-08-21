using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

using SatSim.Methods.TLE_Data;

namespace SatSim.Methods.TimeSeries
{
	public class TimeSeries_calcs
	{
		TLE_MultiSat_DataSet _tle_dataSet;

		public TimeSeries_calcs()
		{
			_tle_dataSet = TLE_MultiSat_DataSet.GetInstance();
		}

		#region Main handler

		public void Get_Sat_mainTimeSeries(uint iterations, tle_sat_serie_variables series_type)
		{
			try
			{
				switch (series_type)
				{
					case tle_sat_serie_variables.Inclination:
						Get_SatInclination_timeSeries(iterations);
						break;

					case tle_sat_serie_variables.RAAN:
						Get_SatRAAN_timeSeries(iterations);
						break;

					case tle_sat_serie_variables.Radious:
						Get_SatRadious_timeSeries(iterations);
						break;

					case tle_sat_serie_variables.Velocity:
						Get_SatVelocity_timeSeries(iterations);
						break;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		#endregion

		#region Calc methods

		public void Get_SatInclination_timeSeries(uint iterations)
		{
			try
			{
				List<PointF> result = new List<PointF>();
				for (uint i = 0; i < iterations; i++)
				{
					PointF data = new PointF(i, (float)Math.Sin(((double)i/(double)iterations) * 2 * Math.PI));
					result.Add(data);
					//_tle_dataSet._TLE_Sat_Selected.Sat_Inclination_series.Add(data);
				}
				_tle_dataSet._TLE_Sat_Selected.Sat_Inclination_series = result;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error while getting inclination timeseries", "Calc error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Debug.WriteLine(ex.ToString());
			}
		}

		public void Get_SatRAAN_timeSeries(uint iterations)
		{
			try
			{
				List<PointF> result = new List<PointF>();
				for (uint i = 0; i < iterations; i++)
				{
					PointF data = new PointF(i, 2 * (float)Math.Cos(((double)i / (double)iterations) * 2 * Math.PI));
					result.Add(data);
				}
				_tle_dataSet._TLE_Sat_Selected.Sat_RAAN_series = result;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error while getting RAAN timeseries", "Calc error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Debug.WriteLine(ex.ToString());
			}
		}

		public void Get_SatRadious_timeSeries(uint iterations)
		{
			try
			{
				float a = (float)_tle_dataSet._TLE_Sat_Selected.Sat_SemiAxis;
				float ecc = (float)_tle_dataSet._TLE_Sat_Selected.Sat_Eccentricity;
				float r_pos = (a * (1 - (float)Math.Pow(ecc, 2)));
				List<PointF> result = new List<PointF>();
				for (uint i = 0; i < iterations; i++)
				{
					float degrees = (float)i * 360 / (float)iterations;
					float rad = (float)degrees * (float)2 * (float)Math.PI / (float)360;
					float r_true = r_pos / (1 + ecc * (float)Math.Cos(rad));

					PointF data = new PointF(rad, r_true);
					result.Add(data);
				}
				_tle_dataSet._TLE_Sat_Selected.Sat_Radious_series = result;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error while getting radious timeseries", "Calc error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Debug.WriteLine(ex.ToString());
			}
		}

		public void Get_SatVelocity_timeSeries(uint iterations)
		{
			try
			{
				// Get radious from sat
				Get_SatRadious_timeSeries(iterations);
				List<PointF> radious_serie = _tle_dataSet._TLE_Sat_Selected.Sat_Radious_series;

				float a = (float)_tle_dataSet._TLE_Sat_Selected.Sat_SemiAxis;
				float aux_val = (float)Math.Sqrt(Sat_Constants.EARTH_MASS_constant * Sat_Constants.G_constant);

				List<PointF> result = new List<PointF>();
				for (int i = 0; i < iterations; i++)
				{
					float degrees = (float)i * 360 / (float)iterations;
					float rad = (float)degrees * (float)2 * (float)Math.PI / (float)360;
					float vel = aux_val * (float)Math.Sqrt((2 / radious_serie[i].Y) - (1 / a));

					PointF data = new PointF(rad, vel);
					result.Add(data);
				}
				_tle_dataSet._TLE_Sat_Selected.Sat_Velocity_series = result;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error while getting velocity timeseries", "Calc error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Debug.WriteLine(ex.ToString());
			}
		}

		#endregion
	}
}
