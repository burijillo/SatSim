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

using SatSim.WaitForm;
using SatSim.Forms;
using SatSim.Methods.TLE_Data;
using SatSim.Methods.TLE_Scrap;

namespace SatSim.Forms
{
	public enum FilterType_enum : uint
	{
		Equal = 0,
		Greater = 1,
		Less = 2,
	}
	public partial class TLE_dataBase_form : Form
	{
		WaitingForm_simpleLine waitForm;
		BackgroundWorker getDatabase_bg;

		TLE_SelectedSatInfo_form tle_selectedSat_form;
		SelectedSatOrbit_form selectedSatOrbit_form;

		public event EventHandler _tle_loaded_event;
		public event EventHandler _tle_sat_selected_event;
		Stopwatch sw;

		public static TLE_MultiSat_DataSet _tle_dataset;
		public static TLE_Scrap _tle_scrap;

		#region Singleton
		private static TLE_dataBase_form _instance;
		public static TLE_dataBase_form GetInstance(TLE_Scrap tle_scrap, TLE_MultiSat_DataSet tle_dataset)
		{
			_tle_dataset = tle_dataset;
			_tle_scrap = tle_scrap;

			if (_instance == null) _instance = new TLE_dataBase_form();
			return _instance;
		}

		#endregion

		#region Initialize
		public TLE_dataBase_form()
		{
			InitializeComponent();
			Initialize_TLE_dataBase_form();

			sw = new Stopwatch();

			_tle_loaded_event += _tle_loaded_triggered;
			_tle_sat_selected_event += _tle_sat_selected_triggered;

			getDatabase_bg = new BackgroundWorker();
			getDatabase_bg.DoWork += GetDatabase_bg_DoWork;
			getDatabase_bg.RunWorkerCompleted += GetDatabase_bg_RunWorkerCompleted;
		}

		public void Initialize_TLE_dataBase_form()
		{
			if (_tle_dataset._TLE_LOADED)
			{
				SatelliteDataGroupBox.Enabled = true;
				TLEFilterTableLayoutPanel.Enabled = true;

				ShowTLESatData();

				TLELoadedEventToolStripLabel.Text = "TLE loaded";
				TLELoadedEventToolStripLabel.BackColor = Color.LightGreen;
			}
			else
			{
				SatelliteDataGroupBox.Enabled = false;
				TLEFilterTableLayoutPanel.Enabled = false;
			}
		}

		#endregion

		#region Threads

		private void GetDatabase_bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			waitForm.Close();
			sw.Stop();

			// Invoke _tle_loaded_event event
			_tle_loaded_event?.Invoke(sender, e);

			ShowElapsedTime();
			ShowTLESatData();
		}
		public void _tle_loaded_triggered(object sender, EventArgs e)
		{
			TLELoadedEventToolStripLabel.Text = "TLE loaded";
			TLELoadedEventToolStripLabel.BackColor = Color.LightGreen;

			_tle_dataset._TLE_LOADED = true;

			EnableFilterTLEDataBase();
		}
		public void _tle_sat_selected_triggered(object sender, EventArgs e)
		{
			// Por ahora en este windows form este evento es indiferente
		}

		private void GetDatabase_bg_DoWork(object sender, DoWorkEventArgs e)
		{
			sw.Start();
			byte[] returndata = _tle_scrap.StartScrap();
			DataSet _dataSet;
			List<TLE_Sat> lista = _tle_dataset.GetTLEDataSet(returndata, out _dataSet);			
		}

		private void ShowTLESatData()
		{
			TLESatDataGridView.DataSource = null;
			TLESatDataGridView.Rows.Clear();
			TLESatDataGridView.Columns.Clear();

			TLESatDataGridView.DataSource = _tle_dataset._TLE_Sat_DataSet;
			TLESatDataGridView.AutoGenerateColumns = true;
			TLESatDataGridView.DataMember = "Sat_table";
		}

		private void ShowElapsedTime()
		{
			TimeElapsedToolStripLabel.Text = sw.ElapsedMilliseconds.ToString();
		}

		#endregion

		#region Closing
		private void TLE_dataBase_form_FormClosing(object sender, FormClosingEventArgs e)
		{
			_instance = null;
		}

		#endregion

		#region Database Load and Save
		private void GetAllSatellitesDBButton_Click(object sender, EventArgs e)
		{
			if (!_tle_dataset._TLE_LOADED)
			{
				waitForm = new WaitingForm_simpleLine();
				waitForm.WaitFormLabel.Text = "Loading data from web...";
				waitForm.WaitFormPictureBox.ImageLocation = @"D:\Repos\SatSim\SatSim\Resources\Loading_infinite.gif";
				waitForm.Show();
				getDatabase_bg.RunWorkerAsync();
			}
			else
			{
				ShowTLESatData();
			}
		}

		private void saveConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveDialog = new SaveFileDialog();

			saveDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
			saveDialog.FilterIndex = 1;
			saveDialog.RestoreDirectory = true;

			if (saveDialog.ShowDialog() == DialogResult.OK)
			{
				_tle_dataset.SaveTLEDataSet_xml(saveDialog.FileName, _tle_dataset._TLE_Sat_DataSet);
			}
		}
		private void loadConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openDialog = new OpenFileDialog();

			openDialog.InitialDirectory = @"C:\Users\adeus\Desktop";
			openDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
			openDialog.FilterIndex = 1;
			//openDialog.RestoreDirectory = true;

			if (openDialog.ShowDialog() == DialogResult.OK)
			{
				string filePath = openDialog.FileName;

				_tle_dataset.LoadTLEDataSet_xml(filePath);
				_tle_loaded_event?.Invoke(sender, e);

				ShowTLESatData();
			}
		}

		private void EnableFilterTLEDataBase()
		{
			if(_tle_dataset._TLE_LOADED)
			{
				SatelliteDataGroupBox.Enabled = true;
				TLEFilterTableLayoutPanel.Enabled = true;
			}

			// Initialize combo boxes
			foreach(var item in Enum.GetNames(typeof(tle_sat_variables)))
			{
				TLEFilterRefComboBox.Items.Add(item);
			}
			TLEFilterRefComboBox.SelectedIndex = 0;

			foreach (var item in Enum.GetNames(typeof(FilterType_enum)))
			{
				TLEFilterFilterTypeComboBox.Items.Add(item);
			}
			TLEFilterFilterTypeComboBox.SelectedIndex = 0;

			TLEFilterValueTextBox.Text = "0";
		}
		private void TLEFilterSetFilterButton_Click(object sender, EventArgs e)
		{
			try
			{
				string strOp = "";
				FilterType_enum filterType;
				Enum.TryParse(TLEFilterFilterTypeComboBox.SelectedItem.ToString(), out filterType);

				// Get filter type
				switch (filterType)
				{
					case FilterType_enum.Equal:
						strOp = @"=";
						break;
					case FilterType_enum.Greater:
						strOp = @">";
						break;
					case FilterType_enum.Less:
						strOp = @"<";
						break;
				}
				string strExpr = TLEFilterRefComboBox.SelectedItem.ToString() + " " + strOp + " " + TLEFilterValueTextBox.Text;

				DataTable dt = new DataTable();
				dt = _tle_dataset._TLE_Sat_DataSet.Tables[0].Select(strExpr).CopyToDataTable();

				TLESatDataGridView.DataSource = dt;
			}
			catch (InvalidOperationException opEx)
			{
				MessageBox.Show("No data found with those parameters", "Filter error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Debug.WriteLine(opEx.ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		private void TLEFilterResetFilterButton_Click(object sender, EventArgs e)
		{
			try
			{
				TLESatDataGridView.DataSource = _tle_dataset._TLE_Sat_DataSet;
				TLESatDataGridView.AutoGenerateColumns = true;
				TLESatDataGridView.DataMember = "Sat_table";
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		#endregion

		#region Satellite selection

		// This events triggers when selection changes from datagridview (better than when click is registered)
		private void TLESatDataGridView_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				int i = (int)TLESatDataGridView.SelectedRows[0].Cells[0].Value;

				// Make TLE_DataSet Satellite selection
				bool isSatSelected = _tle_dataset.GetSelectedSat_fromDataRow(_tle_dataset._TLE_Sat_DataSet.Tables[0].Rows[i]);

				if (isSatSelected && _tle_dataset._SAT_SELECTED)
				{
					ShowTLESatSelectedData();

					// Invoke _tle_sat_selected_event event
					_tle_sat_selected_event?.Invoke(sender, e);
				}
				else
				{
					MessageBox.Show("Satellite selection not completed", "Selection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		private void ShowTLESatSelectedData()
		{
			try
			{
				// Main data
				SelectedSatNameTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_Name;
				SelectedSatYearTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_LaunchYear.ToString();
				SelectedSatNumberTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_LaunchNumber.ToString();
				SelectedSatPieceTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_LaunchPiece;
				SelectedSatIDTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_Number.ToString();
				SelectedSatDesignatorTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_Classification;
				SelectedSatClassificationTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_IntDesignator;
				SelectedSatSetTypeTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_SetType;
				SelectedSatSetNumberTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_SetNumber;

				// Orbital data
				SelectedSatInclinationTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_Inclination.ToString();
				SelectedSatRAANTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_RightAscension.ToString();
				SelectedSatMeanAnomalyTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_MeanAnomaly.ToString();
				SelectedSatEccentricityTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_Eccentricity.ToString();
				SelectedSatArgPerigeeTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_ArgumentPerigee.ToString();
				SelectedSatMeanMotionTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_MeanMotion.ToString();

				// SGP4 data
				SelectedSatFirstDerTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_FirstMeanMotionDer.ToString();
				SelectedSatSecDerTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_SecMeanMotionDer.ToString();
				SelectedSatBDragTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_BSTARDrag.ToString();

				// Reference data
				SelectedSatEpochYearTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_EpochYear.ToString();
				SelectedSatEpochDayTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_EpochDay.ToString();
				SelectedSatEpochRevsTextBox.Text = _tle_dataset._TLE_Sat_Selected.Sat_RevNumber.ToString();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		private void TLESelectedSatMoreInformationLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// TODO: Coger informacion de pagina web
			tle_selectedSat_form = TLE_SelectedSatInfo_form.GetInstance();
			if (!tle_selectedSat_form.Visible)
			{
				tle_selectedSat_form.Show();
				tle_selectedSat_form.Text = _tle_dataset._TLE_Sat_Selected.Sat_Name;
				tle_selectedSat_form.ShowSelectedSatAdInfo(_tle_dataset._TLE_Sat_Selected.Sat_LaunchYear, _tle_dataset._TLE_Sat_Selected.Sat_LaunchNumber, _tle_dataset._TLE_Sat_Selected.Sat_LaunchPiece);
			}
			else
			{
				tle_selectedSat_form.BringToFront();
			}
		}

		private void SeletedSatShowOrbitButton_Click(object sender, EventArgs e)
		{
			selectedSatOrbit_form = SelectedSatOrbit_form.GetInstance(_tle_scrap, _tle_dataset);
			if (!selectedSatOrbit_form.Visible)
			{
				selectedSatOrbit_form.Show();
			}
			else
			{
				selectedSatOrbit_form.BringToFront();
			}
		}

		#endregion
	}
}
