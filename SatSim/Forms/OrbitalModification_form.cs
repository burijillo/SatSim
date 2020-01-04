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
using SatSim.Methods.OrbitMod;

namespace SatSim.Forms
{
    public partial class OrbitalModification_form : Form
    {
        #region Singleton

        private static OrbitalModification_form _instance;
        readonly TLE_Sat _tle_sat;
        public static OrbitalModification_form GetInstance(TLE_Sat tle_sat)
        {
            if (_instance == null) _instance = new OrbitalModification_form(tle_sat);
            return _instance;
        }

        #endregion

        public OrbitalModification_form(TLE_Sat tle_sat)
        {
            _tle_sat = tle_sat;
            InitializeComponent();
        }

        private void OrbitalModification_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instance = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(OrbitModification_auxMethods.GetDeltaVel(200));
        }
    }
}
