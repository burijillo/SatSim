using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GMap.NET.WindowsForms;
using GMap.NET;

namespace SatSim.MapForm
{
    public partial class MapsForm : Form
    {
        #region Singleton
        private static MapsForm _instance;

        public static MapsForm GetInstance()
        {
            if (_instance == null) _instance = new MapsForm();
            return _instance;
        }

        #endregion
        public MapsForm()
        {
            InitializeComponent();

            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl1.Position = new PointLatLng(-25.971684, 32.589759);


            GMapOverlay polyOverlay = new GMapOverlay("polygons");
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(new PointLatLng(-25.969562, 32.585789));
            points.Add(new PointLatLng(-25.966205 + 10, 32.588171 + 10));
            points.Add(new PointLatLng(-25.968134 + 10, 32.591647 + 10));
            points.Add(new PointLatLng(-25.971684, 32.589759));
            GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);
            polyOverlay.Polygons.Add(polygon);
            gMapControl1.Overlays.Add(polyOverlay);
        }

    }
}
