using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lab06_EDII.Cifrado_Asimetrico;
using Lab06_EDII.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Lab06_EDII.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class CipherController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public CipherController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("ceaser2")]
        public void CreateKeys([FromForm] DataCeaser _DataCeaser )
        {
            if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosCifradosCeaser\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosCifradosCeaser\\");
            }
            Ceaser _Ceaser = new Ceaser();
            _Ceaser.CifradoCeaser(_DataCeaser.ArchivoEntrada, _DataCeaser.n, _environment.WebRootPath + "\\ArchivosCifradosCeaser\\");
            //Proceso de cifrado
            
        }

        [HttpPost("getPublicKey")]
        public void GetKeysCreator([FromForm]DataRequired _dataRequired)
            {
            if (!Directory.Exists(_environment.WebRootPath + "\\CipherKeys\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\CipherKeys\\");
            }
            Diffie_Hellman diffie_Hellman = new Diffie_Hellman();
            diffie_Hellman.CreateKeys(_dataRequired.ValueAB, _dataRequired.Rnd_ab, _environment.WebRootPath + "\\CipherKeys\\");
            RSA _RSA = new RSA();
            _RSA.GenerarLlaves(_dataRequired.p, _dataRequired.q, _environment.WebRootPath + "\\CipherKeys\\");
            //Proceso de creacion de llaves

        }
    }
}