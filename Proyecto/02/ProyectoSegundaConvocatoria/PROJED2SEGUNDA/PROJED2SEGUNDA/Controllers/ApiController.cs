using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROJED2SEGUNDA.Cipher;
using PROJED2SEGUNDA.Class;
using PROJED2SEGUNDA.HuffmanCompression;
using PROJED2SEGUNDA.Models;

namespace PROJED2SEGUNDA.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        public static MetodosNecesarios MetodosNecesarios = new MetodosNecesarios();

        public static IWebHostEnvironment _environment;
        public ApiController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        /// <summary>
        /// Metodo donde se cargan los achivos .csv al inicio
        /// </summary>
        /// <returns>Mensaje de lectura exitosa</returns>
        [HttpGet]
        public string Get() {
            MetodosNecesarios.LecturaSucursal_Producto();
            MetodosNecesarios.LecturaProductos();
            MetodosNecesarios.LecturaSucursales();
            return "Se ha leido correctamente los .CSV";
        }

        /// <summary>
        /// Metodo para agregar nueva sucursal 
        /// </summary>
        /// <param name="_NSucursal"></param>
        [HttpPost("Agregar/Sucursal")]
        public string AgregarSucursal([FromForm]Sucursal _NSucursal)
        {
            if (_NSucursal.Id >= 0 && _NSucursal.Nombre != null && _NSucursal.Direccion != null)
            {
                MetodosNecesarios.InsertarSucursal(_NSucursal);
                return "Se a agregado una nueva sucursal";
            }
            else
            {
                return ("Nueva Sucursal Vacio");
            }
        }
        /// <summary>
        /// Metodo para agregar nuevo producto
        /// </summary>
        /// <param name="_NProducto">Nuevo Producto</param>
        [HttpPost("Agregar/Producto")]
        public string AgregarProducto([FromForm]Producto _NProducto) {
            if (_NProducto.Id >= 0 && _NProducto.Nombre != null && _NProducto.Precio >= 0) 
            {
                MetodosNecesarios.InsertarProducto(_NProducto);
                return "Se a agregado un nuevo Producto";
            }
            else
            {
                return ("Nuevo Producto Vacio");
            }
        }
        /// <summary>
        /// Metodo para agregar nueva Sucursal_Producto
        /// </summary>
        /// <param name="_NSucursalProducto"></param>
        [HttpPost("Agregar/SucursalProducto")]
        public string AgregarSucursalProducto([FromForm]Sucursal_Producto _NSucursalProducto) {
            if (_NSucursalProducto.IdSucursal >= 0 && _NSucursalProducto.IDProducto >= 0 && _NSucursalProducto.CantidadInvetariado >= 0)
            {
                var ExistenciaSucursal = MetodosNecesarios.Sucursales.BusquedaNodo(_NSucursalProducto.IdSucursal);
                var ExistenciaProducto = MetodosNecesarios.Producto.BusquedaNodo(_NSucursalProducto.IDProducto);
                if ((ExistenciaSucursal == true) && (ExistenciaProducto == true))
                {
                    MetodosNecesarios.InsertarSucursal_Producto(_NSucursalProducto);
                    return "Se ha agregado Correctamente la nueva Sucursal_Producto";
                }
                return "Se ha agregado nuevo Sucursal_Producto";
            }
            else
            {
                return ("Nuevo Sucursal_Producto Vacio o No existe (Sucursal o Producto)");
            }
        }
        /// <summary>
        /// Metodo que realiza la actualizacion de alguna sucursal en especifico
        /// </summary>
        /// <param name="_SucursalActualizada"></param>
        /// <returns>Mensaje de exito o No Exito con su razon</returns>
        [HttpPost("Actualizar/Sucursal")]
        public string ActualizarSucursal([FromForm] Sucursal _SucursalActualizada) {
            if (_SucursalActualizada.Id >= 0 && _SucursalActualizada.Nombre != null && _SucursalActualizada.Direccion != null)
            {
                var ExistenciaSucursal = MetodosNecesarios.Sucursales.BusquedaNodo(_SucursalActualizada.Id);
                if (ExistenciaSucursal == true)
                {
                    MetodosNecesarios.ActualizarSucursal(_SucursalActualizada);
                    return "La Sucursal: (" + _SucursalActualizada.Nombre + ") ha sido Actualizada";
                }
                else
                {
                    return "La sucursal que se requiere actualizar no existe en el recuento de sucursales";
                } 
            }
            else
            {
                return "Se ha enviado una modificacion de sucursal erronea";
            }
        
        }
        /// <summary>
        /// Metodo que realiza la actualizacion de algun producto en especifico
        /// </summary>
        /// <param name="_ProductoActualizado"></param>
        /// <returns>Mensaje de exito o No Exito con su razon</returns>
        [HttpPost("Actualizar/Producto")]
        public string ActualizarProducto([FromForm]Producto _ProductoActualizado) {
            if (_ProductoActualizado.Id >= 0 && _ProductoActualizado.Nombre != null && _ProductoActualizado.Precio >= 0)
            {
                var ExistenciaProducto = MetodosNecesarios.Producto.BusquedaNodo(_ProductoActualizado.Id);
                if (ExistenciaProducto == true)
                {
                    MetodosNecesarios.ActualizarProducto(_ProductoActualizado);
                    return "El producto: (" + _ProductoActualizado.Nombre + ") fue actualizado";
                }
                else
                {
                    return "El producto que se requiere actualizar no existe en el recuento de productos";
                } 
            }
            else
            {
                return "Se ha enviado una modificacion de producto erronea";
            }
        }
        /// <summary>
        /// Metodo que realiza la actualizacion de Sucursal_Producto en especifico
        /// </summary>
        /// <param name="_SucursalProductoActualizada"></param>
        /// <returns></returns>
        [HttpPost("Actualizar/SucursalProducto")]
        public string ActualizarSucursalProducto([FromForm] Sucursal_Producto _SucursalProductoActualizada) {
            if (_SucursalProductoActualizada.IdSucursal >= 0 && _SucursalProductoActualizada.IDProducto >= 0 && _SucursalProductoActualizada.CantidadInvetariado >= 0)
            {
                var ExistenciaSucursalProducto = MetodosNecesarios.Sucursal_Producto.BusquedaNodo(_SucursalProductoActualizada.IdSucursal, _SucursalProductoActualizada.IDProducto);
                if (ExistenciaSucursalProducto == true)
                {
                    MetodosNecesarios.ActualizarSucursal_Producto(_SucursalProductoActualizada);
                    return "El Id_Sucursal: (" + _SucursalProductoActualizada.IdSucursal + ") y el Id_Producto (" + _SucursalProductoActualizada.IDProducto + ") Ha sido actualizada en su inventario";
                }
                else
                {
                    return "La Sucursal_Producto que se requiere actualizar no existe en el recuento de Sucursal_Producto";
                } 
            }
            else
            {
                return "Se ha enviado una modificacion de Sucursal_Producto erronea";
            }
        }
        /// <summary>
        /// Metodo que retorna todos los datos de productos
        /// </summary>
        /// <returns>Lista de productos</returns>
        [HttpGet("Mostrar/Productos")]
        public string MostrarProductos() {
            var TextoEncabezado = "--Lista de Productos--\n";
            var ListadoProductos = MetodosNecesarios.Producto.DevolverTodo();
            foreach (var Valor in ListadoProductos)
            {
                if (Valor != null)
                {
                    TextoEncabezado += "ID: " + Valor.Id.ToString() + "\n";
                    TextoEncabezado += "NombreProducto: " + Valor.Nombre.ToString() + "\n";
                    TextoEncabezado += "Precio Q: " + Valor.Precio.ToString() + "\n";
                }
            }
            return TextoEncabezado;
        }
        /// <summary>
        /// Metodo que retorna todos los datos de las sucursales 
        /// </summary>
        /// <returns>Lista de sucursales</returns>
        [HttpGet("Mostrar/Sucursales")]
        public string MostrarSucursales() {
            var TextoEncabezado = "--Lista de Sucursales--\n";
            var ListadoSucursales = MetodosNecesarios.Sucursales.DevolverTodo();
            foreach (var Valor in ListadoSucursales)
            {
                if (Valor != null)
                {
                    TextoEncabezado += "ID: " + Valor.Id.ToString() + "\n";
                    TextoEncabezado += "Nombre_Sucursal: " + Valor.Nombre.ToString() + "\n";
                    TextoEncabezado += "Direccion: " + Valor.Direccion.ToString() + "\n";
                }
            }
            return TextoEncabezado;
        }
        /// <summary>
        /// Metodos que retorna todos los datos de las Sucursales_Productos
        /// </summary>
        /// <returns>Lista de Sucursales_Productos</returns>
        [HttpGet("Mostrar/SucursalProducto")]
        public string MostrarSucursalProducto() {
            var TextoEncabezado = "--Lista de SucursalProducto--\n";
            var ListadoSucursalProducto = MetodosNecesarios.Sucursal_Producto.DevolverTodo();
            foreach (var Valor in ListadoSucursalProducto)
            {
                if (Valor != null)
                {
                    TextoEncabezado += "IDSucursal: " + Valor.IdSucursal.ToString() + "\n";
                    TextoEncabezado += "IDProducto: " + Valor.IDProducto.ToString() + "\n";
                    TextoEncabezado += "Cantidad Inventario: " + Valor.CantidadInvetariado.ToString() + "\n";
                }
            }
            return TextoEncabezado;
        }
        /// <summary>
        /// Metodo que retorna un archivo descargable con los archivos cifrados listos para ser enviados a otro destino
        /// </summary>
        /// <param name="clave"></param>
        /// <returns>retornara un archivo .zip que contiene los archivos .huff resultantes de la compresion de huffman</returns>
        [HttpPost("TransporteDeInformacion")]
        public FileContentResult TransporteDeDatos([FromForm]int clave)
        {
            try
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosCompresos\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosCompresos\\");
                }
                if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosCifrados\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosCifrados\\");
                }

                var rutaOriginal1 = Path.GetFullPath(@"ManejoInformacion\" + "producto.csv");
                var rutaOriginal2 = Path.GetFullPath(@"ManejoInformacion\" + "sucursal.csv");
                var rutaOriginal3 = Path.GetFullPath(@"ManejoInformacion\" + "sucursal-producto.csv");
                ////Primer Archivo Producto
                using (var stream = new FileStream(@"C:\Users\HRZCz\OneDrive\Escritorio\URL-Primer Ciclo 2020\Estructura de datos 2\Laboratorio\ProyectoSegundaConvocatoria_EDII\ProyectoSegundaConvocatoria\PROJED2SEGUNDA\PROJED2SEGUNDA\ManejoInformacion\Llaves\Llave.txt", FileMode.OpenOrCreate))
                {
                    using (var reader = new StreamWriter(stream))
                    {
                        reader.Write(clave);
                    }
                }
                SDES modeloSdes1 = new SDES();
                var OpsDic = modeloSdes1.LeerOperaciones(@"C:\Users\HRZCz\OneDrive\Escritorio\URL-Primer Ciclo 2020\Estructura de datos 2\Laboratorio\ProyectoSegundaConvocatoria_EDII\ProyectoSegundaConvocatoria\PROJED2SEGUNDA\PROJED2SEGUNDA\ManejoInformacion\Llaves\Oper.txt");
                var binaryKey = string.Empty;
                modeloSdes1.VerificarLLave(clave.ToString(), ref binaryKey);
                var key1 = string.Empty;
                var key2 = string.Empty;
                var sbox0 = modeloSdes1.CrearSBox0();
                var sbox1 = modeloSdes1.CrearSBox1();
                modeloSdes1.GenerarLlaves(OpsDic, binaryKey, ref key1, ref key2);
                var bytesCifrados = modeloSdes1.CifrarTexto(rutaOriginal1, OpsDic, key1, key2, sbox0, sbox1);
                key1 = string.Empty;
                key2 = string.Empty;
                modeloSdes1.GenerarLlaves(OpsDic, binaryKey, ref key1, ref key2);
                var bytesDecifrados = modeloSdes1.DescifrarTexto(bytesCifrados, OpsDic, key1, key2, sbox0, sbox1);
                var vec1 = rutaOriginal1.Split(@"\");
                var vec2 = vec1[vec1.Length - 1].Split(".");
                var pathHuffman = _environment.WebRootPath + "\\ArchivosCifrados\\";
                using (var stream = new FileStream(pathHuffman + vec1[vec1.Length - 1], FileMode.OpenOrCreate))
                {
                    using (var reader = new BinaryWriter(stream))
                    {
                        foreach (var item in bytesDecifrados)
                        {
                            reader.Write(item);
                        }
                    }
                }
                var pathHuffman2 = _environment.WebRootPath + "\\ArchivosCompresos\\";
                Huffman.Instancia.CompresiónHuffman(pathHuffman + vec1[vec1.Length - 1], vec2, pathHuffman2);


                ////Segundo Archivo Sucursal
                SDES modeloSdes2 = new SDES();
                key1 = string.Empty;
                key2 = string.Empty;
                modeloSdes2.GenerarLlaves(OpsDic, binaryKey, ref key1, ref key2);
                var bytesCifrados_2 = modeloSdes2.CifrarTexto(rutaOriginal2, OpsDic, key1, key2, sbox0, sbox1);
                key1 = string.Empty;
                key2 = string.Empty;
                modeloSdes2.GenerarLlaves(OpsDic, binaryKey, ref key1, ref key2);
                var bytesDecifrados2 = modeloSdes2.DescifrarTexto(bytesCifrados_2, OpsDic, key1, key2, sbox0, sbox1);
                var vec1_2 = rutaOriginal2.Split(@"\");
                var vec2_2 = vec1_2[vec1_2.Length - 1].Split(".");
                var pathHuffman_2 = _environment.WebRootPath + "\\ArchivosCifrados\\";
                using (var stream = new FileStream(pathHuffman_2 + vec1_2[vec1_2.Length - 1], FileMode.OpenOrCreate))
                {
                    using (var reader = new BinaryWriter(stream))
                    {
                        foreach (var item in bytesDecifrados2)
                        {
                            reader.Write(item);
                        }
                    }
                }
                var pathHuffman2_2 = _environment.WebRootPath + "\\ArchivosCompresos\\";
                Huffman.Instancia.CompresiónHuffman(pathHuffman_2 + vec1_2[vec1_2.Length - 1], vec2_2, pathHuffman2_2);


                ////Tercer Archivo Sucursal-Producto
                SDES modeloSdes3 = new SDES();
                key1 = string.Empty;
                key2 = string.Empty;
                modeloSdes3.GenerarLlaves(OpsDic, binaryKey, ref key1, ref key2);
                var bytesCifrados_3 = modeloSdes3.CifrarTexto(rutaOriginal3, OpsDic, key1, key2, sbox0, sbox1);
                key1 = string.Empty;
                key2 = string.Empty;
                modeloSdes3.GenerarLlaves(OpsDic, binaryKey, ref key1, ref key2);
                var bytesDecifrados3 = modeloSdes3.DescifrarTexto(bytesCifrados_3, OpsDic, key1, key2, sbox0, sbox1);
                var vec1_3 = rutaOriginal3.Split(@"\");
                var vec2_3 = vec1_3[vec1_3.Length - 1].Split(".");
                var pathHuffman_3 = _environment.WebRootPath + "\\ArchivosCifrados\\";
                using (var stream = new FileStream(pathHuffman_3 + vec1_3[vec1_3.Length - 1], FileMode.OpenOrCreate))
                {
                    using (var reader = new BinaryWriter(stream))
                    {
                        foreach (var item in bytesDecifrados3)
                        {
                            reader.Write(item);
                        }
                    }
                }
                var pathHuffman2_3 = _environment.WebRootPath + "\\ArchivosCompresos\\";
                Huffman.Instancia.CompresiónHuffman(pathHuffman_3 + vec1_3[vec1_3.Length - 1], vec2_3, pathHuffman2_3);


                ////Manejo de archivo ZIP. resultante con las compresiones de los metodos
                string ruta1 = pathHuffman2 + "producto.huff";
                string ruta2 = pathHuffman2_2 + "sucursal.huff";
                string ruta3 = pathHuffman2_3 + "sucursal-producto.huff";
                string cadena1 = string.Empty;
                string cadena2 = string.Empty;
                string cadena3 = string.Empty;
                using (StreamReader sr = new StreamReader(ruta1))
                {
                    cadena1 = sr.ReadToEnd();
                }
                using (StreamReader sr = new StreamReader(ruta2))
                {
                    cadena2 = sr.ReadToEnd();
                }
                using (StreamReader sr = new StreamReader(ruta3))
                {
                    cadena3 = sr.ReadToEnd();
                }
                byte[] file1 = Encoding.ASCII.GetBytes(cadena1);
                byte[] file2 = Encoding.ASCII.GetBytes(cadena2);
                byte[] file3 = Encoding.ASCII.GetBytes(cadena3);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (var Archivo = new ZipArchive(ms, ZipArchiveMode.Create, true))
                    {
                        var Zip1 = Archivo.CreateEntry("producto.huff", CompressionLevel.Fastest);
                        using (var zipStream1 = Zip1.Open()) zipStream1.Write(file1, 0, file1.Length);
                        var Zip2 = Archivo.CreateEntry("sucursal.huff", CompressionLevel.Fastest);
                        using (var zipStream2 = Zip2.Open()) zipStream2.Write(file2, 0, file2.Length);
                        var Zip3 = Archivo.CreateEntry("sucursal-producto.huff", CompressionLevel.Fastest);
                        using (var zipStream3 = Zip3.Open()) zipStream3.Write(file3, 0, file3.Length);
                    }

                    return File(ms.ToArray(), "Application/zip", "ArchivoFinal.zip");
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Clave vacia o erronea", ex);
            }
            
            
        }
    }
}