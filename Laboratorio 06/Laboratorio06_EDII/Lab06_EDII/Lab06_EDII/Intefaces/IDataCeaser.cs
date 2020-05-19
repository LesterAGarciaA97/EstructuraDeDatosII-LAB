using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab06_EDII.Intefaces
{
    interface IDataCeaser<T>
    {
        public T n { get; set; }
        public IFormFile ArchivoEntrada { get; set; }
    }
}
