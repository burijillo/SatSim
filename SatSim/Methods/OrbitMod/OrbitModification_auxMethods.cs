using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SatSim.Methods.OrbitMod
{
    class OrbitModification_auxMethods
    {
        public static double GetDeltaVel(double height)
        {
			try
			{
				double result = 0;

				// Test for circular orbits from earth
				result = Math.Sqrt(( 2 * TLE_Data.Sat_Constants.G_constant * TLE_Data.Sat_Constants.EARTH_MASS_constant) / (TLE_Data.Sat_Constants.EARTH_RADIOUS_constant * 1000 + height * 1000));

				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return 0;
			}
		}
    }
}
