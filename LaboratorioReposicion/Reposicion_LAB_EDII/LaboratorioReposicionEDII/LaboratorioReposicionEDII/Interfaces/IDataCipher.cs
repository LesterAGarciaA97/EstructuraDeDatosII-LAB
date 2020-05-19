using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Interfaces
{
    interface IDataCipher<T>
    {
        IFormFile ArchivoCargado { get; set; }
        T TamanoCarriles { get; set; }
        string NuevoNombre { get; set; }
        string Llave { get; set; }
        T n { get; set; }
        T m { get; set; }
    }
}
