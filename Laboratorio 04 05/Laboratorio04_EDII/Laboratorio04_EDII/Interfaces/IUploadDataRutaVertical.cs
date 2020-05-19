using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio04_EDII.Interfaces
{
    public class IUploadDataRutaVertical<T>
    {
        IFormFile ArchivoCargado { get; set; }
        T m { get; set; }
        T n { get; set; }
        T matriz { get; set; }
        string NuevoNombre { get; set; }
    }
}
