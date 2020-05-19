using LaboratorioReposicionEDII.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Models
{
    public class DataCeaser : IDataCeaser<int>
    {
        public int n { get; set; }
        public IFormFile ArchivoEntrada { get; set; }
        public IFormFile ArchivoLlaves { get; set; }

    }
}
