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
using SatSim.MapForm;

using OxyPlot.WindowsForms;
using OxyPlot;
using OxyPlot.Axes;

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
        OrbitalModification_form orbitalModification_form;
        MapsForm mainMap_form;

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
            orbitModificationToolStripButton.Enabled = false;

            TLESatelliteDataGroupBox.Enabled = false;
		}

        #endregion

        #region TLE database

        private void loadDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
            tle_database_form = TLE_dataBase_form.GetInstance(tle_scrap, tle_dataset);

            // Events are loaded here because if the window is closed, the instance is null, so the new all to get an instance would get a NEW instance without this definition of events made
            tle_database_form._tle_loaded_event += _tle_loaded_triggered;
            tle_database_form._tle_sat_selected_event += _tle_sat_selected_triggered;

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
            orbitModificationToolStripButton.Enabled = true;

            // Main TLE Satellite Data Groupbox
            TLESatelliteDataGroupBox.Enabled = true;
            Fill_MainWindow_TLESatInfo_GroupBox();
            Paint_MainWindow_TLESatInfo_Orbit();
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

        #region TLE Satellite Data Group Box

        /// <summary>
        /// This method is used to fill all text boxes with the selected satellite data
        /// </summary>
        private void Fill_MainWindow_TLESatInfo_GroupBox()
        {
            try
            {
                // Main data
                SelectedSatNameTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_Name;
                SelectedSatYearTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_LaunchYear.ToString();
                SelectedSatNumberTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_LaunchNumber.ToString();
                SelectedSatPieceTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_LaunchPiece;
                SelectedSatIDTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_Number.ToString();
                SelectedSatDesignatorTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_Classification;
                SelectedSatClassificationTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_IntDesignator;
                SelectedSatSetTypeTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_SetType;
                SelectedSatSetNumberTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_SetNumber;

                // Orbital data
                SelectedSatInclinationTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_Inclination.ToString();
                SelectedSatRAANTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_RightAscension.ToString();
                SelectedSatMeanAnomalyTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_MeanAnomaly.ToString();
                SelectedSatEccentricityTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_Eccentricity.ToString();
                SelectedSatArgPerigeeTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_ArgumentPerigee.ToString();
                SelectedSatMeanMotionTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_MeanMotion.ToString();
                SelectedSatSemiaxisTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_SemiAxis.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

                // SGP4 data
                SelectedSatFirstDerTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_FirstMeanMotionDer.ToString();
                SelectedSatSecDerTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_SecMeanMotionDer.ToString();
                SelectedSatBDragTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_BSTARDrag.ToString();

                // Reference data
                SelectedSatEpochYearTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_EpochYear.ToString();
                SelectedSatEpochDayTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_EpochDay.ToString();
                SelectedSatEpochRevsTextBox.Text = tle_dataset._TLE_Sat_Selected.Sat_RevNumber.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// This method paints the selected satellite orbit plane
        /// </summary>
        private void Paint_MainWindow_TLESatInfo_Orbit()
        {
            // First erase last orbit in panel
            SatelliteOrbitPanel.Controls.Clear();

            PlotView _orbit_plot = new PlotView();
            PlotModel orbitModel = new PlotModel();

            _orbit_plot.Dock = DockStyle.Fill;
            _orbit_plot.BackColor = Color.DimGray;
            _orbit_plot.ForeColor = Color.White;

            orbitModel.TextColor = OxyColor.FromRgb(255, 255, 255);
            orbitModel.PlotAreaBorderColor = OxyColors.Transparent;

            _orbit_plot.Model = orbitModel;
            orbitModel.PlotType = PlotType.Cartesian;

            SatelliteOrbitPanel.Controls.Add(_orbit_plot);

            GetOrbitPainted(orbitModel);
            GetEarthPainted(orbitModel);
        }

        /// <summary>
        /// This method obtains up to 360 points of the selected satellite trajectory
        /// </summary>
        /// <param name="orbitModel"></param>
        private void GetOrbitPainted(PlotModel orbitModel)
        {
            double a = tle_dataset._TLE_Sat_Selected.Sat_SemiAxis;
            double ecc = tle_dataset._TLE_Sat_Selected.Sat_Eccentricity;

            double r_pos = (a * (1 - Math.Pow(ecc, 2)));

            OxyPlot.Series.ScatterSeries orbit = new OxyPlot.Series.ScatterSeries();
            orbit.MarkerSize = 1;
            orbit.MarkerStroke = OxyColor.FromRgb(255, 0, 0);
            orbit.MarkerStrokeThickness = 1;

            orbitModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1, AxislineColor = OxyColor.FromRgb(255, 255, 255), AxislineStyle = LineStyle.Solid, MajorGridlineColor = OxyColors.White });
            orbitModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1, AxislineColor = OxyColor.FromRgb(255, 255, 255), AxislineStyle = LineStyle.Solid });

            for (int j = 0; j < 360; j++)
            {
                double rad = (double)j * (double)2 * Math.PI / (double)360;
                double r_true = r_pos / (1 + ecc * Math.Cos(j));

                double x = r_true * Math.Cos(j);
                double y = r_true * Math.Sin(j);
                OxyPlot.Series.ScatterPoint point = new OxyPlot.Series.ScatterPoint(x, y);
                orbit.Points.Add(point);
            }

            orbitModel.Series.Add(orbit);
        }

        /// <summary>
        /// This method paints the circle of the earth
        /// </summary>
        /// <param name="orbitModel"></param>
        private void GetEarthPainted(PlotModel orbitModel)
        {
            OxyPlot.Annotations.EllipseAnnotation earth = new OxyPlot.Annotations.EllipseAnnotation();

            earth.Fill = OxyColor.FromRgb(0, 0, 0);
            earth.Width = 2 * Sat_Constants.EARTH_RADIOUS_constant * 1000;
            earth.Height = 2 * Sat_Constants.EARTH_RADIOUS_constant * 1000;

            orbitModel.Annotations.Add(earth);

            OxyPlot.Series.ScatterSeries serie = new OxyPlot.Series.ScatterSeries();
            serie.Points.Add(new OxyPlot.Series.ScatterPoint(0, 0));

            orbitModel.Series.Add(serie);
        }

        #endregion

        #region Orbit Modification

        private void orbitModificationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TLE_Sat tle_sat = tle_dataset._TLE_Sat_Selected;
            orbitalModification_form = OrbitalModification_form.GetInstance(tle_sat);
            if (!orbitalModification_form.Visible)
            {
                orbitalModification_form.Show();
            }
            else
            {
                orbitalModification_form.BringToFront();
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
		{
            List<TLE_Sat> TLESatList = new List<TLE_Sat>();
            TLESatList.Add(tle_dataset._TLE_Sat_Selected);
			main3DVisualization_form = MainVisualization_form.GetInstance(TLESatList);
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

        private void button4_Click(object sender, EventArgs e)
        {
            List<TLE_Sat> TLESatList = new List<TLE_Sat>();
            TLESatList.Add(tle_dataset._TLE_Sat_Selected);
            mainMap_form = MapsForm.GetInstance(TLESatList);
            if (!mainMap_form.Visible)
            {
                mainMap_form.Show();
            }
            else
            {
                mainMap_form.BringToFront();
            }
        }
    }
}
