using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Laboratorio00_LesterGarcia_1003115.Models;

namespace Laboratorio00_LesterGarcia_1003115.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        //GET --> /Pelicula --> Ruta
        [HttpGet]
        public List<Pelicula> Mostrar()
        {
            List<Pelicula> listaPeliculas = new List<Pelicula>();
            int contadorPeliculas = persistenciaDatos.instanciaNuevaPelicula.pilaPeliculas.Count;
            if (contadorPeliculas > 0)
            {
                if (contadorPeliculas < 10)
                {
                    for (int i = 0; i < contadorPeliculas; i++)
                    {
                        listaPeliculas.Add(persistenciaDatos.instanciaNuevaPelicula.pilaPeliculas.Pop());
                    }
                    for (int i = contadorPeliculas - 1; i >= 10; i--)
                    {
                        persistenciaDatos.instanciaNuevaPelicula.pilaPeliculas.Push(listaPeliculas[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        listaPeliculas.Add(persistenciaDatos.instanciaNuevaPelicula.pilaPeliculas.Pop());
                    }
                    for (int i = 9; i >= 0; i--)
                    {
                        persistenciaDatos.instanciaNuevaPelicula.pilaPeliculas.Push(listaPeliculas[i]);
                    }
                }
            }
            return listaPeliculas;
        }
        //POST --> /Pelicula --> Ruta
        [HttpPost]
        public Pelicula Obtener([FromBody] Pelicula peliculaIngresada)
        {
            if (peliculaIngresada.id == 0)
            {
                peliculaIngresada.id = persistenciaDatos.instanciaNuevaPelicula.pilaPeliculas.Count() + 1;
                persistenciaDatos.instanciaNuevaPelicula.pilaPeliculas.Push(peliculaIngresada);
            }
            return persistenciaDatos.instanciaNuevaPelicula.pilaPeliculas.Peek();
        }
    }
}