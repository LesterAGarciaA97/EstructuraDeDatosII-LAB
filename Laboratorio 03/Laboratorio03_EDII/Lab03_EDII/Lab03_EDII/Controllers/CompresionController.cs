using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab03_EDII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompresionController : ControllerBase
    {
        // GET: api/Compresion
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Compresion
        [HttpPost("{name}")]
        public void Post([FromForm(Name = "file")] IFormFile file, string name)
        {
            var resultado = new StringBuilder();
            Compresion comprimiendo = new Compresion();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    resultado.AppendLine(reader.ReadLine());
            }
            byte[] textoEnBytes = Encoding.UTF8.GetBytes(resultado.ToString());
        }
    }
}
