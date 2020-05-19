using System.Collections.Generic;
using Lab02_ED2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab02_ED2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase {

        [HttpGet("{datoBuscar?}")]
        public List<Bebida> RetornarDatos(string datoBuscar) {
            if (datoBuscar != null)
            {
                return DataSingelton.Instance.Arbolb.ConvertirALista().FindAll(data => data.Nombre == datoBuscar);
            }
            else
            {
                return DataSingelton.Instance.Arbolb.ConvertirALista();
            }
        }

        [HttpPost]
        public void AgregarDato(Bebida NuevaBebida) {
            DataSingelton.Instance.Arbolb.Insertar(NuevaBebida);
        }

    }
}
