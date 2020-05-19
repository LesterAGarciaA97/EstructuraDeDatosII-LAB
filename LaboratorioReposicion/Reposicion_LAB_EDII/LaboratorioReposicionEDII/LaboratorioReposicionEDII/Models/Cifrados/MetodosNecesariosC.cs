using LaboratorioReposicionEDII.Class;
using LaboratorioReposicionEDII.Class.Cifrados;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Models.Cifrados
{
    public class MetodosNecesariosC
    {
        /// <summary>
        /// Metodo que extrae la cadena dentro del archivo
        /// </summary>
        /// <param name="Archivo">El archivo cargado al metodo</param>
        /// <returns></returns>
        private string EspaciosInnecesarios(IFormFile Archivo)
        {
            string temp = string.Empty;
            string TextoResultante = string.Empty;
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
                Removedor = temp.ToCharArray();
                Receptor = new string[Removedor.Length - 2];
                for (int i = 0; i < Removedor.Length - 2; i++)
                {
                    Receptor[i] = Removedor[i].ToString();
                }
                temp = String.Concat(Receptor);
                if (temp.Contains("\r\n"))
                {
                    TextoResultante = temp.Replace("\r\n", "\n");
                }
                else
                {
                    TextoResultante = temp;
                }
                Lectura.Close();
                
            }
            return TextoResultante;
        }
        /// <summary>
        /// Metodo que maneja los datos, para el cifrado ceaser.
        /// </summary>
        /// <param name="ArchivoCargado">Archivo cargado desde con los datos</param>
        /// <param name="PalabraClave">Clave para realizar con ceaser</param>
        /// <param name="NuevoNombre">nuevo nombre del archivo</param>
        /// <param name="path">ruta para la escritura del archivo contenedor del resultado del metodo</param>
        public void CifradoCeaser(IFormFile ArchivoCargado, string PalabraClave, string NuevoNombre, string path) {
            var ArchivoToString = EspaciosInnecesarios(ArchivoCargado);
            CeaserC _Ceaser = new CeaserC(PalabraClave, ArchivoToString);
            var ArchivoCifradoCeaser = _Ceaser.Cifrado();
            var PathResultante = Path.Combine(path, NuevoNombre + ".txt");
            File.WriteAllText(PathResultante, ArchivoCifradoCeaser);
        }

        /// <summary>
        /// Metodo que maneja los datos, para el cifrado Espiral
        /// </summary>
        /// <param name="ArchivoCargado">Archivo cargado desde con los datos</param>
        /// <param name="N">tamaño de las filas</param>
        /// <param name="M">tamaño de las columnas</param>
        /// <param name="NuevoNombre">nuevo nombre del archivo</param>
        /// <param name="PathAEscribir">ruta para la escritura del archivo contenedor del resultado del metodo</param>
        public void CifradoEspiral(IFormFile ArchivoCargado, int N, int M, string NuevoNombre, string PathAEscribir) {
            var ArchivoToString = EspaciosInnecesarios(ArchivoCargado);
            Espiral _Espiral = new Espiral();
            var TextoCifradoEspiral = _Espiral.CifradoEspiral(ArchivoToString, M, N);
            var PathResultante = Path.Combine(PathAEscribir + NuevoNombre + ".txt");
            File.WriteAllText(PathResultante, TextoCifradoEspiral);
        }

        /// <summary>
        /// Metodo que maneja los datos, para el cifrado vertical
        /// </summary>
        /// <param name="ArchivoCargado">Archivo cargado desde con los datos</param>
        /// <param name="N">tamaño de las filas</param>
        /// <param name="M">tamaño de las columnas</param>
        /// <param name="NuevoNombre">nuevo nombre del archivo</param>
        /// <param name="PathAEscribir">ruta para la escritura del archivo contenedor del resultado del metodo</param>
        public void CifradoVertical(IFormFile ArchivoCargado, int N, int M, string NuevoNombre, string PathAEscribir) {
            var ArchivoToString = EspaciosInnecesarios(ArchivoCargado);
            Vertical _Vertical = new Vertical();
            var TextoCifradoVertical = _Vertical.CifradoVertical(ArchivoToString, N, M);
            var PathResultante = Path.Combine(PathAEscribir + NuevoNombre + ".txt");
            File.WriteAllText(PathResultante, TextoCifradoVertical);
        }
        /// <summary>
        /// Metodo que maneja los datos, para el decifrado ceaser
        /// </summary>
        /// <param name="ArchivoCargado">Archivo cargado desde con los datos</param>
        /// <param name="PalabraClave">Clave para realizar con cease</param>
        /// <param name="NuevoNombre">nuevo nombre del archivo</param>
        /// <param name="path">ruta para la escritura del archivo contenedor del resultado del metodo</param>
        public void DecifradoCeaser(IFormFile ArchivoCargado, string PalabraClave, string NuevoNombre, string path) {
            var ArchivoToString = EspaciosInnecesarios(ArchivoCargado);
            CeaserC _Ceaser = new CeaserC(PalabraClave, ArchivoToString);
            var ArchivoDecifradoCeaser = _Ceaser.Descifrado();
            var PathResultante = Path.Combine(path + NuevoNombre + ".txt");
            File.WriteAllText(PathResultante, ArchivoDecifradoCeaser);
        }

        /// <summary>
        /// Metodo que maneja los datos, para el decifrado Espiral
        /// </summary>
        /// <param name="ArchivoCargado">Archivo cargado desde con los datos</param>
        /// <param name="N">tamaño de las filas</param>
        /// <param name="M">tamaño de las columnas</param>
        /// <param name="NuevoNombre">nuevo nombre del archivo</param>
        /// <param name="PathAEscribir">ruta para la escritura del archivo contenedor del resultado del metodo</param>
        public void DecifradoEspiral(IFormFile ArchivoCargado, int N, int M, string NuevoNombre, string PathAEscribir) {
            var ArchivoToString = EspaciosInnecesarios(ArchivoCargado);
            Espiral _Espiral = new Espiral();
            var TextoCifradoEspiral = _Espiral.DescrifradoEspiral(ArchivoToString, M, N);
            var PathResultante = Path.Combine(PathAEscribir + NuevoNombre + ".txt");
            File.WriteAllText(PathResultante, TextoCifradoEspiral);
        }

        /// <summary>
        /// Metodo que maneja los datos, para el decifrado vertical
        /// </summary>
        /// <param name="ArchivoCargado">Archivo cargado desde con los datos</param>
        /// <param name="N">tamaño de las filas</param>
        /// <param name="M">tamaño de las columnas</param>
        /// <param name="NuevoNombre">nuevo nombre del archivo</param>
        /// <param name="PathAEscribir">ruta para la escritura del archivo contenedor del resultado del metodo</param>
        public void DecifradoVertical(IFormFile ArchivoCargado, int N, int M, string NuevoNombre, string PathAEscribir) {
            var ArchivoToString = EspaciosInnecesarios(ArchivoCargado);
            Vertical _Vertical = new Vertical();
            var TextoCifradoVertical = _Vertical.DecifradoVertical(ArchivoToString, N, M);
            var PathResultante = Path.Combine(PathAEscribir + NuevoNombre + ".txt");
            File.WriteAllText(PathResultante, TextoCifradoVertical);
        }

    }
}
