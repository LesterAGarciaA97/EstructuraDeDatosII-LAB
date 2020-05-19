
using ProyectoFinal_EDII.Interfaces;
using ProyectoFinal_EDII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_EDII.WritterMetods
{
    public class CreacionSucursal : ICreateFixedSizeText<Sucursal>
    {
        public Sucursal Create(string FixedSizeText)
        {
            Sucursal _Sucursal = new Sucursal();
            _Sucursal.ID = Convert.ToInt32(FixedSizeText.Substring(0, 10));
            _Sucursal.Nombre = Convert.ToString(FixedSizeText.Substring(11, 25));
            _Sucursal.Direccion = Convert.ToString(FixedSizeText.Substring(37, 25));
            return _Sucursal;
        }

        public Sucursal CreateNull()
        {
            Sucursal _Sucursal = new Sucursal();
            _Sucursal.ID = 0;
            _Sucursal.Nombre = "";
            _Sucursal.Direccion = "";
            return _Sucursal;
        }
    }
}
