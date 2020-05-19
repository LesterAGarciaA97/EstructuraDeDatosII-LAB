using ProyectoFinal_EDII.Interfaces;
using ProyectoFinal_EDII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_EDII.WritterMetods
{
    public class CreacionSucursal_Producto : ICreateFixedSizeText<Sucursal_Producto>
    {
        public Sucursal_Producto Create(string FixedSizeText)
        {
            Sucursal_Producto _Sucursal_Producto = new Sucursal_Producto();
            _Sucursal_Producto.IDSucursal = Convert.ToInt32(FixedSizeText.Substring(0, 10));
            _Sucursal_Producto.IDProducto = Convert.ToInt32(FixedSizeText.Substring(11, 10));
            _Sucursal_Producto.InventarioDisponible = Convert.ToInt32(FixedSizeText.Substring(22, 10));
            return _Sucursal_Producto;
        }

        public Sucursal_Producto CreateNull()
        {
            Sucursal_Producto _Sucursal_Producto = new Sucursal_Producto();
            _Sucursal_Producto.IDSucursal = 0;
            _Sucursal_Producto.IDProducto = 0;
            _Sucursal_Producto.InventarioDisponible = 0;
            return _Sucursal_Producto;
        }
    }
}
