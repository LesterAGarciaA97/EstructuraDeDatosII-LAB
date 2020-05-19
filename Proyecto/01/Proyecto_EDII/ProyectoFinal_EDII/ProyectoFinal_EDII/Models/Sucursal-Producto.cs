using ProyectoFinal_EDII.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_EDII.Models
{
    public class Sucursal_Producto : IComparable , IFixedSizeText
    {

        public int IDSucursal { get; set; }
        public int IDProducto { get; set; }
        public int InventarioDisponible { get; set; }

        public int FixedSize { get { return 30; } }

        public int CompareTo(object obj)
        {
            var _s2 = (Sucursal_Producto)obj;
            return IDSucursal.CompareTo(_s2.IDSucursal);
        }

        public string ToFixedSizeString()
        {
            return $"{IDSucursal.ToString("0000000000;-0000000000")}~" + $"{IDProducto.ToString("0000000000;-0000000000")}~" + $"{InventarioDisponible.ToString("0000000000;-0000000000")}";
        }
        public int FixedSizeText {
            get { return FixedSize; }
        }

        public override string ToString()
        {
            return string.Format("IDSucursal: {0}\r\nIDProducto: {1}\r\nInventarioDisponible: {2}", IDSucursal.ToString("0000000000;-0000000000"), IDProducto.ToString("0000000000;-0000000000"), InventarioDisponible.ToString("0000000000;-0000000000"));
        }
    }
}
