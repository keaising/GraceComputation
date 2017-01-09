using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace.Models
{
    public class City
    {
        public Int32 No { get; set; }
        public String Name { get; set; }
        public Dictionary<Int32, Double> Distances { get; set; } //距离
        
    }
}
