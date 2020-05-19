using PROJED2SEGUNDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJED2SEGUNDA.BtreeStar
{
    public class NodoSucursal
    {
        int GradoMaximo;
        public NodoSucursal Padre { get; set; }
        public NodoSucursal[] Hijos { get; set; }
        public Sucursal[] LlavesNodos { get; set; }
        public List<string> LineasDeDatos { get; set; }
        public string LineaDelNodo { get; set; }
        public int IndiceHijoPadre { get; set; }
        public int Tamano { get; set; }
        public int[] LineasHijos { get; set; }
        public bool esNodoHoja { get; set; }
        public bool estaCapacidadMax { get; set; }
        public bool esNodoRaiz { get; set; }
        public NodoSucursal(int GradoArbol)
        {
            GradoMaximo = GradoArbol;
            LlavesNodos = new Sucursal[GradoArbol - 1];
            Hijos = new NodoSucursal[GradoArbol];
        }
    }
}
