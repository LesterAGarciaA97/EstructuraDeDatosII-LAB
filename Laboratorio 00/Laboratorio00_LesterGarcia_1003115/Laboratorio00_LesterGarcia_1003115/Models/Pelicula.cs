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
        //Datos quemados de forma predeterminada en el programa
        private static Pelicula[] listaPeliculas =
        {
            new Pelicula {numero = 0, director ="Zack Snyder", nombre = "Batman vs Superman", anio = 2016},
            new Pelicula {numero = 1, director ="Anthony & Joe Russo", nombre = "Avengers: Infinity War", anio = 2018},
            new Pelicula {numero = 2, director ="Anthony & Joe Russo", nombre = "Avengers: Endgame", anio = 2019},
            new Pelicula {numero = 3, director ="Chris Wedge & Carlos Saldanha", nombre = "La era de hielo", anio = 2002},
            new Pelicula {numero = 4, director ="Rob Minkoff & Roger Allers", nombre = "El rey león", anio = 1994},
            new Pelicula {numero = 5, director ="Andrés Muschietti", nombre = "IT", anio = 2017},
            new Pelicula {numero = 6, director ="John Leonetti", nombre = "Annabelle", anio = 2014},
            new Pelicula {numero = 7, director ="Rob Letterman", nombre = "Pokémon: Detective Pikachu", anio = 2019},
            new Pelicula {numero = 8, director ="Guy Ritchie", nombre = "Aladdín", anio = 2019},
            new Pelicula {numero = 9, director ="Cate Shortland", nombre = "Black Widow", anio = 2020}
        };
        public Stack<Pelicula> listadoPeliculas = new Stack<Pelicula>(listaPeliculas);
    }
}
