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
        //Se trabaja con stack, para que no de error debe de tener 10 posiciones iniciales
        private static Pelicula[] listaPeliculas =
        {
            new Pelicula {id = 0, director ="Joe Johnston", nombre = "Hulk", anio = 2003},
            new Pelicula {id = 1, director ="Steven Spielberg", nombre = "Jurassic Park", anio = 1993},
            new Pelicula {id = 2, director ="Anthony & Joe Russo", nombre = "Avengers: Endgame", anio = 2019},
            new Pelicula {id = 3, director ="Chris Wedge & Carlos Saldanha", nombre = "La era de hielo", anio = 2002},
            new Pelicula {id = 4, director ="Rob Minkoff & Roger Allers", nombre = "El rey león", anio = 1994},
            new Pelicula {id = 5, director ="Andrés Muschietti", nombre = "IT", anio = 2017},
            new Pelicula {id = 6, director ="John Leonetti", nombre = "Annabelle", anio = 2014},
            new Pelicula {id = 7, director ="Rob Letterman", nombre = "Pokémon: Detective Pikachu", anio = 2019},
            new Pelicula {id = 8, director ="Guy Ritchie", nombre = "Aladdín", anio = 2019},
            new Pelicula {id = 9, director ="Cate Shortland", nombre = "Black Widow", anio = 2020}
        };
        public Stack<Pelicula> listadoPeliculas = new Stack<Pelicula>(listaPeliculas);
    }
}
