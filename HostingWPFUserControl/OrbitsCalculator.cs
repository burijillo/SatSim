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
using System.Diagnostics;

using HostingWPFUserControl;

namespace SolarSystem
{
    class OrbitsCalculator : INotifyPropertyChanged
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

        public double SatelliteOrbitRadius { get { return 40; } set { } }
		public double SatelliteCoveredAngle { get; set; }
        public double Days { get; set; }
        public double EarthRotationAngle { get; set; }
        public double SatelliteRotationAngle { get; set; }
		public double SatelliteInclinationAngle { get; set; }
        public double SatelliteOrbitPositionX { get; set; }
        public double SatelliteOrbitPositionY { get; set; }
        public double SatelliteOrbitPositionZ { get; set; }
        public bool Paused { get; set; }

        public OrbitsCalculator()
        {
			SatelliteOrbitPositionX = SatelliteOrbitRadius;
            DaysPerSecond = 2;
            SatelliteInclinationAngle = 30;
            _satelliteInclinationAngle = SatelliteInclinationAngle;
        }

        public void StartTimer()
        {
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
			SatelliteOrbitPositionX = SatelliteOrbitRadius * Math.Cos(angle);
			SatelliteOrbitPositionY = SatelliteOrbitRadius * Math.Sin(angle);
			SatelliteOrbitPositionZ = SatelliteOrbitRadius * Math.Sin(angleInclination) * Math.Sin(angle);
			//Debug.WriteLine("HEEEY: days " + SatelliteOrbitRadius + "; x " + SatelliteOrbitPositionX + "; y " + SatelliteOrbitPositionY);
            Update("SatelliteOrbitPositionX");
            Update("SatelliteOrbitPositionY");
			Update("SatelliteOrbitPositionZ");
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
