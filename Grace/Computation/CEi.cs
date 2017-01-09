using Grace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace.Computation
{
    public class CEi
    {
        public static Dictionary<Int32, Double> List(List<City> cities)
        {
            var lambda1 = 10.0;
            var lambda2 = 5.0;
            var stop = 0.001;
            var CEi1 = new Dictionary<Int32, Double>();
            var N = cities.Count;
            cities.ForEach(c => CEi1.Add(c.No, 1));


            while (Math.Abs(lambda1 - lambda2) > stop)
            {
                var CEi2 = new Dictionary<Int32, Double>();
                foreach (var city in cities)
                {
                    var newCEi = city.Distances.Sum(c => CEi1[c.Key] * c.Value);
                    CEi2.Add(city.No, newCEi);
                }
                lambda1 = lambda2;
                lambda2 = Math.Sqrt(CEi2.Values.ToList().Sum(ei => ei * ei));
                CEi1 = new Dictionary<int, double>();
                foreach (var cei2 in CEi2)
                {
                    CEi1.Add(cei2.Key, cei2.Value / lambda2);
                }
            }
            return CEi1;
        }
    }
}
