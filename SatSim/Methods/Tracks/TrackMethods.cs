using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using SatSim.Methods.TLE_Data;

namespace SatSim.Methods.Tracks
{
    class TrackMethods
    {
        private int debug_revs = 0;
        public void GetTrackCoordinates(double inclination, double arg_perigee, double semiaxis, double eccentricity, double period, double RAAN, double mean_motion, int iterations, out List<double> longitude, out List<double> latitude)
        {
            latitude = new List<double>();
            longitude = new List<double>();
            double earth_rotation = 2 * Math.PI / 86164;                                                // rad/s
            double greenwich_right_ascension = 60 * Math.PI / 180;
            double time = 0.0;

            double prev_true_anomaly = 0.0;

            double n = Math.Sqrt((Sat_Constants.G_constant * Sat_Constants.EARTH_MASS_constant) / Math.Pow(semiaxis, 3));
            n = 2 * Math.PI / period;

            for (int i = 0; i < iterations; i++)
            {
                // Auxiliar variables
                time = mean_motion * i * period / iterations;

                double true_anomaly = 0.0;

                //PSEUDO ITERATIVE CALC
                double M = n * (time);

                if (M > 2 * Math.PI)
                {
                    M = M - (2 * Math.PI);
                }

                true_anomaly = M + (2 * eccentricity - 0.25 * Math.Pow(eccentricity, 3)) * Math.Sin(M) + 5 / 4 * Math.Pow(eccentricity, 2) * Math.Sin(2 * M) + 13 / 12 * Math.Pow(eccentricity, 3) * Math.Sin(3 * M);

                bool debug_0 = (Math.Abs(arg_perigee * Math.PI / 180 + prev_true_anomaly) < (Math.PI / 2)) && (Math.Abs(arg_perigee * Math.PI / 180 + true_anomaly) > (Math.PI / 2));
                bool debug_1 = (Math.Abs(arg_perigee * Math.PI / 180 + prev_true_anomaly) < (3 * Math.PI / 2)) && (Math.Abs(arg_perigee * Math.PI / 180 + true_anomaly) > (3 * Math.PI / 2));

                bool debug_2 = debug_0 || debug_1;

                if ((i > 0) && debug_2)
                {
                    debug_revs++;
                }

                latitude.Add(GetTrackLatitude(inclination * Math.PI / 180, arg_perigee * Math.PI / 180, true_anomaly));

                longitude.Add(GetTrackLongitude(inclination * Math.PI / 180, arg_perigee * Math.PI / 180, true_anomaly, time, RAAN * Math.PI / 180, greenwich_right_ascension, earth_rotation));

                Debug.WriteLine(i + " --> " + time + " ; " + true_anomaly * 180 / Math.PI + " ; " + Math.Cos(inclination * Math.PI / 180) * Math.Tan(arg_perigee * Math.PI / 180 + true_anomaly) + " ; " + Math.Atan(Math.Cos(inclination * Math.PI / 180) * Math.Tan(arg_perigee * Math.PI / 180 + true_anomaly)) * 180 / Math.PI);
                Debug.WriteLine(RAAN * Math.PI / 180 - (earth_rotation * time + greenwich_right_ascension) + Math.Atan(Math.Cos(inclination * Math.PI / 180) * Math.Tan(arg_perigee * Math.PI / 180 + true_anomaly)));

                prev_true_anomaly = true_anomaly;
            }

            for (int i = 0; i < latitude.Count; i++)
            {
                Debug.WriteLine(i + "; " + debug_revs + " --> " + longitude[i] + "; " + latitude[i]);
            }

            Debug.WriteLine(semiaxis + " ; " + n);
        }

        private double GetTrackLatitude(double inclination, double arg_perigee, double true_anomaly)
        {
            double latitude = 0.0;

            double aux_0 = Math.Sin(inclination) * Math.Sin(arg_perigee + true_anomaly);
            latitude = Math.Asin(aux_0) * 180 / Math.PI;

            return latitude;
        }

        private double GetTrackLongitude(double inclination, double arg_perigee, double true_anomaly, double time, double RAAN, double greenwich_right_ascension, double earth_rotation)
        {
            double longitude = 0.0;

            double aux_0 = Math.Atan(Math.Cos(inclination) * Math.Tan(arg_perigee + true_anomaly));

            aux_0 += debug_revs * Math.PI;

            longitude = RAAN - (earth_rotation * time + greenwich_right_ascension) + aux_0;
            longitude = longitude * 180 / Math.PI - 180;

            while (longitude > 180)
            {
                longitude -= 360;
            }

            return longitude;
        }
    }
}
