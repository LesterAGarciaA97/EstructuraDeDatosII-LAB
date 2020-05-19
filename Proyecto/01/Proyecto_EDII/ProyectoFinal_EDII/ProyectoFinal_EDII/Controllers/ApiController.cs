using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_EDII.Models;

namespace ProyectoFinal_EDII.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public ApiController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpPost("AgregarSucursal")]
        public void AgregarSucursal([FromForm] Sucursal _Sucursal) {
            if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosCifradosCeaser\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosCifradosCeaser\\");
            }
            string Ruta = _environment.WebRootPath + "\\ArchivosCifradosCeaser\\";


           // Singelton.Singelton.instance.ArbolDeSucursales.Add();           
        }
    }
}