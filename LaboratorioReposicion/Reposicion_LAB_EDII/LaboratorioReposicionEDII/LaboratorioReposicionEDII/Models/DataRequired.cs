using LaboratorioReposicionEDII.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Models
{
    public class DataRequired : IDataRequired<int>
    { 
        public int Rnd_ab { get; set; }
        public int p { get; set; }
        public int q { get; set; }
    }
}
