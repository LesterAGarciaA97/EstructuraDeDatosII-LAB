using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Models.Huffman
{
    public class DataHuffman
    {
        public IFormFile ArchivoComprimir { get; set; }
        public string NuevoNombre { get; set; }
    }
}
