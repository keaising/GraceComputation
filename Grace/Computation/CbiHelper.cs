using Grace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace.Computation
{
    public class CbiHelper
    {
        /// <summary>
        /// 计算每个城市的介数
        /// </summary>
        /// <param name="shorts"></param>
        /// <param name="cities"></param>
        public static void Bi(List<Shortest> shorts, List<City> cities)
        {
            foreach (var sho in shorts)
            {
                foreach (var interCity in sho.InterCities)
                {
                    cities[interCity].Bi += 1;
                }
            }
        }

        public static Dictionary<Int32, Double> CbiList(List<City> cities)
        {
            var N = cities.Count;
            var cbis = new Dictionary<Int32, Double>();
            foreach (var city in cities)
            {
                double cbi = city.Bi * 2.0 / (Double)((N - 1) * (N - 2));
                cbis.Add(city.No, cbi);
            }
            return cbis;
        }
    }
}
