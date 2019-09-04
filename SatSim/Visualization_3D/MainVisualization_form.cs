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

		HostingWPFUserControl.UserControl1 uc;
		public static MainVisualization_form GetInstance()
		{
			if (_instance == null) _instance = new MainVisualization_form();
			return _instance;
		}

		#endregion

		public MainVisualization_form()
		{
			InitializeComponent();
		}

		private void MainVisualization_form_FormClosing(object sender, FormClosingEventArgs e)
		{
			_instance = null;
		}

		public void Testing(double parsed)
		{
			uc.Test(parsed);
		}

		private void MainVisualization_form_Load(object sender, EventArgs e)
		{
			ElementHost host = new ElementHost();
			host.Dock = DockStyle.Fill;

			uc = new HostingWPFUserControl.UserControl1();

            // INITIALIZE VISUALIZATION PARAMETERS
            uc._data._EARTH_RADIUS = Sat_Constants.EARTH_RADIOUS_constant * Sat_Constants.VISUALIZATION3D_SCALE;

            host.Child = uc;

			this.Controls.Add(host);
		}
	}
}
