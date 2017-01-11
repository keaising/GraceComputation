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
        /// <summary>
        /// 最短距离
        /// </summary>
        /// <param name="cities"></param>
        /// <param name="shortest"></param>
        /// <returns></returns>
        public static Dictionary<Int32, Double> CciW(List<City> cities, List<Shortest> shortest)
        {
            var N = cities.Count;
            var ccis = new Dictionary<Int32, Double>();
            foreach (var city in cities)
            {
                var ci = 1 / shortest.Where(s => s.StartCity == city.No).Select(s => s.Distance).Sum();
                var cci = ci * (N - 1);
                ccis.Add(city.No, cci);
            }
            return ccis;
        }

        /// <summary>
        /// 边数
        /// </summary>
        /// <param name="cities"></param>
        /// <param name="shortest"></param>
        /// <returns></returns>
        public static Dictionary<Int32, Double> Cci2(List<City> cities, List<Shortest> shortest)
        {
            var N = cities.Count;
            var ccis = new Dictionary<Int32, Double>();
            foreach (var city in cities)
            {
                var ci = 1 / shortest.Where(s => s.StartCity == city.No).Select(s => s.InterCities.Count + 1).Sum();
                var cci = ci * (N - 1);
                ccis.Add(city.No, cci);
            }
            return ccis;
        }
    }
}
