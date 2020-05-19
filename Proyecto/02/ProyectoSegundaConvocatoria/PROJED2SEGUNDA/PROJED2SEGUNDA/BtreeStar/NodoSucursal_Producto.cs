using PROJED2SEGUNDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJED2SEGUNDA.BtreeStar
{
    public class NodoSucursal_Producto
    {
        int GradoMaximo;
        public NodoSucursal_Producto Padre { get; set; }
        public NodoSucursal_Producto[] Hijos { get; set; }
        public Sucursal_Producto[] LlavesNodos { get; set; }
        public List<string> LineasDeDatos { get; set; }
        public string LineaDelNodo { get; set; }
        public int IndiceHijoPadre { get; set; }
        public int Tamano { get; set; }
        public int[] LineasHijos { get; set; }
        public bool esNodoHoja { get; set; }
        public bool estaCapacidadMax { get; set; }
        public bool esNodoRaiz { get; set; }
        public NodoSucursal_Producto(int GradoArbol)
        {
            GradoMaximo = GradoArbol;
            LlavesNodos = new Sucursal_Producto[GradoArbol - 1];
            Hijos = new NodoSucursal_Producto[GradoArbol];
        }
    }
}
