using Grace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace.Computation
{
    public static class CsiHelper
    {
        public static Double Si(this City city)
        {
            return city.Distances.Values.Sum();
        }
        public static Double Csi(this City city, Double W)  //W 是所有点之间距离的加和
        {
            return city.Distances.Values.Sum() / W;
        }
    }
}
