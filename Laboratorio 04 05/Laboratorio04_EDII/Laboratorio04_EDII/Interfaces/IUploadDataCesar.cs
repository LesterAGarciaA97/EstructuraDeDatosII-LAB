using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio04_EDII.Interfaces
{
    public class IUploadDataCesar<T>
    {
        IFormFile ArchivoCargado { get; set; }
        T Llave { get; set; }
        string NuevoNombre { get; set; }
    }
}
