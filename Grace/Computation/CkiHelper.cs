using Grace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace.Computation
{
    public static class CkiHelper
    {
        public static Int32 ki(this City city)
        {
            return city.Distances.Keys.Count;
        }

        public static Double Cki(this City city, Int32 N) //N:城市数量
        {
            return city.Distances.Keys.Count / (N - 1);
        }
    }
}
