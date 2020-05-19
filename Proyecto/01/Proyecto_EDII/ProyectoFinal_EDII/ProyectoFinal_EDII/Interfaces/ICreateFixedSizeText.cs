using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoFinal_EDII.Interfaces;

namespace ProyectoFinal_EDII.Interfaces
{
    public interface ICreateFixedSizeText<T> where T : IFixedSizeText
    {
        T Create(string FixedSizeText);
        T CreateNull();
    }
}
