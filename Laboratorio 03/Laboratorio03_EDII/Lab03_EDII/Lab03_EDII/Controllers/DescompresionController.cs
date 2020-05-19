using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Lab03_EDII.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DescompresionController : ControllerBase
    {
        // GET: api/Descompresion
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Descompresion/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Descompresion/archivo        
        [HttpPost("{name}")]
        public void Post([FromForm(Name = "archivo")] IFormFile file, string name)
        {
            Descompresion descomprimiendo = new Descompresion();
            string ruta = @"C:\CompresionesHechas\";
            string fullPath = ruta + file.FileName;

            byte[] archivo = new byte[file.Length];
            using (FileStream fs = new FileStream(fullPath, FileMode.Open))
            {
                int count;                           
                int sum = 0;                         

                while ((count = fs.Read(archivo, sum, archivo.Length - sum)) > 0)
                    sum += count;
            }
        }
    }
}
