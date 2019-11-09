using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.Windows.Threading;
using System.Diagnostics;
using System.ComponentModel;

using System.Windows.Media.Media3D;
using SolarSystem;

namespace HostingWPFUserControl
{
	public class SinConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				double angle = System.Convert.ToDouble(value);
				double angleRad = Math.PI * angle / 180;
				double radius = System.Convert.ToDouble(parameter);
				return radius * Math.Sin(angleRad);
			}
			catch
			{
				return Binding.DoNothing;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
	/// <summary>
	/// Lógica de interacción para UserControl1.xaml
	/// </summary>
	public partial class UserControl1 : UserControl, INotifyPropertyChanged
    {
        public OrbitsCalculator _orbitsCalculator;
        private DispatcherTimer _timer;

        List<Ikriv.Surfaces.Sphere> sphereList = new List<Ikriv.Surfaces.Sphere>();
        List<Ikriv.Surfaces.Arc> orbitList = new List<Ikriv.Surfaces.Arc>();

        public double Days { get; set; }
        private double _daysPerSecond = 2;
        private double _satelliteInclinationAngle;
        public double SatelliteOrbitRadius { get { return 890; } set { } }
        public double SatelliteCoveredAngle { get; set; }
        public double SatelliteRotationAngle { get; set; }
        public double EarthRotationAngle { get; set; }

        public double _RAAN_ind { get; set; }
        public double[] SatelliteInclinationAngle { get; set; }
        //public double[] SatelliteOrbitPositionX { get; set; }
        //public double[] SatelliteOrbitPositionY { get; set; }
        //public double[] SatelliteOrbitPositionZ { get; set; }
        //public Point3D[] SatellitePointPosition { get; set; }

        public double _EARTH_RADIUS { get; set; }
        private double[] _INCLINATION { get; set; }
        private double[] _RAAN { get; set; }
        private double[] _SEMIAXIS { get; set; }
        private double[] _ECCENTRICITY { get; set; }
        private double[] _PERIOD { get; set; }

        private int tle_sat_count { get; set; }

        public string LabelInfo { get; set; }
        private double DaysPerSecond
        {
            get { return _daysPerSecond; }
            set { _daysPerSecond = value; Update("DaysPerSecond"); }
        }

        public UserControl1(OrbitsCalculator orbitsCalculator, double[] INCLINATION, double[] RAAN, double[] SEMIAXIS, double[] ECCENTRICITY, double[] PERIOD)
        {
            InitializeComponent();
            _orbitsCalculator = orbitsCalculator;
			//this.DataContext = orbitsCalculator;

            _INCLINATION = INCLINATION;
            _RAAN = RAAN;
            _SEMIAXIS = SEMIAXIS;
            _ECCENTRICITY = ECCENTRICITY;
            _PERIOD = PERIOD;

            tle_sat_count = INCLINATION.Count();
            SatelliteInclinationAngle = new double[tle_sat_count];
        }

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
            _orbitsCalculator.StartTimer();
            DaysPerSecond = 0.1;
            UpdateVariables();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += new EventHandler(OnTimerTick);
            Days = 0;
            _timer.Start();
        }
        void OnTimerTick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            Update("Days");
            UpdateVariables();
            OnTimeChanged();
            //Days += (now-_startTime).TotalMilliseconds * DaysPerSecond / 1000.0;
            Days += 10 * DaysPerSecond / 1000.0;
            //_startTime = now;
        }

        public void UpdateVariables()
        {
            Update("_EARTH_RADIUS");

            _RAAN_ind = _RAAN[0];
            Update("_RAAN_ind");
            Update("_INCLINATION");
            Update("_PERIOD");
            //LabelInfo = _EARTH_RADIUS.ToString();
            Update("LabelInfo");
            SatelliteInclinationAngle = _INCLINATION;
            EarthRotation();
            //_satelliteInclinationAngle = SatelliteInclinationAngle;
        }

        private void OnTimeChanged()
        {
            LabelInfo = Days.ToString();
            Update("_EARTH_RADIUS");
            for (int i = 0; i < tle_sat_count; i++)
            {
                Point3D satPosition = new Point3D();
                _orbitsCalculator.SatPosition(Days, _PERIOD[i], _RAAN[i], _SEMIAXIS[i], _ECCENTRICITY[i], _INCLINATION[i], out satPosition);
                //CheckOrbits(i);
                // TODO: Checkear que los cálculos para sacar estos parámetros están bien
                Ikriv.Surfaces.Sphere esferita = viewport.Children[viewport.Children.Count - (1 + tle_sat_count) - i] as Ikriv.Surfaces.Sphere;
                Ikriv.Surfaces.Arc orbita = viewport.Children[viewport.Children.Count - 1 - i] as Ikriv.Surfaces.Arc;
                esferita.Position = satPosition;
                orbita.SatPosition = satPosition;
            }
        }

        private void EarthRotation()
        {
            double EarthRotationPeriod = 1.0;

            EarthRotationAngle = Convert.ToDouble(360) * Days / EarthRotationPeriod;
            Update("EarthRotationAngle");
        }

        public void LoadSatellitesToViewPort(int tle_sat_count)
		{
            
            /*Ikriv.Surfaces.Sphere testerSphere = new Ikriv.Surfaces.Sphere();
            testerSphere.Radius = 25;
            testerSphere.Position = new Point3D(10, 10, 10);
            testerSphere.Visible = true;
            testerSphere.Material = viewport.Resources["GreyGoo"] as Material;
            testerSphere.BackMaterial = viewport.Resources["GreyGoo"] as Material;
            this.viewport.Children.Add(testerSphere);

            Ikriv.Surfaces.Sphere testerSphere2 = new Ikriv.Surfaces.Sphere();
            testerSphere2.Radius = 950;
            testerSphere2.Position = new Point3D(1000, 1000, 1000);
            testerSphere2.Visible = true;
            testerSphere2.Material = viewport.Resources["GreyGoo"] as Material;
            testerSphere2.BackMaterial = viewport.Resources["GreyGoo"] as Material;
            testerSphere2.Transform.Transform(SatellitePointPosition);
            this.viewport.Children.Add(testerSphere2);*/

            for(int i = 0; i < tle_sat_count; i++)
            {
                sphereList.Add(new Ikriv.Surfaces.Sphere() { BackMaterial = viewport.Resources["SatelliteCover"] as Material, Material = viewport.Resources["SatelliteCover"] as Material, Visible = true, Radius = 25 });
                viewport.Children.Add(sphereList[sphereList.Count - 1]);
            }

            for(int i = 0; i < tle_sat_count; i++)
            {
                orbitList.Add(new Ikriv.Surfaces.Arc() { BackMaterial = viewport.Resources["RedGoo"] as Material, Material = viewport.Resources["RedGoo"] as Material, Visible = true });
                viewport.Children.Add(orbitList[orbitList.Count - 1]);
            }

            //sphereList.Add(testerSphere2);
            //this.viewport.Children.Add(new ModelVisual3D() { Content = (ModelVisual3D)testerSphere });

            ////testerSphere.Transform.Transform()
            //viewport.Children.Add(testerSphere);
            //sstoryboard.Stop();
            //animation.Duration = new Duration(TimeSpan.FromSeconds(parsed));
            //sstoryboard.Begin();
            //Storyboard.Pause(this);
            //Storyboard.SetSpeedRatio(parsed);
            //animation.Duration = new Duration(TimeSpan.FromSeconds(parsed));
            //Storyboard.Resume();
        }

        /*public void CheckOrbits(int i)
        {
                double angle = 2 * Math.PI * Days / _PERIOD[i];

                int augmentedInc = 0;

                if (InclinationEvol)
                {
                    augmentedInc = 3 * Convert.ToInt32(Math.Floor(Days / Convert.ToDouble(_PERIOD)));
                    SatelliteInclinationAngle = _satelliteInclinationAngle + augmentedInc;
                }
                else
                {
                    SatelliteInclinationAngle = _satelliteInclinationAngle + augmentedInc;
                }

                double angleInclination = 2 * Math.PI / 360 * SatelliteInclinationAngle[i];
                SatelliteCoveredAngle = angle;
                //SatelliteOrbitPositionX = SatelliteOrbitRadius * Math.Cos(angle);
                //SatelliteOrbitPositionY = SatelliteOrbitRadius * Math.Sin(angle);
                //SatelliteOrbitPositionZ = SatelliteOrbitRadius * Math.Sin(angleInclination) * Math.Sin(angle);

                //--------------TEST--------------

                double RAAN = _RAAN[i] * Math.PI / 180;
                double b = _SEMIAXIS[i] * Math.Pow(1 - Math.Pow(_ECCENTRICITY[i], 2), 0.5);
                // Get focus distance to center (negative)
                double c = -Math.Sqrt(Math.Pow(_SEMIAXIS[i], 2) - Math.Pow(b, 2));

                double r_pos = (_SEMIAXIS[i] * (1 - Math.Pow(_ECCENTRICITY[i], 2)));

                double r_true = r_pos / (1 + _ECCENTRICITY[i] * Math.Cos(angle));

                double SatelliteOrbitPositionX0 = r_true * Math.Cos(angle);
                double SatelliteOrbitPositionY0 = r_true * Math.Sin(angle);
                double SatelliteOrbitPositionZ0 = 0;

                // Change axis via axis X rotation -> from (x0,y0,z0) to (x1,y1,z1)
                double SatelliteOrbitPositionX1 = SatelliteOrbitPositionX0;
                double SatelliteOrbitPositionY1 = SatelliteOrbitPositionY0 * Math.Cos(angleInclination) - SatelliteOrbitPositionZ0 * Math.Sin(angleInclination);
                double SatelliteOrbitPositionZ1 = SatelliteOrbitPositionY0 * Math.Sin(angleInclination) + SatelliteOrbitPositionZ0 * Math.Cos(angleInclination);

                // Change axis via axis X rotation -> from (x1,y1,z1) to (x2,y2,z2)
                SatelliteOrbitPositionX[i] = SatelliteOrbitPositionX1 * Math.Cos(RAAN) - SatelliteOrbitPositionY1 * Math.Sin(RAAN);
                SatelliteOrbitPositionY[i] = SatelliteOrbitPositionX1 * Math.Sin(RAAN) + SatelliteOrbitPositionY1 * Math.Cos(RAAN);
                SatelliteOrbitPositionZ[i] = SatelliteOrbitPositionZ1;

                SatellitePointPosition[i] = new Point3D(SatelliteOrbitPositionX[i], SatelliteOrbitPositionY[i], SatelliteOrbitPositionZ[i]);


                //--------------TEST--------------

                //LabelInfo = Days.ToString("F05") + "\n" + _RAAN + "\n" + SatelliteInclinationAngle + "\n" + (r_true * 10).ToString("F05") + "\n" + (angle / (2 * Math.PI)).ToString("F05");

                //Debug.WriteLine("HEEEY: days " + SatelliteOrbitRadius + "; x " + SatelliteOrbitPositionX + "; y " + SatelliteOrbitPositionY);
                Update("SatelliteOrbitPositionX");
                Update("SatelliteOrbitPositionY");
                Update("SatelliteOrbitPositionZ");
                Update("SatellitePointPosition");
                //Update("Days");
                Update("LabelInfo");
                Update("SatelliteCoveredAngle");
                Update("SatelliteInclinationAngle"); 
            
        }*/

        private void Update(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var args = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("SALIIIII");
            //_orbitsCalculator.StopTimer();
		}
	}
}
