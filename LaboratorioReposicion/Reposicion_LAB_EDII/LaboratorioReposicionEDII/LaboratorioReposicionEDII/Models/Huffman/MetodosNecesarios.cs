using LaboratorioReposicionEDII.Class.Huffman;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Models.Huffman
{
    public class MetodosNecesarios
    {
        private static string NombreArchivoAComprimir;
        private static string RutaAEscribir;
        private static string RutaContenedora;
        private static string NombreArchivoADescomprimir;
        private static double tamanoDescompreso;
        private static double tamanoCompreso;
        public DateTime time = DateTime.Now;
        private string EspaciosInnecesarios(IFormFile Archivo) {
            string temp = string.Empty;
            string TextoResultante = string.Empty;
            //byte[] fileData = null;
            char[] Removedor = null;
            string[] Receptor = null;
            using (var Lectura = new StreamReader(Archivo.OpenReadStream()))
            {
                var CapturaArchivo = new StringBuilder();
                while (Lectura.Peek() >= 0)
                {
                    CapturaArchivo.AppendLine(Lectura.ReadLine());
                }
                temp = CapturaArchivo.ToString();
                /* Removedor = temp.ToCharArray();
                 Receptor = new string[Removedor.Length - 2];
                 for (int i = 0; i < Removedor.Length - 2; i++)
                 {
                     Receptor[i] = Removedor[i].ToString();
                 }
                 temp = String.Concat(Receptor);*/
                if (temp.Contains("\r\n"))
                {
                    TextoResultante = temp.Replace("\r\n", "\n");
                }
                Lectura.Close();
            }
            return TextoResultante;
        }

        /// <summary>
        /// Metodo para la compresion de huffman Correctamente
        /// </summary>
        /// <param name="ArchivoEntrada">Archivo a comprimir ingresado desde la API</param>
        /// <param name="Path1">Ruta del donde se escribira el archivo</param>
        /// <param name="NuevoNombre">Nuevo nombre del archivo</param>
        public void ProcesoCompresionHuffman(IFormFile ArchivoEntrada, string Path1,string NuevoNombre) {
            string ArchivoAString = EspaciosInnecesarios(ArchivoEntrada);
            byte[] ArchivoAByte = Encoding.ASCII.GetBytes(ArchivoAString);
            byte[] ArchivoCompresoHuff = Compresion.CompresionCompleta(ArchivoAByte);
            var RutaArchivo = Path.Combine(Path1, NuevoNombre + ".huff");
            NombreArchivoAComprimir = ArchivoEntrada.FileName.ToString();//captura nombre del archivo antes de ser comprimido
            File.WriteAllBytes(RutaArchivo, ArchivoCompresoHuff);
        }

        /// <summary>
        /// metodo que descomprime los datos del archivo compreso 
        /// </summary>
        /// <param name="ArchivoEntrada">Archivo compreso que se ingresa a la API</param>
        /// <param name="PathContendor">Ruta donde se encuentra el archivo</param>
        /// <param name="PathAEscribir">Ruta donde se escribira el resultado de la descompresion</param>
        public void ProcesoDecompresionHuffman(IFormFile ArchivoEntrada, string PathContendor, string PathAEscribir) {
            string NombreArchivo = string.Empty;
            byte[] ArchivoAByte = File.ReadAllBytes((PathContendor + ArchivoEntrada.FileName));
            byte[] ArchivoDecompresoHuff = Descompresion.DescompresionCompleta(ArchivoAByte);
            var RutaArchivo = Path.Combine(PathAEscribir, NombreArchivoAComprimir);
            RutaContenedora = PathContendor;
            RutaAEscribir = PathAEscribir;
            tamanoCompreso = ArchivoAByte.Length;
            tamanoDescompreso = ArchivoDecompresoHuff.Length;
            NombreArchivoADescomprimir = ArchivoEntrada.FileName.ToString();//captura nombre del archivo original

            File.WriteAllBytes(RutaArchivo, ArchivoDecompresoHuff);
        }

        /// <summary>
        /// Escribir los datos de los archivos nombre original, nombre archivo comprimido, ruta archivo compreso, factor, razon, porcentaje
        /// </summary>
        /// <param name="PathCompressions">Ruta de archivo donde se escribira el archivo de compress</param>
        public void ProcesoEscrituraDatosHuffman(string PathCompressions) {
            double razon = 0;
            double factor = 0;
            double Porcentaje = 0;
            var Path1 = Path.Combine(PathCompressions, +time.Minute+"CompressionsHuffman" + ".txt");
            using (StreamWriter Escritura = new StreamWriter(Path1))
            {
                for (int i = 0; i <= 5; i++)
                {

                    if (i == 0)
                    {
                        Escritura.Write("Nombre Original: "+ NombreArchivoAComprimir);
                    }
                    if (i == 1)
                    {
                        Escritura.Write(Environment.NewLine + "Nombre Archivo Compreso: "+NombreArchivoADescomprimir + ", Ruta Contenedora: " + RutaContenedora);
                    }
                    if (i == 2)
                    {
                        razon = Math.Round(Convert.ToDouble(tamanoCompreso / tamanoDescompreso), 2);
                        Escritura.Write(Environment.NewLine + "Razon de compresion: " + razon.ToString());
                    }
                    if (i == 3)
                    {
                        factor = Math.Round(Convert.ToDouble(tamanoDescompreso / tamanoCompreso), 2);
                        Escritura.Write(Environment.NewLine + "Factor de compresion: " + factor.ToString());
                    }
                    if (i == 4)
                    {
                        Porcentaje = Math.Round(((factor / razon)*100),2);
                        Escritura.Write(Environment.NewLine + "Porcentaje de compresion: " + Porcentaje.ToString()+"%");
                    }
                    if (i == 5)
                    {
                        Escritura.Write(Environment.NewLine + "Archivo Compreso y decompreso con: Huffman");
                    }

                }
            }
        }
    }
}
