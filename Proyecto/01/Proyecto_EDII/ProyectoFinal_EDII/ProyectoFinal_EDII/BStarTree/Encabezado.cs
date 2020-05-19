using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_EDII.BStarTree
{
    public class Encabezado
    {
        public int Raiz { get; set; }
        public int SiguientePosicion { get; set; }
        public int Order { get; set; }

        public static int tamanoAjustado { get { return 34; } }

        public string ParaAjusteTamanoCadena() {
            return $"{Raiz.ToString("0000000000;-000000000")}" + MetodosNecesarios.Separador.ToString() + $"{Order.ToString("0000000000;-000000000")}" + MetodosNecesarios.Separador.ToString() + $"{SiguientePosicion.ToString("0000000000;-000000000")}\r\n";
        }
        public int AjusteTamanoCadena {
            get { return tamanoAjustado; }
        }
    }
}
