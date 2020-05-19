using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Lab03_EDII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        // GET: api/Historial
        [HttpGet]
        public IEnumerable<Compresiones> Get()
        {
            return Data.Instance.historialArchivosComprimidos; //Con esto se retornará el historial de archivos que se han comprimido y sus datos perninentes
        }
    }
}
