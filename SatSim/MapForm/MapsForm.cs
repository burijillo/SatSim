using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SatSim.Methods.Tracks;
using SatSim.Methods.TLE_Data;

using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;

namespace SatSim.MapForm
{
    public partial class MapsForm : Form
    {
        #region Singleton
        private static MapsForm _instance;
        readonly List<TLE_Sat> _tle_sat_list;

        public static MapsForm GetInstance(List<TLE_Sat> tle_Sat_list)
        {
            if (_instance == null) _instance = new MapsForm(tle_Sat_list);
            return _instance;
        }

        #endregion
        public MapsForm(List<TLE_Sat> tle_Sat_list)
        {
            _tle_sat_list = tle_Sat_list;
            InitializeComponent();

            LoadGroundTrack();
        }

        private void LoadGroundTrack()
        {
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            GMapOverlay markersOverlay = new GMapOverlay("markers");

            //GMapOverlay polyOverlay = new GMapOverlay("polygons");
            //List<PointLatLng> points = new List<PointLatLng>();

            // Test for coordinates
            TrackMethods _trackMethods = new TrackMethods();
            foreach (TLE_Sat sat in _tle_sat_list)
            {
                List<double> longitude = new List<double>();
                List<double> latitude = new List<double>();
                double sat_period = 86164 / sat.Sat_MeanMotion;
                _trackMethods.GetTrackCoordinates(sat.Sat_Inclination, sat.Sat_ArgumentPerigee, sat.Sat_SemiAxis, sat.Sat_Eccentricity, sat_period, sat.Sat_RightAscension, sat.Sat_MeanMotion, 1000, out longitude, out latitude);
                //_trackMethods.GetTrackCoordinates(20, 270, 42164, 0.3, 86160, 60, 0, 360, out longitude, out latitude);

                for (int i = 1; i < longitude.Count; i++)
                {
                    //points.Add(new PointLatLng(latitude[i - 1], longitude[i - 1]));
                    //points.Add(new PointLatLng(latitude[i - 1] + 10, longitude[i - 1] + 10));
                    //points.Add(new PointLatLng(latitude[i] + 10, longitude[i] + 10));
                    //points.Add(new PointLatLng(latitude[i], longitude[i]));

                    GMarkerCross marker = new GMarkerCross(new PointLatLng(latitude[i], longitude[i]));
                    marker.Pen = new Pen(Color.Yellow);
                    markersOverlay.Markers.Add(marker);

                    marker.IsVisible = true;

                    //gMapControl1.Update();
                }
            }

            gMapControl1.Overlays.Add(markersOverlay);


            //GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
            //polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            //polygon.Stroke = new Pen(Color.Red, 1);
            //polyOverlay.Polygons.Add(polygon);
            //gMapControl1.Overlays.Add(polyOverlay);
        }

        private void MapsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instance = null;
        }
    }
}
