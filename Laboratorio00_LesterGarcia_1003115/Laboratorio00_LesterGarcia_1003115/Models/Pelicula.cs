using System.Collections.Generic;

namespace Laboratorio00_LesterGarcia_1003115.Models
{
    public class Pelicula
    {
        //Campos los cuales ser+an requeridos en el método POST
        public int numero { get; set; }
        public string nombre { get; set; }
        public int anio { get; set; }
        public string director { get; set; }


        //---------------------------------------------------------------------------------------------
        public string name;
        public int value;
        public bool isChecked;
        public List<int> numbers = new List<int>();
        public List<Pelicula> weatherForecasts = new List<Pelicula>();

        private static Pelicula instancia = null;
        public static Pelicula Instance
        {
            get
            {
                if (instancia == null) instancia = new Pelicula();
                return instancia;
            }
        }
    }
}
