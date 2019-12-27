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
using System.Globalization;

using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;

using SatSim.Methods.TLE_Data;
using SatSim.Methods.TLE_Scrap;

namespace SatSim.Forms
{
    public partial class TLE_HistoricSelectedSatInfo_form : Form
    {
        public static TLE_MultiSat_DataSet _tle_dataset;
        public static TLE_Scrap _tle_scrap;

        private bool _isDataLoaded = false;
        List<PointF> _plotPointsSerie = new List<PointF>();

        PlotModel MainPlotModel = new PlotModel();

        #region Singleton

        private static TLE_HistoricSelectedSatInfo_form _instance;
        public static TLE_HistoricSelectedSatInfo_form GetInstance(TLE_Scrap tle_scrap, TLE_MultiSat_DataSet tle_dataset)
        {
            _tle_dataset = tle_dataset;
            _tle_scrap = tle_scrap;

            if (_instance == null) _instance = new TLE_HistoricSelectedSatInfo_form();
            return _instance;
        }

        #endregion

        #region Initialize

        public TLE_HistoricSelectedSatInfo_form()
        {
            InitializeComponent();

            DataLimitTextBox.Text = "100";

            // For short time, later on this will check all sats
            foreach(DataRow item in _tle_dataset._TLE_Sat_DataSet.Tables[0].Rows)
            {
                SelectedSatComboBox.Items.Add(item.ItemArray[1].ToString());
            }

            // Apparently not working
            TLEHistoricDataSetPlotTabPage.Hide();
            PlotDataButton.Enabled = false;

            MainPlotModel.PlotType = PlotType.XY;
            MainPlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
            MainPlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1, MajorGridlineStyle = LineStyle.Solid });

            OxyColor background_color = new OxyColor();
            background_color = Color.DimGray.ToOxyColor();
            MainPlotModel.PlotAreaBackground = background_color;

            HistoricDataPlotView.Model = MainPlotModel;
        }

        #endregion

        #region Main data limit selector

        /// <summary>
        /// This method searches for TLE information in web
        /// 
        /// Once information has been retrieved it shows into a datagridview and different textboxes with information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchHistoricTLEButton_Click(object sender, EventArgs e)
        {
            try
            {
                _isDataLoaded = false;

                byte[] input_raw;
                string TLE_selectedSat_ID = TLE_Data_AuxMethods.GetSatIDfromName(_tle_dataset._TLE_Sat_DataSet.Tables[0], SelectedSatComboBox.Text);
                uint DataLimit = Convert.ToUInt32(DataLimitTextBox.Text);
                input_raw = _tle_scrap.StartHistoricScrap(TLE_selectedSat_ID, DataLimit);

                List<byte[]> _raw_bytes_divided = TLE_IndividualSat_DataSet.TLE_Lines_Divider(input_raw);
                List<TLE_Sat> TLE_individualSat_List = TLE_IndividualSat_DataSet.TLE_Lines_DataExtractor(_raw_bytes_divided);
                List<TLE_Sat> TLE_individualSat_ListProcessed = TLE_IndividualSat_DataSet.TLE_HistoricData_Processed(TLE_individualSat_List);

                //int count = 1;
                //foreach (var item in TLE_individualSat_List)
                //{
                //    Debug.WriteLine("Counter " + count + ": " + item.Sat_Inclination);
                //    count++;
                //}

                Debug.WriteLine(string.Format("Sat name: {0}, sat id: {1}", SelectedSatComboBox.Text, TLE_selectedSat_ID));

                _isDataLoaded = true;

                FillMainInformationTabDataGridView(TLE_individualSat_ListProcessed);
                FillMainInformationTabTextBoxes(TLE_individualSat_ListProcessed);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        #endregion

        #region Main information tab

        /// <summary>
        /// This method fill datagridview with TLE historic data
        /// </summary>
        /// <param name="input_TLE_list">Historic TLE data list</param>
        private void FillMainInformationTabDataGridView(List<TLE_Sat> input_TLE_list)
        {
            var list = new BindingList<TLE_Sat>(input_TLE_list);
            MainTLEHistoricInfoDataGridView.DataSource = list;
        }

        /// <summary>
        /// This method fills information text boxes in main information tab page
        /// </summary>
        /// <param name="input_TLE_list">Historic TLE data list</param>
        private void FillMainInformationTabTextBoxes(List<TLE_Sat> input_TLE_list)
        {
            int data_count = input_TLE_list.Count;

            // Fill data count textbox
            TLEDataCountTextBox.Text = Convert.ToString(data_count);
        }

        private void TLEDataCountTextBox_TextChanged(object sender, EventArgs e)
        {
            // If this is called, the data is loaded for sure so some features are unlocked
            if (_isDataLoaded)
            {
                TLEHistoricDataSetPlotTabPage.Show();

                // Load data into plot series combo box
                foreach (DataGridViewColumn item in MainTLEHistoricInfoDataGridView.Columns)
                {
                    //item as DataGridViewColumn;
                    SeriesPlotComboBox.Items.Add(item.HeaderText);
                }
                SeriesPlotComboBox.SelectedItem = 0;

                PlotDataButton.Enabled = true;
            }
        }

        #endregion

        #region Plot data tab

        private void PlotDataButton_Click(object sender, EventArgs e)
        {
            // Redundant check
            if(_isDataLoaded)
            {
                ResetSeries();

                int index = SeriesPlotComboBox.SelectedIndex;
                float counter = 0;

                foreach (DataGridViewRow item in MainTLEHistoricInfoDataGridView.Rows)
                {
                    if (item.Cells[index].Value != null)
                    {
                        float value = float.Parse(item.Cells[index].Value.ToString().Replace(",", "."), CultureInfo.InvariantCulture);
                        Debug.WriteLine(value);
                        _plotPointsSerie.Add(new PointF(counter, value));

                        counter++;
                    }
                }

                PlotDataIntoPlotView();
            }
        }

        private void PlotDataIntoPlotView()
        {
            LineSeries serie = new LineSeries();

            serie.MarkerSize = 2;
            serie.MarkerFill = Color.LightBlue.ToOxyColor();
            serie.MarkerType = MarkerType.Circle;
            serie.LineStyle = LineStyle.Solid;
            serie.Color = Color.White.ToOxyColor();
            serie.Smooth = true;

            foreach (var item in _plotPointsSerie)
            {
                DataPoint point = new DataPoint(item.X, item.Y);
                serie.Points.Add(point);
            }

            MainPlotModel.Series.Add(serie);
            HistoricDataPlotView.InvalidatePlot(true);
        }

        private void ResetSeries()
        {
            // Clear plot points serie
            _plotPointsSerie.Clear();

            // Clear series and chartareas from historic plot
            MainPlotModel.Series.Clear();
            HistoricDataPlotView.InvalidatePlot(true);
        }

        #endregion

        #region Closing

        private void TLE_HistoricSelectedSatInfo_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instance = null;
        }

        #endregion
    }
}
