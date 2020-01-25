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
    }
}
