using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Interfaces
{
    public class IDataCeaser <T>
    {
        public T n { get; set; }
        public IFormFile ArchivoEntrada { get; set; }
        public IFormFile ArchivoLlaves { get; set; }
    }
}
