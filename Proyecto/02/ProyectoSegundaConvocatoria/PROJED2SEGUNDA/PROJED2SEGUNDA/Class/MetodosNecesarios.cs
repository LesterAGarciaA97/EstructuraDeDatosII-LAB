using PROJED2SEGUNDA.BtreeStar;
using PROJED2SEGUNDA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PROJED2SEGUNDA.Class
{
    public class MetodosNecesarios
    {
        public ArbolSucursal Sucursales = new ArbolSucursal(9);
        public ArbolProducto Producto = new ArbolProducto(9);
        public ArbolSucursal_Producto Sucursal_Producto = new ArbolSucursal_Producto(9);
        public List<Sucursal> LSucursal = new List<Sucursal>();
        public List<Producto> LProducto = new List<Producto>();
        public List<Sucursal_Producto> LSucursal_Producto = new List<Sucursal_Producto>();
        public string Ruta1 = @"C:\Users\HRZCz\OneDrive\Escritorio\URL-Primer Ciclo 2020\Estructura de datos 2\Laboratorio\ProyectoSegundaConvocatoria_EDII\ProyectoSegundaConvocatoria\PROJED2SEGUNDA\PROJED2SEGUNDA\ManejoInformacion\producto.csv";
        public string Ruta2 = @"C:\Users\HRZCz\OneDrive\Escritorio\URL-Primer Ciclo 2020\Estructura de datos 2\Laboratorio\ProyectoSegundaConvocatoria_EDII\ProyectoSegundaConvocatoria\PROJED2SEGUNDA\PROJED2SEGUNDA\ManejoInformacion\sucursal-producto.csv";
        public string Ruta3 = @"C:\Users\HRZCz\OneDrive\Escritorio\URL-Primer Ciclo 2020\Estructura de datos 2\Laboratorio\ProyectoSegundaConvocatoria_EDII\ProyectoSegundaConvocatoria\PROJED2SEGUNDA\PROJED2SEGUNDA\ManejoInformacion\sucursal.csv";
        /// <summary>
        /// Metodo que ingresa nueva sucursal al arbol sucursales
        /// </summary>
        /// <param name="_NuevaSucursal">Nueva Sucursal a ser agregada</param>
        public void InsertarSucursal(Sucursal _NuevaSucursal) {
            if (_NuevaSucursal.Id >= 0 && _NuevaSucursal.Nombre != null && _NuevaSucursal.Direccion != null)
            {
                string Contenido = string.Empty;
                using (StreamReader Lector = new StreamReader(Ruta3))
                {
                    Contenido = Lector.ReadToEnd();
                }
                string NuevoDato = _NuevaSucursal.Id.ToString() + "," + _NuevaSucursal.Nombre + "," + _NuevaSucursal.Direccion;
                using (StreamWriter Escritor = new StreamWriter(Ruta3))
                {
                    Escritor.WriteLine(Contenido + NuevoDato);
                }
                Sucursales.InsertarSucursal(_NuevaSucursal);
                LSucursal.Add(_NuevaSucursal);
            }
            else
            {
                throw new Exception("El nuevo valor es vacio o se encuentra incompleto");
            }
        }
        /// <summary>
        /// Metodo para agregar un nuevo producto dentro del arbol de productos.
        /// </summary>
        /// <param name="_NuevoProducto">Nuevo producto a ser agregado</param>
        public void InsertarProducto(Producto _NuevoProducto) {//
            if (_NuevoProducto.Id >= 0 && _NuevoProducto.Nombre != null && _NuevoProducto.Precio >= 0)
            {
                string Contenido = string.Empty;
                using (StreamReader Lector = new StreamReader(Ruta1))
                {
                    Contenido = Lector.ReadToEnd();
                }
                string NuevoDato = _NuevoProducto.Id.ToString() + "," + _NuevoProducto.Nombre + "," + _NuevoProducto.Precio.ToString();
                using (StreamWriter Escritor = new StreamWriter(Ruta1))
                {
                    Escritor.WriteLine(Contenido + NuevoDato);
                }
                Producto.InsertarProductor(_NuevoProducto);
                LProducto.Add(_NuevoProducto); 
            }
            else
            {
                throw new Exception("El nuevo valor es vacio o se encuentra incompleto");
            }
        }
        /// <summary>
        /// Metodo para agregar una nueva Sucursal_Producto dentro del arbol de Sucursal_Producto
        /// </summary>
        /// <param name="_NuevaSucursal_Producto">Nueva Sucursal_Producto a ser agregado</param>
        public void InsertarSucursal_Producto(Sucursal_Producto _NuevaSucursal_Producto) {
            if (_NuevaSucursal_Producto.IdSucursal >= 0 && _NuevaSucursal_Producto.IDProducto >= 0 && _NuevaSucursal_Producto.CantidadInvetariado >= 0)
            {
                string Contenido = string.Empty;
                using (StreamReader Lector = new StreamReader(Ruta2))
                {
                    Contenido = Lector.ReadToEnd();
                }
                string NuevoDato = _NuevaSucursal_Producto.IdSucursal + "," + _NuevaSucursal_Producto.IDProducto + "," + _NuevaSucursal_Producto.CantidadInvetariado;
                using (StreamWriter Escritor = new StreamWriter(Ruta2))
                {
                    Escritor.WriteLine(Contenido + NuevoDato);
                }
                Sucursal_Producto.InsertarSucursal_Producto(_NuevaSucursal_Producto);
                LSucursal_Producto.Add(_NuevaSucursal_Producto); 
            }
            else
            {
                throw new Exception("El nuevo valor es vacio o se encuentra incompleto");
            }

        }
        /// <summary>
        /// Lectura de sucursales del .csv
        /// </summary>
        public void LecturaSucursales() {
            using (StreamReader Lector = new StreamReader(Ruta3))
            {
                string NuevaLinea = "";
                while ((NuevaLinea = Lector.ReadLine()) != null)
                {
                    string[] LecturaSucursal = NuevaLinea.Split(',');
                    Sucursal NuevaSucursal = new Sucursal();
                    NuevaSucursal.Id = Convert.ToInt32(LecturaSucursal[0]);
                    NuevaSucursal.Nombre = LecturaSucursal[1];
                    NuevaSucursal.Direccion = LecturaSucursal[2];
                    Sucursales.InsertarSucursal(NuevaSucursal);
                    LSucursal.Add(NuevaSucursal);
                }
            }
        }
        /// <summary>
        /// Lectura de Productos del .csv
        /// </summary>
        public void LecturaProductos() {
            using (StreamReader Lector = new StreamReader(Ruta1))
            {
                string NuevaLinea = "";
                while ((NuevaLinea = Lector.ReadLine()) != null)
                {
                    string[] LecturaProducto = NuevaLinea.Split(',');
                    Producto NuevaProducto = new Producto();
                    NuevaProducto.Id = Convert.ToInt32(LecturaProducto[0]);
                    NuevaProducto.Nombre = LecturaProducto[1];
                    NuevaProducto.Precio = Convert.ToDouble(LecturaProducto[2]);
                    Producto.InsertarProductor(NuevaProducto);
                    LProducto.Add(NuevaProducto);
                }
            }
        }
        /// <summary>
        /// Lectura de la Sucursal_Producto del .csv
        /// </summary>
        public void LecturaSucursal_Producto() {
            using (StreamReader Lector = new StreamReader(Ruta2))
            {
                string NuevaLinea = "";
                while ((NuevaLinea = Lector.ReadLine()) != null)
                {
                    string[] LecturaSucursal_Producto = NuevaLinea.Split(',');
                    Sucursal_Producto NuevaSucursal_Producto = new Sucursal_Producto();
                    NuevaSucursal_Producto.IdSucursal = Convert.ToInt32(LecturaSucursal_Producto[0]);
                    NuevaSucursal_Producto.IDProducto = Convert.ToInt32(LecturaSucursal_Producto[1]);
                    NuevaSucursal_Producto.CantidadInvetariado = Convert.ToInt32(LecturaSucursal_Producto[2]);
                    Sucursal_Producto.InsertarSucursal_Producto(NuevaSucursal_Producto);
                    LSucursal_Producto.Add(NuevaSucursal_Producto);
                }
            }
        }
        /// <summary>
        /// Actualizar los datos de una Sucursal
        /// </summary>
        /// <param name="_SucursalActualizada">Nuevos datos de la sucursal</param>
        public void ActualizarSucursal(Sucursal _SucursalActualizada) {
            string Contenido = string.Empty;
            File.Delete(Ruta3);
            ArbolSucursal NuevoArbolSucursal = new ArbolSucursal(9);
            var Archivo = new FileStream(Ruta3, FileMode.OpenOrCreate);
            Archivo.Close();
            foreach (var item in LSucursal)
            {
                if (item.Id == _SucursalActualizada.Id)
                {
                    item.Nombre = _SucursalActualizada.Nombre;
                    item.Direccion = _SucursalActualizada.Direccion;
                }
                using (StreamReader Lector = new StreamReader(Ruta3))
                {
                    Contenido = Lector.ReadToEnd();
                }
                using (StreamWriter Escritor = new StreamWriter(Ruta3))
                {
                    Escritor.WriteLine(Contenido + item.Id.ToString() + "," + item.Nombre + "," + item.Direccion);
                }
                NuevoArbolSucursal.InsertarSucursal(item);
            }
            Sucursales.NodoRaiz = NuevoArbolSucursal.NodoRaiz;
        }
        /// <summary>
        /// Actualizar los datos de un producto
        /// </summary>
        /// <param name="_ProductoActualizada">Nuevos datos del producto</param>
        public void ActualizarProducto(Producto _ProductoActualizada) {
            string Contenido = string.Empty;
            File.Delete(Ruta1);
            ArbolProducto NuevoArbolProducto = new ArbolProducto(9);
            var Archivo = new FileStream(Ruta1, FileMode.OpenOrCreate);
            Archivo.Close();
            foreach (var item in LProducto)
            {
                if (item.Id == _ProductoActualizada.Id)
                {
                    item.Nombre = _ProductoActualizada.Nombre;
                    item.Precio = _ProductoActualizada.Precio;
                }
                using (StreamReader Lector = new StreamReader(Ruta1))
                {
                    Contenido = Lector.ReadToEnd();
                }
                using (StreamWriter Escrito = new StreamWriter(Ruta1))
                {
                    Escrito.WriteLine(Contenido + item.Id.ToString() + "," + item.Nombre + "," + item.Precio.ToString());
                }
                NuevoArbolProducto.InsertarProductor(item);
            }
            Producto.NodoRaiz = NuevoArbolProducto.NodoRaiz;
        }
        /// <summary>
        /// Actualizar los datos de inventario del Sucursal_Producto
        /// </summary>
        /// <param name="_SucursalProductoActualizada">Nuevos datos de invetario de la Sucursal_Producto</param>
        public void ActualizarSucursal_Producto(Sucursal_Producto _SucursalProductoActualizada) {
            string Contenido = string.Empty;
            File.Delete(Ruta2);
            ArbolSucursal_Producto NuevoArbolSucursal_Producto = new ArbolSucursal_Producto(9);
            var Archivo = new FileStream(Ruta2, FileMode.OpenOrCreate);
            Archivo.Close();
            foreach (var item in LSucursal_Producto)
            {
                if ((item.IdSucursal == _SucursalProductoActualizada.IdSucursal) && (item.IDProducto == _SucursalProductoActualizada.IDProducto))
                {
                    item.CantidadInvetariado = item.CantidadInvetariado + _SucursalProductoActualizada.CantidadInvetariado;
                }
                using (StreamReader Lector = new StreamReader(Ruta2))
                {
                    Contenido = Lector.ReadToEnd();
                }
                using (StreamWriter Escritor = new StreamWriter(Ruta2))
                {
                    Escritor.WriteLine(Contenido + item.IdSucursal.ToString() + "," + item.IDProducto.ToString() + "," + item.CantidadInvetariado.ToString());
                }
                NuevoArbolSucursal_Producto.InsertarSucursal_Producto(item);
            }
            Sucursal_Producto.NodoRaiz = NuevoArbolSucursal_Producto.NodoRaiz;
        }
    }
}
