using Laboratorio04_EDII.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio04_EDII.Models
{
    public class UploadDataZigZag : IUploadDataZigZag<int>
    {
        public IFormFile ArchivoCargado { get; set; }
        public int TamañoCarriles { get; set; }
        public string NuevoNombre { get; set; }
    }
}
