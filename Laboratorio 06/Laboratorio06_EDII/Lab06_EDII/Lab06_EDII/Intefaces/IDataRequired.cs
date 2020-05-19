using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab06_EDII.Intefaces
{
    public interface IDataRequired<T>
    {
        public T ValueAB { get; set; }
        public T Rnd_ab { get; set; }
        public T p { get; set; }
        public T q { get; set; }
    }
}
