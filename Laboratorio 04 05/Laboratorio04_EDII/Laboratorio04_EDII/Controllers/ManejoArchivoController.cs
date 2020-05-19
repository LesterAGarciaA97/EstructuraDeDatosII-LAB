using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Laboratorio04_EDII.Cifrados;
using Laboratorio04_EDII.Interfaces;
using Laboratorio04_EDII.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio04_EDII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManejoArchivoController : ControllerBase
    {

        public static IWebHostEnvironment _environment;
        public ManejoArchivoController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpGet("cipher/ZigZag")]
        public string CifrarZigZag([FromForm]UploadDataZigZag CifrarZigZag) {
            if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosCifradosZigZag\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosCifradosZigZag\\");
            }
            ZigZag CifradoZigZag = new ZigZag();
            CifradoZigZag.CifradoZigZag(CifrarZigZag.ArchivoCargado, CifrarZigZag.TamañoCarriles, CifrarZigZag.NuevoNombre, _environment.WebRootPath + "\\ArchivosCifradosZigZag\\");
            return ("El archivo cifrado se encuentra en la carpeta 'ArchivosCifradosZigZag' en la Carpeta 'wwwroot' dentro de la carpeta del Laboratorio");
        }

        [HttpGet("decipher/ZigZag")]
        public string DescifrarZigZag([FromForm]UploadDataZigZag DescifrarZigZag) {
            if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosDescifradosZigZag\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosDescifradosZigZag\\");
            }
            ZigZag DescifradoZigZag = new ZigZag();
            DescifradoZigZag.DescifradoZigZag(DescifrarZigZag.ArchivoCargado, DescifrarZigZag.TamañoCarriles, DescifrarZigZag.NuevoNombre, _environment.WebRootPath + "\\ArchivosDescifradosZigZag\\");
            return ("El archivo descifrado se encuentra en la carpeta 'ArchivosDescifradosZigZag' en la Carpeta 'wwwroot' dentro de la carpeta del Laboratorio");
        }
        /*[HttpGet("DescifradoZigZag")]*/
        [HttpGet("CifradoCesar")]
        public void CifrarCesar([FromForm]UploadDataCesar CifrarCesar)
        {
            if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosCifradosCesar\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosCifradosCesar\\");
            }
            Cesar CifradoCesar = new Cesar();
            CifradoCesar.CifradoCesar(CifrarCesar.ArchivoCargado, CifrarCesar.Llave, CifrarCesar.NuevoNombre, _environment.WebRootPath + "\\ArchivosCifradosCesar\\");
        }
        [HttpGet("DescifradoCesar")]
        public void DescifrarCesar([FromForm]UploadDataCesar DescifrarCesar)
        {
            if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosCifradosCesar\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosCifradosCesar\\");
            }
            Cesar DescifradoCesar = new Cesar();
            DescifradoCesar.CifradoCesar(DescifrarCesar.ArchivoCargado, DescifrarCesar.Llave, DescifrarCesar.NuevoNombre, _environment.WebRootPath + "\\ArchivosDescifradosCesar\\");
        }
    }
}