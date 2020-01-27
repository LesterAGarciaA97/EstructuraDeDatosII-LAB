using System.Collections.Generic;

namespace Laboratorio00_LesterGarcia_1003115.Models
{
    public class Pelicula
    {
        //Campos requeridos para el método POST
        public int id { get; set; }
        public string nombre { get; set; }
        public int anio { get; set; }
        public string director { get; set; }
    }
    //Persistencia de datos
    public class persistenciaDatos
    {
        private static persistenciaDatos instancia = null;
        public static persistenciaDatos instanciaNuevaPelicula
        {
            get
            {
                if (instancia == null) instancia = new persistenciaDatos();
                {
                    return instancia;
                }
            }
        }
        //Se inicaliza con un solo dato en ella para luego ser ingresada en una pila
        private static Pelicula[] arregloPeliculas =
        {
            new Pelicula {id = 0, director ="Joe Johnston", nombre = "Hulk", anio = 2003}

        };
        //Como es una pila temporal, se necesita por lo menos un objeto en posición para evitar error de "Empty Stack"
        public Stack<Pelicula> pilaPeliculas = new Stack<Pelicula>(arregloPeliculas);
    }
}
