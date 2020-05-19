using Lab06_EDII.Intefaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab06_EDII.Models
{
    public class DataCeaser : IDataCeaser<int>
    {
        public int n { get; set; }
        public IFormFile ArchivoEntrada { get; set; }
    }
}
