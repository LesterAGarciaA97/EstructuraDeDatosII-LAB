using LaboratorioReposicionEDII.Class.LZW;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Models.LZW
{
    public class MetodosNecesariosL
    {
        private static string NombreArchivoOriginal;
        private static double TamanoCompreso = 0;
        private static double TamanoDescompreso = 0;
        private static string NombreArchivoCompreso;
        private static string RutaContenedora;
        private DateTime time = DateTime.Now;
        private string EspaciosInnecesarios(IFormFile Archivo)
        {
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
                if (temp.Contains("\r\n"))
                {
                    TextoResultante = temp.Replace("\r\n", "\n");
                }
                Lectura.Close();
            }
            return TextoResultante;
        }
        /// <summary>
        /// metodo que utiliza los datos correctos para la compresion 
        /// </summary>
        /// <param name="ArchivoEntrada"></param>
        /// <param name="PathContenedora"></param>
        /// <param name="NuevoNombre"></param>
        public void ProcesoCompresionLZW(IFormFile ArchivoEntrada, string PathContenedora ,string NuevoNombre){
            CompresionLzw Compresion = new CompresionLzw();
            var TextofFile = EspaciosInnecesarios(ArchivoEntrada);
            var TextoCompreso = Compresion.LzwEncode(TextofFile);
            NombreArchivoOriginal = ArchivoEntrada.FileName.ToString();
            RutaContenedora = PathContenedora;
            File.WriteAllText(PathContenedora + NuevoNombre + ".lzw", TextoCompreso.ToString());
        }
        /// <summary>
        /// metodo que descomprime los datos del archivo compreso
        /// </summary>
        /// <param name="ArchivoEntrada">archivo de entrada cifrado con el metodo.</param>
        /// <param name="pathContenedora">ruta donde se escribira el resultado del metodo</param>
        public void ProcesoDecompresionLZW(IFormFile ArchivoEntrada, string pathContenedora) {
            DescompresionLzw Descompresion = new DescompresionLzw();
            var TextoOfFile = EspaciosInnecesarios(ArchivoEntrada);
            var TextoDecompreso = Descompresion.LzwDecode(TextoOfFile);
            NombreArchivoCompreso = ArchivoEntrada.FileName.ToString();
            TamanoCompreso = TextoOfFile.Length;
            TamanoDescompreso = TextoDecompreso.Length;
            File.WriteAllText(pathContenedora + NombreArchivoOriginal, TextoDecompreso.ToString());

        }
        /// <summary>
        /// Escribir los datos de los archivos nombre original, nombre archivo comprimido, ruta archivo compreso, factor, razon, porcentaje
        /// </summary>
        /// <param name="PathCompressions">Ruta de archivo donde se escribira el archivo de compress</param>
        public void ProcesoEscrituraDatosLZW(string PathCompressions) {
            double razon = 0;
            double factor = 0;
            double Porcentaje = 0;
            var Path1 = Path.Combine(PathCompressions, +time.Minute + "CompressionsLZW" + ".txt");
            using (StreamWriter Escritura = new StreamWriter(Path1))
            {
                for (int i = 0; i <= 5; i++)
                {

                    if (i == 0)
                    {
                        Escritura.Write("Nombre Original: " + NombreArchivoOriginal);
                    }
                    if (i == 1)
                    {
                        Escritura.Write(Environment.NewLine + "Nombre Archivo Compreso: " + NombreArchivoCompreso + ", Ruta Contenedora: " + RutaContenedora);
                    }
                    if (i == 2)
                    {
                        razon = Math.Round(Convert.ToDouble(TamanoCompreso / TamanoDescompreso), 2);
                        Escritura.Write(Environment.NewLine + "Razon de compresion: " + razon.ToString());
                    }
                    if (i == 3)
                    {
                        factor = Math.Round(Convert.ToDouble(TamanoDescompreso / TamanoCompreso), 2);
                        Escritura.Write(Environment.NewLine + "Factor de compresion: " + factor.ToString());
                    }
                    if (i == 4)
                    {
                        Porcentaje = Math.Round(((factor / razon) * 100), 2);
                        Escritura.Write(Environment.NewLine + "Porcentaje de compresion: " + Porcentaje.ToString() + "%");
                    }
                    if (i == 5)
                    {
                        Escritura.Write(Environment.NewLine + "Archivo Compreso y decompreso con: LZW");
                    }

                }
            }

        }
    }
}
