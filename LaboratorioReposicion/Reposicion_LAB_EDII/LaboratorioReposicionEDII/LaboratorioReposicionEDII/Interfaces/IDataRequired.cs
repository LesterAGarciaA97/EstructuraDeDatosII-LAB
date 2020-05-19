using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Interfaces
{
    interface IDataRequired<T>
    {
        public T Rnd_ab { get; set; }
        public T p { get; set; }
        public T q { get; set; }

    }
}
