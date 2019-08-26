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

using SatSim.Methods.TLE_Data;
using SatSim.Methods.TLE_Scrap;

namespace SatSim.Forms
{
    public partial class TLE_HistoricSelectedSatInfo_form : Form
    {
        public static TLE_MultiSat_DataSet _tle_dataset;
        public static TLE_Scrap _tle_scrap;

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

            DataLimitTextBox.Text = "1";

            // For short time, later on this will check all sats
            foreach(DataRow item in _tle_dataset._TLE_Sat_DataSet.Tables[0].Rows)
            {
                SelectedSatComboBox.Items.Add(item.ItemArray[1].ToString());
            }
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
                byte[] input_raw;
                string TLE_selectedSat_ID = TLE_Data_AuxMethods.GetSatIDfromName(_tle_dataset._TLE_Sat_DataSet.Tables[0], SelectedSatComboBox.Text);
                uint DataLimit = Convert.ToUInt32(DataLimitTextBox.Text);
                input_raw = _tle_scrap.StartHistoricScrap(TLE_selectedSat_ID, DataLimit);

                List<byte[]> _raw_bytes_divided = TLE_IndividualSat_DataSet.TLE_Lines_Divider(input_raw);
                List<TLE_Sat> TLE_individualSat_List = TLE_IndividualSat_DataSet.TLE_Lines_DataExtractor(_raw_bytes_divided);

                int count = 1;
                foreach (var item in TLE_individualSat_List)
                {
                    Debug.WriteLine("Counter " + count + ": " + item.Sat_Inclination);
                    count++;
                }

                Debug.WriteLine(string.Format("Sat name: {0}, sat id: {1}", SelectedSatComboBox.Text, TLE_selectedSat_ID));

                FillMainInformationTabDataGridView(TLE_individualSat_List);
                FillMainInformationTabTextBoxes(TLE_individualSat_List);
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

        #endregion

        #region Closing

        private void TLE_HistoricSelectedSatInfo_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instance = null;
        }

        #endregion
    }
}
