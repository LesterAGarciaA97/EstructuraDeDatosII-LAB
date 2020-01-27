using System.Collections.Generic;

namespace Laboratorio00_LesterGarcia_1003115.Models
{
    public class Pelicula
    {
        //Campos los cuales serán requeridos en el método POST
        public int numero { get; set; }
        public string nombre { get; set; }
        public int anio { get; set; }
        public string director { get; set; }
    }
    //Persistencia de datos en tiempo de ejecución
    public class Data
    {
        private static Data instancia = null;

        public static Data instanciaPelicula
        {
            get
            {
                if (instancia == null) instancia = new Data();
                {
                    return instancia;
                }
            }
        }
        //La lista al iniciar el progarma está vacía, irá reciendo los datos conforme el usuario los ingrese
        private static Pelicula[] listaPeliculas =
        {
        };
        public Stack<Pelicula> listadoPeliculas = new Stack<Pelicula>(listaPeliculas);
    }
}
