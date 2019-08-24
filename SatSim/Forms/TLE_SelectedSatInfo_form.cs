using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SatSim.Methods.TLE_Scrap;

namespace SatSim.Forms
{
	public partial class TLE_SelectedSatInfo_form : Form
	{
		public TLE_Scrap tle_scrap;

		#region Singleton
		private static TLE_SelectedSatInfo_form _instance;
		public static TLE_SelectedSatInfo_form GetInstance()
		{
			if (_instance == null) _instance = new TLE_SelectedSatInfo_form();
			return _instance;
		}

		#endregion
		public TLE_SelectedSatInfo_form()
		{
			InitializeComponent();

			tle_scrap = new TLE_Scrap();
		}

		public void ShowSelectedSatAdInfo(uint launchYear, uint launchNumber, string launchPiece)
		{
			AdditionalInfoRichTextBox.Text = tle_scrap.GetAdditionalSatInfo(launchYear, launchNumber, launchPiece);
		}

		private void TLE_SelectedSatInfo_form_FormClosing(object sender, FormClosingEventArgs e)
		{
			_instance = null;
		}
	}
}
