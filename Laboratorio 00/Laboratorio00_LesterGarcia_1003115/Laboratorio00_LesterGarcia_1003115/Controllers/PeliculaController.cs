using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Laboratorio00_LesterGarcia_1003115.Models; //Necesario para creación de objetos de modelo específico

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
            int contadorPeliculas = dataPersistence.instanciaNuevaPelicula.listadoPeliculas.Count;
            if (contadorPeliculas > 0)
            {
                if (contadorPeliculas < 10)
                {
                    for (int i = 0; i < contadorPeliculas; i++)
                    {
                        listaPeliculas.Add(dataPersistence.instanciaNuevaPelicula.listadoPeliculas.Pop());
                    }
                    for (int i = contadorPeliculas - 1; i >= 10; i--)
                    {
                        dataPersistence.instanciaNuevaPelicula.listadoPeliculas.Push(listaPeliculas[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        listaPeliculas.Add(dataPersistence.instanciaNuevaPelicula.listadoPeliculas.Pop());
                    }
                    for (int i = 9; i >= 0; i--)
                    {
                        dataPersistence.instanciaNuevaPelicula.listadoPeliculas.Push(listaPeliculas[i]);
                    }
                }
            }
            return listaPeliculas;
        }
        //POST --> /Pelicula --> Ruta
        [HttpPost]
        public Pelicula Obtener([FromBody] Pelicula peliculaIngresada)
        {
            if (peliculaIngresada.id  == 0)
            {
                peliculaIngresada.id = dataPersistence.instanciaNuevaPelicula.listadoPeliculas.Count() + 1;
                dataPersistence.instanciaNuevaPelicula.listadoPeliculas.Push(peliculaIngresada);
            }
            return dataPersistence.instanciaNuevaPelicula.listadoPeliculas.Peek();
        }
    }
}