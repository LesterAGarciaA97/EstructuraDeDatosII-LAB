using ProyectoFinal_EDII.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_EDII.Models
{
    public class Producto : IComparable , IFixedSizeText
    {
        public int ID { get; set; }

        public string Nombre { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        public int FixedSize { get { return 45; } } 

        public int CompareTo(object obj)
        {
            var _s2 = (Producto)obj;
            return ID.CompareTo(_s2.ID);
        }
        
        public string ToFixedSizeString()
        {
            return $"{ID.ToString("0000000000;-0000000000")}~" + $"{string.Format("{0,-25}", Nombre)}~" + $"{Precio.ToString("0000000000;-0000000000")}";
        }
        public int FixedSizeText {
            get { return FixedSize; }
        }
        public override string ToString()
        {
            return string.Format("ID: {0}\r\nNombre: {1}\r\nPrecio: {2}", ID.ToString("0000000000;-0000000000"), string.Format("{0,-25}", Nombre), Precio.ToString("0000000000;-0000000000"));
        }
    }
}
