using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace.Models
{
    public class City
    {
        private Dictionary<Int32, Double> _distances;
        public Int32 No { get; set; }
        public String Name { get; set; }
        public Dictionary<Int32, Double> Distances = new Dictionary<int, double>();
        public Int32 Bi { get; set; }
        public override String ToString()
        {
            return String.Format($"城市序号：{No}, 城市名字：{Name}, 度：{Distances?.Count}, 介数：{Bi}. ");
        }
    }
}
