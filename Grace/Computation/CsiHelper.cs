using Grace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace.Computation
{
    public class CsiHelper
    {
        public static Dictionary<Int32, Double> CsiList(List<City> cities, Double W)  //W 是所有点之间距离的加和
        {
            var csis = new Dictionary<Int32, Double>();
            foreach (var city in cities)
            {
                var csi = city.Distances.Values.Sum() / W;
                csis.Add(city.No, csi);
            }
            return csis;
        }
    }
}
