using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;

using OxyPlot.WindowsForms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Annotations;

namespace SatSim.Methods.TLE_Scrap
{
	public partial class SelectedSatOrbit_form : Form
	{
		public static TLE_DataSet _tle_dataset;
		public static TLE_Scrap _tle_scrap;
		public Sat_Constants _sat_constants = new Sat_Constants();

		PlotView _main_plot = new PlotView();
		PlotModel orbitModel = new PlotModel();

		#region Singleton
		private static SelectedSatOrbit_form _instance;
		public static SelectedSatOrbit_form GetInstance(TLE_Scrap tle_scrap, TLE_DataSet tle_dataset)
		{
			_tle_dataset = tle_dataset;
			_tle_scrap = tle_scrap;

			if (_instance == null) _instance = new SelectedSatOrbit_form();
			return _instance;
		}

		#endregion

		private void SelectedSatOrbit_form_FormClosing(object sender, FormClosingEventArgs e)
		{
			_instance = null;
		}

		#region Initialize

		public SelectedSatOrbit_form()
		{
			InitializeComponent();

			Initialize_SelectedSatOrbit_form();
		}

		public void Initialize_SelectedSatOrbit_form()
		{
			_main_plot.Dock = DockStyle.Fill;

			_main_plot.Model = orbitModel;
			orbitModel.PlotType = PlotType.Cartesian;

			mainPanel.Controls.Add(_main_plot);

			GetOrbit();
			GetEarth();
		}

		#endregion

		#region Main chart controller

		public void GetOrbit()
		{
			double a = _tle_dataset._TLE_Sat_Selected.Sat_SemiAxis;
			double ecc = _tle_dataset._TLE_Sat_Selected.Sat_Eccentricity;
			double b = a * Math.Pow(1 - Math.Pow(ecc, 2), 0.5);
			// Get focus distance to center (negative)
			double c = - Math.Sqrt(Math.Pow(a, 2) - Math.Pow(b, 2));

			double r_pos = (a * (1 - Math.Pow(ecc, 2)));

			OxyPlot.Series.ScatterSeries orbit = new OxyPlot.Series.ScatterSeries();
			orbit.MarkerSize = 1;
			orbit.MarkerStroke = OxyColor.FromRgb(255, 0, 0);
			orbit.MarkerStrokeThickness = 1;

			orbitModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
			orbitModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1 });

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

		public void GetEarth()
		{
			OxyPlot.Annotations.EllipseAnnotation earth = new OxyPlot.Annotations.EllipseAnnotation();

			earth.Fill = OxyColor.FromRgb(0, 0, 0);
			earth.Width = 2 * _sat_constants.EARTH_RADIOUS_constant * 1000;
			earth.Height = 2 * _sat_constants.EARTH_RADIOUS_constant * 1000;

			orbitModel.Annotations.Add(earth);

			OxyPlot.Series.ScatterSeries serie = new OxyPlot.Series.ScatterSeries();
			serie.Points.Add(new OxyPlot.Series.ScatterPoint(0,0));

			orbitModel.Series.Add(serie);

			//OxyPlot.Series.LineSeries serie = new OxyPlot.Series.LineSeries();

			//serie.MarkerSize = 1;
			//serie.MarkerStroke = OxyColor.FromRgb(255, 0, 0);
			//serie.MarkerStrokeThickness = 1;
			////serie.MarkerType = MarkerType.None;

			//double a = _tle_dataset._TLE_Sat_Selected.Sat_SemiAxis;
			//double ecc = _tle_dataset._TLE_Sat_Selected.Sat_Eccentricity;
			//double b = a * Math.Pow(1 - Math.Pow(ecc, 2), 0.5);

			//orbitModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
			//orbitModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1 });

			//OxyPlot.Annotations.EllipseAnnotation ellipse = new OxyPlot.Annotations.EllipseAnnotation();
			//ellipse.Width = a/2;
			//ellipse.Height = b/2;
			//ellipse.Fill = OxyColor.FromRgb(0, 255, 0);

			//orbitModel.Annotations.Add(ellipse);


			//for (int j = 0; j < 360; j++)
			//{
			//	double rad = (double)j * (double)2 * Math.PI / (double)360;
			//	double x = a * Math.Cos(j);
			//	double y = b * Math.Sin(j);
			//	OxyPlot.DataPoint point = new OxyPlot.DataPoint(x, y);
			//	serie.Points.Add(point);
			//}

			//orbitModel.Series.Add(serie);
		}

		#endregion
	}
}
