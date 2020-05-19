using Lab06_EDII.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab06_EDII.Models
{
    public class DataRequired : IDataRequired<int>
    {
        public int ValueAB { get; set; }
        public int Rnd_ab { get; set; }
        public int p { get; set; }
        public int q { get; set; }
    }
}
