using System.Collections.Generic;

namespace Laboratorio00_LesterGarcia_1003115.Models
{
    public class Pelicula
    {
        //Campos los cuales serán requeridos en el método POST
        public int id { get; set; }
        public string nombre { get; set; }
        public int anio { get; set; }
        public string director { get; set; }
    }
    //Persistencia de datos en tiempo de ejecución
    public class dataPersistence
    {
        private static dataPersistence instancia = null;

        public static dataPersistence instanciaNuevaPelicula
        {
            get
            {
                if (instancia == null) instancia = new dataPersistence();
                {
                    return instancia;
                }
            }
        }
        //Como es Stack temporal, se necesita por lo menos un objeto en posición
        private static Pelicula[] listaPeliculas =
        {
            new Pelicula {id = 0, director ="Joe Johnston", nombre = "Hulk", anio = 2003}
        };
        public Stack<Pelicula> listadoPeliculas = new Stack<Pelicula>(listaPeliculas);
    }
}
