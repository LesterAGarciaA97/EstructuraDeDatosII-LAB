using LaboratorioReposicionEDII.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Models.Cifrados
{
    public class DataCipher : IDataCipher<int>
    {
        public IFormFile ArchivoCargado { get; set; }
        public int TamanoCarriles { get; set; }
        public string NuevoNombre { get; set; }
        public string Llave { get; set; }
        public int n { get; set; }
        public int m { get; set; }
    }
}
