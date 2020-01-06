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
using OxyPlot.Annotations;

using SatSim.Methods.TLE_Data;
using SatSim.Methods.TLE_Scrap;
using SatSim.WaitForm;

namespace SatSim.Forms
{
    public partial class TLE_HistoricSelectedSatInfo_form : Form
    {
        public static TLE_MultiSat_DataSet _tle_dataset;
        public static TLE_Scrap _tle_scrap;

        private bool _isDataLoaded = false;
        private bool _isAxisChanging = true;
        List<PointF> _plotPointsSerie = new List<PointF>();

        PlotModel MainPlotModel = new PlotModel();

        WaitingForm_simpleLine waitForm;
        BackgroundWorker getDatabase_bg;

        List<TLE_Sat> TLE_individualSat_ListProcessed = new List<TLE_Sat>();
        int repeated_data = 0;

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

            getDatabase_bg = new BackgroundWorker();
            getDatabase_bg.DoWork += GetDatabase_bg_DoWork;
            getDatabase_bg.RunWorkerCompleted += GetDatabase_bg_RunWorkerCompleted;

            AcceptButton = SearchHistoricTLEButton;
        }

        #endregion

        #region Main data limit selector

        private void GetDatabase_bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            waitForm.Close();

            FillMainInformationTabDataGridView();
            FillMainInformationTabTextBoxes();
        }

        private void GetDatabase_bg_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _isDataLoaded = false;

                byte[] input_raw;
                string selectedSatComboBoxText = "";
                string dataLimitTextBoxText = "";
                //DataTable tleDataTable = new DataTable();
                
                if (SelectedSatComboBox.InvokeRequired)
                {
                    SelectedSatComboBox.Invoke(new MethodInvoker(delegate { selectedSatComboBoxText = SelectedSatComboBox.Text; }));
                }
                if (DataLimitTextBox.InvokeRequired)
                {
                    DataLimitTextBox.Invoke(new MethodInvoker(delegate { dataLimitTextBoxText = DataLimitTextBox.Text; }));
                }

                uint DataLimit = Convert.ToUInt32(dataLimitTextBoxText);

                string TLE_selectedSat_ID = TLE_Data_AuxMethods.GetSatIDfromName(_tle_dataset._TLE_Sat_DataSet.Tables[0], selectedSatComboBoxText);
                input_raw = _tle_scrap.StartHistoricScrap(TLE_selectedSat_ID, DataLimit);

                List<byte[]> _raw_bytes_divided = TLE_IndividualSat_DataSet.TLE_Lines_Divider(input_raw);
                List<TLE_Sat> TLE_individualSat_List = TLE_IndividualSat_DataSet.TLE_Lines_DataExtractor(_raw_bytes_divided);
                TLE_individualSat_ListProcessed = TLE_IndividualSat_DataSet.TLE_HistoricData_Processed(TLE_individualSat_List);

                //int count = 1;
                //foreach (var item in TLE_individualSat_List)
                //{
                //    Debug.WriteLine("Counter " + count + ": " + item.Sat_Inclination);
                //    count++;
                //}

                _isDataLoaded = true;
                repeated_data = TLE_individualSat_List.Count - TLE_individualSat_ListProcessed.Count;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// This method searches for TLE information in web
        /// 
        /// Once information has been retrieved it shows into a datagridview and different textboxes with information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchHistoricTLEButton_Click(object sender, EventArgs e)
        {
            waitForm = new WaitingForm_simpleLine();
            waitForm.WaitFormLabel.Text = "Loading data from web...";
            waitForm.WaitFormPictureBox.ImageLocation = @"D:\Repos\SatSim\SatSim\Resources\Loading_infinite.gif";
            waitForm.Show();
            getDatabase_bg.RunWorkerAsync();

            _isAxisChanging = true;
        }

        #endregion

        #region Main information tab

        /// <summary>
        /// This method fill datagridview with TLE historic data
        /// </summary>
        /// <param name="input_TLE_list">Historic TLE data list</param>
        private void FillMainInformationTabDataGridView()
        {
            var list = new BindingList<TLE_Sat>(TLE_individualSat_ListProcessed);
            MainTLEHistoricInfoDataGridView.DataSource = list;
        }

        /// <summary>
        /// This method fills information text boxes in main information tab page
        /// </summary>
        /// <param name="input_TLE_list">Historic TLE data list</param>
        private void FillMainInformationTabTextBoxes()
        {
            int data_count = TLE_individualSat_ListProcessed.Count;

            // Fill data count textbox
            TLEDataCountTextBox.Text = Convert.ToString(data_count);

            // Fill repeated data count textbox
            TLERepeatedDataCountTextBox.Text = Convert.ToString(repeated_data);
        }

        private void TLEDataCountTextBox_TextChanged(object sender, EventArgs e)
        {
            // If this is called, the data is loaded for sure so some features are unlocked
            if (_isDataLoaded)
            {
                SeriesPlotComboBox.Items.Clear();
                TLEHistoricDataSetPlotTabPage.Show();

                // Load data into plot series combo box
                foreach (DataGridViewColumn item in MainTLEHistoricInfoDataGridView.Columns)
                {
                    //item as DataGridViewColumn;
                    SeriesPlotComboBox.Items.Add(item.HeaderText);
                }
                SeriesPlotComboBox.SelectedIndex = 12;
                SeriesPlotComboBox.Refresh();

                PlotDataButton.Enabled = true;
            }
        }

        #endregion

        #region Plot data tab

        /// <summary>
        /// In case the axis need updating, this method is called so the variable _isAxisChanging is set true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeriesPlotComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isAxisChanging = true;
        }

        private void PlotDataButton_Click(object sender, EventArgs e)
        {
            // Redundant check
            if(_isDataLoaded)
            {
                ResetSeries();

                int index = SeriesPlotComboBox.SelectedIndex;
                float counter = MainTLEHistoricInfoDataGridView.Rows.Count - 1;
                // Get latest date as initial year to iterate
                DateTime _initialDateTime = Convert.ToDateTime(MainTLEHistoricInfoDataGridView.Rows[1].Cells[22].Value.ToString());
                int _prevYear = _initialDateTime.Year;
                List<KeyValuePair<int, int>> YearIndexList = new List<KeyValuePair<int, int>>();

                foreach (DataGridViewRow item in MainTLEHistoricInfoDataGridView.Rows)
                {
                    if (item.Cells[index].Value != null)
                    {
                        // Check if year changes
                        int _actualYear = (Convert.ToDateTime(item.Cells[22].Value.ToString())).Year;
                        if (_actualYear != _prevYear)
                        {
                            YearIndexList.Add(new KeyValuePair<int, int>((int)counter, _actualYear + 1));
                        }
                        _prevYear = _actualYear;

                        //Debug.WriteLine(item.Cells[22].Value.ToString());
                        float value = float.Parse(item.Cells[index].Value.ToString().Replace(",", "."), CultureInfo.InvariantCulture);
                        _plotPointsSerie.Add(new PointF(counter, value));

                        counter--;
                    }
                }

                PlotDataIntoPlotView();
                PlotVerticalLinesYearsIntoPlotView(YearIndexList);
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

        private void PlotVerticalLinesYearsIntoPlotView(List<KeyValuePair<int, int>> YearIndexList)
        {
            // Get Y maximum and minimum
            double _Ymin = MainPlotModel.Axes[1].ActualMinimum;
            double _Ymax = MainPlotModel.Axes[1].ActualMaximum;

            // These vañues are not always updated, so the values are hardcoded big just in case
            if (_isAxisChanging)
            {
                _Ymin = -10000000;
                _Ymax = 10000000;
            }

            // Create annotations for year
            foreach(KeyValuePair<int, int> item in YearIndexList)
            {
                LineAnnotation annotation = new LineAnnotation();
                annotation.Color = OxyColors.Red;
                annotation.MinimumY = _Ymin;
                annotation.MaximumY = _Ymax;
                annotation.X = item.Key;
                annotation.LineStyle = LineStyle.Solid;
                annotation.Type = LineAnnotationType.Vertical;
                annotation.Text = item.Value.ToString();
                annotation.TextColor = OxyColors.LightSteelBlue;
                MainPlotModel.Annotations.Add(annotation);
            }
            
            HistoricDataPlotView.InvalidatePlot(true);

            _isAxisChanging = false;
        }

        private void ResetSeries()
        {
            // Clear plot points serie
            _plotPointsSerie.Clear();

            //TLE_individualSat_ListProcessed.Clear();
            repeated_data = 0;

            // Clear series and chartareas from historic plot
            MainPlotModel.Series.Clear();
            MainPlotModel.Annotations.Clear();
            MainPlotModel.Axes[0].Reset();
            MainPlotModel.Axes[1].Reset();
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
