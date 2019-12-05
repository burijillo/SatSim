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
		#region Singleton
		private static MainVisualization_form _instance;
		readonly List<TLE_Sat> _tle_sat_list;

		HostingWPFUserControl.UserControl1 uc;
		public static MainVisualization_form GetInstance(List<TLE_Sat> tle_Sat_list)
		{
			if (_instance == null) _instance = new MainVisualization_form(tle_Sat_list);
			return _instance;
		}

		#endregion

		public MainVisualization_form(List<TLE_Sat> tle_Sat_list)
		{
			_tle_sat_list = tle_Sat_list;
			InitializeComponent();
		}

		private void MainVisualization_form_FormClosing(object sender, FormClosingEventArgs e)
		{
			_instance = null;
		}

		private void MainVisualization_form_Load(object sender, EventArgs e)
		{
			ElementHost host = new ElementHost();
			host.Dock = DockStyle.Fill;

            OrbitsCalculator orbitsCalculator = new OrbitsCalculator();

            // Initialize array of orbits data. One for each satellite
            double[] _ECCENTRICITY = new double[_tle_sat_list.Count];
            double[] _INCLINATION = new double[_tle_sat_list.Count];
            double[] _SEMIAXIS = new double[_tle_sat_list.Count];
            double[] _RAAN = new double[_tle_sat_list.Count];
            double[] _PERIOD = new double[_tle_sat_list.Count];

            foreach (TLE_Sat tle_sat in _tle_sat_list)
            {
                int index = _tle_sat_list.IndexOf(tle_sat);
                _ECCENTRICITY[index] = tle_sat.Sat_Eccentricity;
                _INCLINATION[index] = tle_sat.Sat_Inclination;
                _SEMIAXIS[index] = tle_sat.Sat_SemiAxis / 10000;
                _RAAN[index] = tle_sat.Sat_RightAscension;
                _PERIOD[index] = 1 / tle_sat.Sat_MeanMotion;
            }

            uc = new HostingWPFUserControl.UserControl1(orbitsCalculator, _INCLINATION, _RAAN, _SEMIAXIS, _ECCENTRICITY, _PERIOD);

            uc._EARTH_RADIUS = Sat_Constants.EARTH_RADIOUS_constant * Sat_Constants.VISUALIZATION3D_SCALE;

            // This method initializes satellites into viewport
            uc.LoadSatellitesToViewPort(_tle_sat_list.Count);

			host.Child = uc;
			MainPanel.Controls.Add(host);
		}
	}
}
