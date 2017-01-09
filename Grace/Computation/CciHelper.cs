using Grace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace.Computation
{
    public class CciHelper
    {
        public static Dictionary<Int32, Double> Cci(List<City> cities, List<Shortest> shortest, Int32 N)
        {
            var ccis = new Dictionary<Int32, Double>();
            foreach (var city in cities)
            {
                var ci = 1 / shortest.Where(s => s.StartCity == city.No).Select(s => s.Distance).Sum();
                var cci = ci * (N - 1);
                ccis.Add(city.No, cci);
            }
            return ccis;
        }
    }
}
