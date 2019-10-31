using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;

using SatSim.Methods.TLE_Scrap;
using SatSim.Methods.TLE_Data;
using SatSim.WaitForm;
using SatSim.Forms;
using SatSim.Visualization_3D;
using SatSim.Visualization_Graphs;

namespace SatSim
{
	public partial class MainForm : Form
	{
		public TLE_MultiSat_DataSet tle_dataset;
		public TLE_Scrap tle_scrap;

		TLE_dataBase_form tle_database_form;
        TLE_HistoricSelectedSatInfo_form tle_historic_form;
		MainVisualization_form main3DVisualization_form;
		MainGraphVisualization_form mainGraphVisualization_form;

		#region Initialize

		public MainForm()
		{
            InitializeComponent();

            

            tle_scrap = new TLE_Scrap();
			tle_dataset = TLE_MultiSat_DataSet.GetInstance();

			tle_database_form = TLE_dataBase_form.GetInstance(tle_scrap, tle_dataset);
			mainGraphVisualization_form = MainGraphVisualization_form.GetInstance(tle_scrap, tle_dataset);

            // Only when a satellite is selected this form would be available
            historicTLEDataToolStripMenuItem.Enabled = false;

			tle_database_form._tle_loaded_event += _tle_loaded_triggered;
			tle_database_form._tle_sat_selected_event += _tle_sat_selected_triggered;
		}

        #endregion

        #region TLE database

        private void loadDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			tle_database_form = TLE_dataBase_form.GetInstance(tle_scrap, tle_dataset);
			if (!tle_database_form.Visible)
			{
				tle_database_form.Show();
			}
			else
			{
				tle_database_form.BringToFront();
			}
		}

		public void _tle_loaded_triggered(object sender, EventArgs e)
		{
			TLELoadedEventToolStripLabel.Text = "TLE loaded";
			TLELoadedEventToolStripLabel.BackColor = Color.LightGreen;
		}

		public void _tle_sat_selected_triggered(object sender, EventArgs e)
		{
			SatSelectedEventToolStripLabel.Text = tle_dataset._TLE_Sat_Selected.Sat_Name;
			SatSelectedEventToolStripLabel.BackColor = Color.LightGreen;

            historicTLEDataToolStripMenuItem.Enabled = true;
		}

        #endregion

        #region Historic TLE
        private void tLEDataSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tle_historic_form = TLE_HistoricSelectedSatInfo_form.GetInstance(tle_scrap, tle_dataset);
            if (!tle_historic_form.Visible)
            {
                tle_historic_form.Show();
            }
            else
            {
                tle_historic_form.BringToFront();
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
		{
			main3DVisualization_form = MainVisualization_form.GetInstance(tle_dataset._TLE_Sat_Selected);
			if (!main3DVisualization_form.Visible)
			{
				main3DVisualization_form.Show();
			}
			else
			{
				main3DVisualization_form.BringToFront();
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			chart1.Series.Clear();

			double a = tle_dataset._TLE_Sat_Selected.Sat_SemiAxis;
			double ecc = tle_dataset._TLE_Sat_Selected.Sat_Eccentricity;
			double b = a * Math.Pow(1 - Math.Pow(ecc, 2), 0.5);

			Series serie = new Series();
			serie.ChartType = SeriesChartType.Spline;

			chart1.ChartAreas[0].Position.Height = 100;
			chart1.ChartAreas[0].Position.Width = 100;

			chart1.ChartAreas[0].AxisY.Maximum = 90000000;
			chart1.ChartAreas[0].AxisX.Maximum = 90000000;
			chart1.ChartAreas[0].AxisY.Minimum = -90000000;
			chart1.ChartAreas[0].AxisX.Minimum = -90000000;

			for (int j = 0; j < 360; j++)
			{
				double rad = (double)j  * (double)2 * Math.PI / (double)360;
				double x = a * Math.Cos(j);
				double y = b * Math.Sin(j);
				serie.Points.AddXY(x, y);
			}

			chart1.Series.Add(serie);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			mainGraphVisualization_form = MainGraphVisualization_form.GetInstance(tle_scrap, tle_dataset);
			if (!mainGraphVisualization_form.Visible)
			{
				mainGraphVisualization_form.Show();
			}
			else
			{
				mainGraphVisualization_form.BringToFront();
			}
		}
	}
}
