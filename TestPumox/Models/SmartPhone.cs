using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPumox.Models
{
    public class SmartPhone
    {
        public String Name { get; set; }
        public String System { get; set; }
        public String Resolution { get; set; }
        public int Ram { get; set; }
        public int Memory { get; set; }
        public float Diagonal { get; set; }
        public int Batery { get; set; }
        
        public String Processor { get; set; }
        public String Camera { get; set; }
    }
}
