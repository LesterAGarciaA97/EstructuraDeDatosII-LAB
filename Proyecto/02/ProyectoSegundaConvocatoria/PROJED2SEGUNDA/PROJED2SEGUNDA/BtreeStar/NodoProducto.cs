using PROJED2SEGUNDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJED2SEGUNDA.BtreeStar
{
    public class NodoProducto
    {
        int GradoMaximo;
        public NodoProducto Padre { get; set; }
        public NodoProducto[] Hijos { get; set; }
        public Producto[] LlavesNodos { get; set; }
        public List<string> LineasDeDatos { get; set; }
        public string LineaDelNodo { get; set; }
        public int IndiceHijoPadre { get; set; }
        public int Tamano { get; set; }
        public int[] LineasHijos { get; set; }
        public bool esNodoHoja { get; set; }
        public bool estaCapacidadMax { get; set; }
        public bool esNodoRaiz { get; set; }
        public NodoProducto(int GradoArbol)
        {
            GradoMaximo = GradoArbol;
            LlavesNodos = new Producto[GradoArbol - 1];
            Hijos = new NodoProducto[GradoArbol];
        }
    }
}
