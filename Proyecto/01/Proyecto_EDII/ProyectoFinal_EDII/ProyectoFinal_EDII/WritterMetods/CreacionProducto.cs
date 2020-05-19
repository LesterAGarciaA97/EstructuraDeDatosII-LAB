using ProyectoFinal_EDII.Interfaces;
using ProyectoFinal_EDII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_EDII.WritterMetods
{
    public class CreacionProducto : ICreateFixedSizeText<Producto>
    {
        public Producto Create(string FixedSizeText)
        {
            Producto _Producto = new Producto();
            _Producto.ID = Convert.ToInt32(FixedSizeText.Substring(0, 10));
            _Producto.Nombre = Convert.ToString(FixedSizeText.Substring(11, 25)).Trim();
            _Producto.Precio = Convert.ToDecimal(FixedSizeText.Substring(37, 10));
            return _Producto;
        }

        public Producto CreateNull()
        {
            Producto _Producto = new Producto();
            _Producto.ID = 0;
            _Producto.Nombre = "";
            _Producto.Precio = 0;
            return _Producto;
        }
    }
}
