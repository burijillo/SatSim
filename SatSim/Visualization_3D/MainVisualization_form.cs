using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

using SatSim.Methods.TLE_Data;
using SolarSystem;

namespace SatSim.Visualization_3D
{
	public partial class MainVisualization_form : Form
	{
		// TODO: PASAR LOS PUNTOS Y QUE SE PINTEN LOS PUNTOS ALREDEDOR DE LA TIERRA (con las vistas que tengan que ser)
		#region Singleton
		private static MainVisualization_form _instance;
		TLE_Sat _tle_sat;

		HostingWPFUserControl.UserControl1 uc;
		public static MainVisualization_form GetInstance(TLE_Sat tle_Sat)
		{
			if (_instance == null) _instance = new MainVisualization_form(tle_Sat);
			return _instance;
		}

		#endregion

		public MainVisualization_form(TLE_Sat tle_Sat)
		{
			_tle_sat = tle_Sat;
			InitializeComponent();
		}

		private void MainVisualization_form_FormClosing(object sender, FormClosingEventArgs e)
		{
			_instance = null;
		}

		public void Testing(double parsed)
		{
			uc.Test();
		}

		private void MainVisualization_form_Load(object sender, EventArgs e)
		{
			ElementHost host = new ElementHost();
			host.Dock = DockStyle.Fill;

			uc = new HostingWPFUserControl.UserControl1();
			
			// INITIALIZE VISUALIZATION PARAMETERS
			uc._data._EARTH_RADIUS = Sat_Constants.EARTH_RADIOUS_constant * Sat_Constants.VISUALIZATION3D_SCALE;
			uc._data._ECCENTRICITY = _tle_sat.Sat_Eccentricity;
			uc._data._INCLINATION = _tle_sat.Sat_Inclination;
			uc._data._SEMIAXIS = _tle_sat.Sat_SemiAxis / 10000;
			uc._data._RAAN = _tle_sat.Sat_RightAscension;
			uc._data._PERIOD = 1 / _tle_sat.Sat_MeanMotion;
			//uc._data._INCLINATION = 10;
			//uc._data._RAAN = 30;
			//uc._data._SEMIAXIS = 950;
			//uc._data._ECCENTRICITY = 0.3;

			host.Child = uc;
			MainPanel.Controls.Add(host);

			//this.Controls.Add(host);
		}

		private void PauseResetButton_Click(object sender, EventArgs e)
		{
			if (!uc._data.Paused)
			{
				PauseResetButton.Text = "Reset";
				uc._data.Pause(true);
			}
			else
			{
				PauseResetButton.Text = "Pause";
				uc._data.Pause(false);
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.CheckState == CheckState.Checked)
			{
				uc._data.InclinationEvol = false;
			}
			else
			{
				uc._data.InclinationEvol = true;
			}
		}
	}
}
