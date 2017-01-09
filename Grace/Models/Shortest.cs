using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace.Models
{
    public class Shortest
    {
        public Int32 StartCity { get; set; }
        public Int32 EndCity { get; set; }
        public List<Int32> InterCities { get; set; }
        public Double Distance { get; set; }
    }
}
