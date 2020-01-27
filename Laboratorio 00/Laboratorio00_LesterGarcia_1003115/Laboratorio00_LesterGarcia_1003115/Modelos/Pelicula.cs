using System.Collections.Generic;

namespace Laboratorio00_LesterGarcia_1003115.Modelos
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
        //Se inicaliza una lista con un solo dato en ella para luego ser ingresada en una pila
        private static Pelicula[] listaPeliculas =
        {
            new Pelicula {id = 1, director ="Joe Johnston", nombre = "Hulk", anio = 2003},
            new Pelicula {id = 2, director = "Cristopher Nolan", nombre = "Batman", anio = 2010 }

        };
        //Como es una pila temporal, se necesita por lo menos un objeto en posición para evitar error de "Empty Stack"
        public Stack<Pelicula> listadoPeliculas = new Stack<Pelicula>(listaPeliculas);
    }
}
