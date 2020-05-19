using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Models.LZW
{
    public class DataLzw
    {
        public IFormFile ArchivoEntrada { get; set; }
        public string NuevoNombre { get; set; }
    }
}
