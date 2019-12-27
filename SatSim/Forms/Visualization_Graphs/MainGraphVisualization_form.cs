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

using SatSim.Methods.TLE_Scrap;
using SatSim.Methods.TLE_Data;
using SatSim.Methods.TimeSeries;
using SatSim.Forms;

using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;

namespace SatSim.Visualization_Graphs
{
	public partial class MainGraphVisualization_form : Form
	{
		public static TLE_MultiSat_DataSet _tle_dataset;
		public static TLE_Scrap _tle_scrap;

		TimeSeries_calcs _timeSeries_calc;

		Dictionary<tle_sat_serie_variables, List<PointF>> _mainSeries_dict = new Dictionary<tle_sat_serie_variables, List<PointF>>();
		Dictionary<tle_sat_serie_variables, Color> _colorLegends_dict = new Dictionary<tle_sat_serie_variables, Color>();

		//ChartArea CA = new ChartArea();

		PlotView MainChart = new PlotView();
		PlotModel MainPlotModel = new PlotModel();

		#region Singleton
		private static MainGraphVisualization_form _instance;

		public static MainGraphVisualization_form GetInstance(TLE_Scrap tle_scrap, TLE_MultiSat_DataSet tle_dataset)
		{
			_tle_dataset = tle_dataset;
			_tle_scrap = tle_scrap;

			if (_instance == null) _instance = new MainGraphVisualization_form();
			return _instance;
		}

		#endregion

		#region Initialize

		public MainGraphVisualization_form()
		{
			InitializeComponent();

			InitializeGraphVisualizatio_form();

			TLE_dataBase_form tle_database_form = TLE_dataBase_form.GetInstance(_tle_scrap, _tle_dataset);

			tle_database_form._tle_sat_selected_event += _tle_sat_selected_triggered;
		}

		public void InitializeGraphVisualizatio_form()
		{
			if (_tle_dataset._TLE_LOADED)
			{
				mainTableLayoutPanel.Enabled = true;
				
				SatSelectedEventToolStripLabel.Text = "TLE loaded";
				SatSelectedEventToolStripLabel.BackColor = Color.LightGreen;
			}
			else
			{
				mainTableLayoutPanel.Enabled = false;
			}

			foreach(var item in Enum.GetNames(typeof(tle_sat_serie_variables)))
			{
				SeriesListBox.Items.Add(item);
			}

			MainChart.Dock = DockStyle.Fill;
			MainPlotModel.PlotType = PlotType.XY;
			MainPlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
			MainPlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1, MajorGridlineStyle = LineStyle.Solid });

			OxyColor background_color = new OxyColor();
			background_color = Color.DimGray.ToOxyColor();
			MainPlotModel.PlotAreaBackground = background_color;

			GraphPanel.Controls.Add(MainChart);

			MainChart.Model = MainPlotModel;

			//MainChart.ChartAreas.Add(CA);
			//MainChart.Titles.Add("Main chart");
		}

		#endregion

		private void MainGraphVisualization_form_FormClosing(object sender, FormClosingEventArgs e)
		{
			_instance = null;
		}

		#region Threads

		public void _tle_sat_selected_triggered(object sender, EventArgs e)
		{
			SatSelectedEventToolStripLabel.Text = _tle_dataset._TLE_Sat_Selected.Sat_Name;
			SatSelectedEventToolStripLabel.BackColor = Color.LightGreen;

			mainTableLayoutPanel.Enabled = true;
		}

		#endregion

		#region Chart manipulation

		private void SeriesListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ResetSeries();

				tle_sat_serie_variables series_type;
				Enum.TryParse(SeriesListBox.SelectedItem.ToString(), out series_type);
				bool _isSerieAlreadyAdded = CheckSerieInDict(series_type);
				if (!_isSerieAlreadyAdded)
				{
					_timeSeries_calc = new TimeSeries_calcs();
					_timeSeries_calc.Get_Sat_mainTimeSeries(Convert.ToUInt32(IterationsNumericUpDown.Text), series_type);
					SeriesType_handler(series_type);

					InitializeChartSeries();
					AddDataToChartSeries();

					// Iterations box is not enabled so all series have the same horizontal axis
					IterationsNumericUpDown.Enabled = false;
				}
				else
				{
					MessageBox.Show(series_type.ToString() + " is already added to the chart data", "Adding series error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error while adding series to charts", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Debug.WriteLine(ex.ToString());

				ResetSeries();
			}
		}

		public bool CheckSerieInDict(tle_sat_serie_variables series_type)
		{
			try
			{
				bool result = false;

				result = _mainSeries_dict.ContainsKey(series_type);

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return false;
			}
		}

		public void InitializeChartSeries()
		{
			// Get index from last data added
			int lastDataAdded_index = 0;
			lastDataAdded_index = _mainSeries_dict.Count - 1;

			//Series series = MainChart.Series.Add(_mainSeries_dict.Keys.ElementAt(lastDataAdded_index).ToString());
			//series.ChartType = SeriesChartType.Spline;
			//MainChart.Series[lastDataAdded_index].Color = _serieColor;

			//MainChart.Legends.Add(new Legend());

			// Check axis max and min
			//if (CA.AxisY.Maximum < Math.Ceiling(_mainSeries_dict[_mainSeries_dict.Keys.ElementAt(lastDataAdded_index)].Max(x => x.Y)))
			//{
			//	CA.AxisY.Maximum = Math.Ceiling(_mainSeries_dict[_mainSeries_dict.Keys.ElementAt(lastDataAdded_index)].Max(x => x.Y));
			//}

			//if (CA.AxisY.Minimum > Math.Floor(_mainSeries_dict[_mainSeries_dict.Keys.ElementAt(lastDataAdded_index)].Min(x => x.Y)))
			//{
			//	CA.AxisY.Minimum = Math.Floor(_mainSeries_dict[_mainSeries_dict.Keys.ElementAt(lastDataAdded_index)].Min(x => x.Y));
			//}

			MainChart.Model = MainPlotModel;
		}

		public void AddDataToChartSeries()
		{
			for(int i = 0; i < _mainSeries_dict.Count; i++)
			{
				List<PointF> _chart_list = _mainSeries_dict[_mainSeries_dict.Keys.ElementAt(i)];
				LineSeries serie = new LineSeries();

				serie.MarkerSize = 2;
				serie.MarkerFill = Color.LightBlue.ToOxyColor();
				serie.MarkerType = MarkerType.Circle;
				serie.LineStyle = LineStyle.Solid;
				serie.Color = Color.White.ToOxyColor();

				foreach (var item in _chart_list)
				{
					DataPoint point = new DataPoint(item.X, item.Y);
					serie.Points.Add(point);
				}

				MainPlotModel.Series.Add(serie);
				MainChart.InvalidatePlot(true);
			}
			//// Get index from last data added
			//int lastDataAdded_index = 0;
			//lastDataAdded_index = _mainSeries_dict.Count - 1;

			//List<PointF> _chart_list = _mainSeries_dict[_mainSeries_dict.Keys.ElementAt(lastDataAdded_index)];
			//LineSeries serie = new LineSeries();

			//serie.MarkerSize = 2;
			//serie.MarkerFill = Color.LightBlue.ToOxyColor();
			//serie.MarkerType = MarkerType.Circle;
			//serie.LineStyle = LineStyle.Solid;
			//serie.Color = Color.White.ToOxyColor();

			//foreach (var item in _chart_list)
			//{
			//	DataPoint point = new DataPoint(item.X, item.Y);
			//	serie.Points.Add(point);
			//}

			//MainPlotModel.Series.Add(serie);
			//MainChart.InvalidatePlot(true);
		}

		public void SeriesType_handler(tle_sat_serie_variables series_type)
		{
			try
			{
				// Add each type to the main list so the data can be represented later
				switch(series_type)
				{
					case tle_sat_serie_variables.Inclination:
						_mainSeries_dict.Add(series_type, _tle_dataset._TLE_Sat_Selected.Sat_Inclination_series);
						break;

					case tle_sat_serie_variables.RAAN:
						_mainSeries_dict.Add(series_type, _tle_dataset._TLE_Sat_Selected.Sat_RAAN_series);
						break;

					case tle_sat_serie_variables.Radious:
						_mainSeries_dict.Add(series_type, _tle_dataset._TLE_Sat_Selected.Sat_Radious_series);
						break;

					case tle_sat_serie_variables.Velocity:
						_mainSeries_dict.Add(series_type, _tle_dataset._TLE_Sat_Selected.Sat_Velocity_series);
						break;

					case tle_sat_serie_variables.Velocity_vs_Radious:
						_mainSeries_dict.Add(tle_sat_serie_variables.Velocity, _tle_dataset._TLE_Sat_Selected.Sat_Velocity_series);
						_mainSeries_dict.Add(tle_sat_serie_variables.Radious, _tle_dataset._TLE_Sat_Selected.Sat_Radious_series);
						break;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		private void ResetSeriesButton_Click(object sender, EventArgs e)
		{
			ResetSeries();
		}

		public void ResetSeries()
		{
			// Clear dicts
			_mainSeries_dict.Clear();
			_colorLegends_dict.Clear();

			// Iterations box is enabled as _mainSeries_List is already clear
			IterationsNumericUpDown.Enabled = true;

			//// Clear series and chartareas from MainChart
			MainPlotModel.Series.Clear();
			MainChart.InvalidatePlot(true);
			//MainChart.Series.Clear();
			//MainChart.Legends.Clear();
		}

		#endregion

		#region File strip



		#endregion

	}
}
