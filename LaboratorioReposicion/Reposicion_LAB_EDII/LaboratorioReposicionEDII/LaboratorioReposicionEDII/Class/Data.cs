using LaboratorioReposicionEDII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Class
{
    public class Data
    {
        public static List<Movie> ListaMovie = new List<Movie>();
        /// <summary>
        /// Metodo que agrega nuevo nodo a la lista
        /// </summary>
        /// <param name="_Movie">Archivo enviado JSON</param>
        public void Add(Movie _Movie) {
            if (ListaMovie.Count <= 9)
            {
                ListaMovie.Add(_Movie);
            }
            else
            {
                ListaMovie.Reverse();
                ListaMovie.RemoveAt(0);
                ListaMovie.Add(_Movie);
            }
        }
        /// <summary>
        /// Metodo que devuelve la lista por completo
        /// </summary>
        /// <returns>Devuelve la listaMovie</returns>
        public List<Movie> ReturnList() {
            ListaMovie.Reverse();
            return ListaMovie;
        }
	}
}
