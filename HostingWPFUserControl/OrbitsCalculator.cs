/* Copyright 2009 Ivan Krivyakov

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */
using System;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Diagnostics;

using HostingWPFUserControl;

namespace SolarSystem
{
    public class OrbitsCalculator : INotifyPropertyChanged
    {
        private DateTime _startTime;
        private double _startDays;
        private DispatcherTimer _timer;

        const double EarthYear = 30;
        const double SatelliteRotationPeriod = 10000.0;
        const double EarthRotationPeriod = 2.0;
        const double TwoPi = Math.PI * 2;

        private double _daysPerSecond = 2;
        private double _satelliteInclinationAngle;
        public double DaysPerSecond
        {
            get { return _daysPerSecond; }
            set { _daysPerSecond = value; Update("DaysPerSecond"); }
        }

        public double SatelliteOrbitRadius { get { return 890; } set { } }
		public double SatelliteCoveredAngle { get; set; }
        public double Days { get; set; }
        public double EarthRotationAngle { get; set; }
        public double SatelliteRotationAngle { get; set; }
		public double SatelliteInclinationAngle { get; set; }
        public double SatelliteOrbitPositionX { get; set; }
        public double SatelliteOrbitPositionY { get; set; }
        public double SatelliteOrbitPositionZ { get; set; }
		public Point3D SatellitePointPosition { get; set; }
        public bool Paused { get; set; }

		public double _EARTH_RADIUS { get; set; }

        public OrbitsCalculator()
        {
			SatelliteOrbitPositionX = SatelliteOrbitRadius;
            DaysPerSecond = 2;
            SatelliteInclinationAngle = 30;
            _satelliteInclinationAngle = SatelliteInclinationAngle;
        }

		public void UpdateVariables()
		{
			Update("_EARTH_RADIUS");
		}

        public void StartTimer()
        {
			UpdateVariables();
            _startTime = DateTime.Now;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += new EventHandler(OnTimerTick);
			Days = 0;
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
            _timer.Tick -= OnTimerTick;
            _timer = null;
        }

        public void Pause(bool doPause)
        {
            if (doPause)
            {
                StopTimer();
            }
            else
            {
                StartTimer();
            }
        }

        void OnTimerTick(object sender, EventArgs e)
        {
			var now = DateTime.Now;
			Update("Days");
			OnTimeChanged();
            //Days += (now-_startTime).TotalMilliseconds * DaysPerSecond / 1000.0;
			Days += 10 * DaysPerSecond / 1000.0;
            _startTime = now;
        }

        private void OnTimeChanged()
        {
			SatPosition();
            EarthRotation();
            SatelliteRotation();
        }

        private void SatPosition()
        {
            double angle = 2 * Math.PI * Days / EarthYear;

			int augmentedInc = 3 * Convert.ToInt32(Math.Floor(Days / Convert.ToDouble(EarthYear)));
			//Debug.WriteLine("AUG " + augmentedInc + "; DAYS " + Days);
			SatelliteInclinationAngle = _satelliteInclinationAngle + augmentedInc;

			double angleInclination = 2 * Math.PI / 360 * SatelliteInclinationAngle;
			SatelliteCoveredAngle = angle;
			//SatelliteOrbitPositionX = SatelliteOrbitRadius * Math.Cos(angle);
			//SatelliteOrbitPositionY = SatelliteOrbitRadius * Math.Sin(angle);
			//SatelliteOrbitPositionZ = SatelliteOrbitRadius * Math.Sin(angleInclination) * Math.Sin(angle);

			//--------------TEST--------------

			double a = 950;
			double ecc = 0.2;
			double RAAN = 80 * Math.PI / 180;
			double b = a * Math.Pow(1 - Math.Pow(ecc, 2), 0.5);
			// Get focus distance to center (negative)
			double c = -Math.Sqrt(Math.Pow(a, 2) - Math.Pow(b, 2));

			double r_pos = (a * (1 - Math.Pow(ecc, 2)));

			double r_true = r_pos / (1 + ecc * Math.Cos(angle));

			double SatelliteOrbitPositionX0 = r_true * Math.Cos(angle);
			double SatelliteOrbitPositionY0 = r_true * Math.Sin(angle);
			double SatelliteOrbitPositionZ0 = 0;

			// Change axis via axis X rotation -> from (x0,y0,z0) to (x1,y1,z1)
			double SatelliteOrbitPositionX1 = SatelliteOrbitPositionX0;
			double SatelliteOrbitPositionY1 = SatelliteOrbitPositionY0 * Math.Cos(angleInclination) - SatelliteOrbitPositionZ0 * Math.Sin(angleInclination);
			double SatelliteOrbitPositionZ1 = SatelliteOrbitPositionY0 * Math.Sin(angleInclination) + SatelliteOrbitPositionZ0 * Math.Cos(angleInclination);

			// Change axis via axis X rotation -> from (x1,y1,z1) to (x2,y2,z2)
			SatelliteOrbitPositionX = SatelliteOrbitPositionX1 * Math.Cos(RAAN) - SatelliteOrbitPositionY1 * Math.Sin(RAAN);
			SatelliteOrbitPositionY = SatelliteOrbitPositionX1 * Math.Sin(RAAN) + SatelliteOrbitPositionY1 * Math.Cos(RAAN);
			SatelliteOrbitPositionZ = SatelliteOrbitPositionZ1;

			SatellitePointPosition = new Point3D(SatelliteOrbitPositionX, SatelliteOrbitPositionY, SatelliteOrbitPositionZ);

			//--------------TEST--------------

			//Debug.WriteLine("HEEEY: days " + SatelliteOrbitRadius + "; x " + SatelliteOrbitPositionX + "; y " + SatelliteOrbitPositionY);
            Update("SatelliteOrbitPositionX");
            Update("SatelliteOrbitPositionY");
			Update("SatelliteOrbitPositionZ");
			Update("SatellitePointPosition");
            Update("Days");
			Update("SatelliteCoveredAngle");
			Update("SatelliteInclinationAngle");
        }

        private void EarthRotation()
        {
            EarthRotationAngle = 360 * Days / EarthRotationPeriod;
            Update("EarthRotationAngle");
        }

        private void SatelliteRotation()
        {
			SatelliteRotationAngle = 360 * Days / SatelliteRotationPeriod;
            Update("SatelliteRotationAngle");
        }

        private void Update(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var args = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
