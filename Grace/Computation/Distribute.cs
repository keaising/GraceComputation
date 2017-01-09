using Grace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace.Computation
{
    public class Distribute
    {
        public static Dictionary<Int32, Double> K(List<City> cities)
        {
            var ks = new Dictionary<Int32, Double>();
            foreach (var city in cities)
            {
                var k = city.Distances.Keys.Count;
                if (ks.Keys.Contains(k))
                {
                    ks[k] += 1;
                }
                else
                {
                    ks.Add(k, 1);
                }
            }
            return ks;
        }
        public static Dictionary<Int32, Double> Si(List<City> cities)
        {
            var sis = new Dictionary<Int32, Double>();
            foreach (var city in cities)
            {
                var si = (Int32)Math.Ceiling(city.Distances.Values.Sum());
                if (sis.Keys.Contains(si))
                {
                    sis[si] += 1;
                }
                else
                {
                    sis.Add(si, 1);
                }
            }
            return sis;
        }
    }
}
