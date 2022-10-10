using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.medálok
{
    public class OlympicResult
    {
        public int Year { get; set; }
        public string Country { get; set; }
        public  string[] Medals { get; set; }
        public int Position { get; set; }


    }
}
