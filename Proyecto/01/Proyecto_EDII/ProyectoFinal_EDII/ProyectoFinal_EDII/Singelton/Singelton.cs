using ProyectoFinal_EDII.BStarTree;
using ProyectoFinal_EDII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_EDII.Singelton
{
    public class Singelton
    {
        private static Singelton _instance = null;
        public static Singelton instance {
            get {
                if (_instance == null) _instance = new Singelton();
                return _instance;
            }
        }
        public ArbolBStar<Sucursal> ArbolDeSucursales = new ArbolBStar<Sucursal>();

    }
}
