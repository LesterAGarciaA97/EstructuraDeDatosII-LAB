using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Laboratorio00_LesterGarcia_1003115.Models; //Necesario para creación de objetos

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
            int contadorPeliculas = Data.instanciaPelicula.listadoPeliculas.Count;
            if (contadorPeliculas > 0)
            {
                if (contadorPeliculas < 10)
                {
                    for (int i = 0; i < contadorPeliculas; i++)
                    {
                        listaPeliculas.Add(Data.instanciaPelicula.listadoPeliculas.Pop());
                    }
                    for (int i = contadorPeliculas - 1; i >= 10; i--)
                    {
                        Data.instanciaPelicula.listadoPeliculas.Push(listaPeliculas[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        listaPeliculas.Add(Data.instanciaPelicula.listadoPeliculas.Pop());
                    }
                    for (int i = 9; i >= 0; i--)
                    {
                        Data.instanciaPelicula.listadoPeliculas.Push(listaPeliculas[i]);
                    }
                }
            }
            return listaPeliculas;
        }
        //POST --> /Pelicula --> Ruta
        [HttpPost]
        public Pelicula Obtener([FromBody] Pelicula peliculaIngresada)
        {
            if (peliculaIngresada.numero == 0)
            {
                peliculaIngresada.numero = Data.instanciaPelicula.listadoPeliculas.Count() + 1;
                Data.instanciaPelicula.listadoPeliculas.Push(peliculaIngresada);
            }
            return Data.instanciaPelicula.listadoPeliculas.Peek();
        }
    }
}