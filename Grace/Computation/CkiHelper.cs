﻿using Grace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace.Computation
{
    public class CkiHelper
    {
        public static Dictionary<Int32, Double> CkiList(List<City> cities)
        {
            var N = cities.Count;
            var ckis = new Dictionary<Int32, Double>();
            foreach (var city in cities)
            {
                var cki = city.Distances.Where(d => d.Value > 0 && d.Value < 90000)
                    .ToDictionary(d => d.Key, d => d.Value)
                    .Keys.Count / (N - 1);
                ckis.Add(city.No, cki);
            }
            return ckis;
        }
    }
}
